using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DataCenterUnpack
{
    class MemoryScanner : IDisposable
    {
        // REQUIRED CONSTS

        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;


        // REQUIRED METHODS

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeProcessHandle OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(SafeProcessHandle hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(SafeProcessHandle hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(SafeProcessHandle hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWow64Process([In] SafeProcessHandle hProcess, [Out] out bool lpSystemInfo);
        public bool Is64Bit()
        {
            bool retVal;

            IsWow64Process(processHandle, out retVal);

            return !retVal;
        }

        internal sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern bool CloseHandle(IntPtr handle);

            private SafeProcessHandle()
                : base(true)
            {
            }

            internal SafeProcessHandle(IntPtr handle)
                : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return CloseHandle(handle);
            }
        }

        [Flags]
        public enum PageFlags
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            TargetsInvalid = 0x40000000,
            TargetsNoUpdate = 0x40000000,

            Guard = 0x100,
            NoCache = 0x200,
            WriteCombine = 0x400,
        }

        public enum PageState
        {
            Commit = 0x1000,
            Free = 0x10000,
            Reserve = 0x2000
        }

        // REQUIRED STRUCTS

        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public PageState State;
            public PageFlags Protect;
            public int lType;

            public override string ToString()
            {
                var result = string.Format("{0:x8} - {1:x8} {2}", BaseAddress, BaseAddress + RegionSize, Protect);
                if (State != PageState.Commit)
                    result += " " + State;
                return result;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION64
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public int AllocationProtect;
            public UInt32 __alignment1;
            public Int64 RegionSize;
            public PageState State;
            public PageFlags Protect;
            public int Type;
            public UInt32 __alignment2;
            public override string ToString()
            {
                var result = string.Format("{0:x16} - {1:x16} {2}", BaseAddress, (long)BaseAddress + RegionSize, Protect);
                if (State != PageState.Commit)
                    result += " " + State;
                return result;
            }
        }
        private const Int32 ChunkSize = 2000000000;
        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        private SafeProcessHandle processHandle;

        public IEnumerable<MEMORY_BASIC_INFORMATION64> MemoryRegions()
        {
            // getting minimum & maximum address

            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            ulong proc_min_address_l = (ulong)proc_min_address;
            ulong proc_max_address_l = (ulong)proc_max_address;

            MEMORY_BASIC_INFORMATION64 mem_basic_info = new MEMORY_BASIC_INFORMATION64();

            ulong current = proc_min_address_l;
            while (current < proc_max_address_l)
            {
                var informationSize = VirtualQueryEx(processHandle, (IntPtr)current, out mem_basic_info, (uint)Marshal.SizeOf(mem_basic_info));
                if (informationSize == 0)
                    throw new Win32Exception();

                if (mem_basic_info.RegionSize<ChunkSize)
                    yield return mem_basic_info;
                else {
                    Int64 remaining = mem_basic_info.RegionSize;
                    UInt64 currentBase = unchecked((UInt64) (Int64) mem_basic_info.BaseAddress);
                    while (remaining >= ChunkSize) {
                        MEMORY_BASIC_INFORMATION64 chunk = mem_basic_info;
                        chunk.BaseAddress = (IntPtr)(long)currentBase;
                        chunk.RegionSize = ChunkSize;
                        yield return chunk;
                        remaining -= ChunkSize;
                        currentBase = checked(currentBase + ChunkSize);
                    }

                    if (remaining > 0) {
                        MEMORY_BASIC_INFORMATION64 chunk = mem_basic_info;
                        chunk.BaseAddress = (IntPtr)(long)currentBase;
                        chunk.RegionSize = ChunkSize;
                        yield return chunk;
                    }
                }

                // move to the next memory chunk
                current += (ulong)mem_basic_info.RegionSize;
            }
        }


        public byte[] ReadMemory(IntPtr baseAddress, int size)
        {
            var stream = new MemoryStream(size);
            ReadMemory(stream, baseAddress, size);
            Debug.Assert(stream.GetBuffer().Length == stream.Length);
            return stream.GetBuffer();
        }

        public void ReadMemory(MemoryStream stream, IntPtr baseAddress, int size)
        {
            stream.SetLength(size);
            int bytesRead = 0;

            // read everything in the buffer above
            bool success = ReadProcessMemory(processHandle, baseAddress, stream.GetBuffer(), size, ref bytesRead);
            if (!success)
                throw new Win32Exception();
            if (bytesRead != size)
                throw new Exception("Didn't read all bytes");
        }

        //public static bool IsAccessible(MEMORY_BASIC_INFORMATION mem_basic_info)
        //{
        //    return mem_basic_info.Protect == PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT;
        //}

        private static SafeProcessHandle OpenProcess(Process process)
        {
            var result = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);
            if (result.IsInvalid)
                throw new Win32Exception();
            return result;
        }

        public MemoryScanner(Process process)
        {
            processHandle = OpenProcess(process);
        }

        public void Dispose()
        {
            if (processHandle != null)
            {
                processHandle.Close();
                processHandle = null;
            }
        }
    }
}
