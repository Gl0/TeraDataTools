using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SharpDisasm;
using SharpDisasm.Udis86;

namespace DataCenterUnpack
{
    class KeyScanner
    {
        static readonly byte[] _pattern32 = new byte[]
        {
            0x56,                               // push esi
            0x57,                               // push edi
            0x50,                               // push eax
            0x8D, 0x45, 0xF4,                   // lea eax, [ebp-0xC]
            0x64, 0xA3, 0x00, 0x00, 0x00, 0x00, // mov large fs:0x0, eax
            0x8B, 0x73, 0x08,                   // mov esi, [ebp+0x8]
        };

        static readonly byte[] _pattern64 = new byte[]
        {
            0x48, 0x33, 0xC4,                                 // xor rax,rsp
            0x48, 0x89, 0x84, 0x24, 0x40, 0x01, 0x00, 0x00,   // mov [rsp+0x140],rax
            0x4C, 0x8B, 0xF9,                                 // mov r15, rcx
            0x41, 0xC7, 0x43                                  // mov dword ptr [r11-xx],0xXXXXXXXX
        };

        private static byte[] GetBytes(uint i)
        {
            var bytes = new byte[4];
            bytes[0] = (byte)(i >> 0);
            bytes[1] = (byte)(i >> 8);
            bytes[2] = (byte)(i >> 16);
            bytes[3] = (byte)(i >> 24);
            return bytes;
        }

        public static List<Tuple<string, string, int>> Find()
        {
            var candidates = new List<Tuple<string, string, int>>();

            var process = Process.GetProcessesByName("tera").SingleOrDefault();
            if (process == null) throw new ApplicationException("Tera is not runing");
            using (var memoryScanner = new MemoryScanner(process)) {
                bool x64 = memoryScanner.Is64Bit();
                var memoryRegions = memoryScanner.MemoryRegions();
                var relevantRegions = memoryRegions.Where(x => x.State == MemoryScanner.PageState.Commit && (x.Protect == MemoryScanner.PageFlags.ExecuteReadWrite||x.Protect==MemoryScanner.PageFlags.ExecuteRead));
                foreach (var memoryRegion in relevantRegions)
                {
                    var data = memoryScanner.ReadMemory(memoryRegion.BaseAddress, (int)memoryRegion.RegionSize);
                    var searcher= x64 ? new BoyerMoore(_pattern64) : new BoyerMoore(_pattern32);
                    var dataSlice = new byte[300];
                    var index = 0;
                    while ((index = searcher.Search(data,index)) >= 0)
                    {
                        index++;
                        Array.Copy(data, index, dataSlice, 0, Math.Min(data.Length - index, dataSlice.Length));
                        var disasm = new Disassembler(dataSlice, x64 ? ArchitectureMode.x86_64 : ArchitectureMode.x86_32, (ulong)memoryRegion.BaseAddress + (uint)index, true);
                        try
                        {
                            var instructions = disasm.Disassemble().TakeWhile(x => !x.Error && x.Mnemonic != ud_mnemonic_code.UD_Iint3);

                            var movs = new List<Instruction>();
                            foreach (var instruction in instructions)
                            {
                                if (instruction.Mnemonic == ud_mnemonic_code.UD_Imov && x64? instruction.Operands[0].Base == ud_type.UD_R_R11 : instruction.Operands[0].Base==ud_type.UD_R_EBP)
                                    movs.Add(instruction);
                                else
                                {
                                    if (movs.Count == 8)
                                    {
                                        var keyIv = string.Join(" ", movs.Select(x => x.Operands[1].Value).Select(x => BitConverter.ToString(GetBytes((uint)x)).Replace("-", "")));
                                        var interestingChars = keyIv.Count(c => !"0F ".Contains(c));
                                        var key = keyIv.Substring(0, 32 + 3);
                                        var iv = keyIv.Substring(32 + 4, 32 + 3);

                                        candidates.Add(Tuple.Create(key, iv, interestingChars));
                                        movs.Clear();
                                        break;
                                    }
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                        }
                    }
                }
            }
            var candidatesByQuality = candidates.OrderByDescending(t => t.Item3).Where(t => t.Item3 >= 32).ToList();
            return candidatesByQuality;
        }
    }
}
