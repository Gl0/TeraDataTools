using System;
using System.IO;

namespace DataCenterUnpack
{
    class ResourcesUnpacker
    {
        //thx Caali
        private static byte[] Key = new byte[32] { 0x1C, 0x24, 0x00, 0x00, 0x1F, 0x04, 0x00, 0x00, 0x72, 0xF4, 0x00, 0x00, 0x1D, 0x62, 0x00, 0x00, 0xBD, 0xA8, 0x00, 0x00, 0xDB, 0xA7, 0x00, 0x00, 0x01, 0x30, 0x00, 0x00, 0x33, 0x27, 0x00, 0x00 };
        //thx Gl0 (https://github.com/Gl0)
        private static byte[] MagicPattern = new byte[] { 0x95, 0x74, 0x4E, 0x47, 0x12, 0x0E };
        public static void Unpack(string inputFileName, string outputDirectory)
        {

            var resource = File.ReadAllBytes(inputFileName);
            var searcher = new BoyerMoore(MagicPattern).SearchAll(resource);
            var j = 0;
            var laststart = 0;
            for (var i = 0; i < resource.Length; ++i)
            {
                resource[i] ^= Key[j % 32];
                j++;
                if (searcher.Contains(i))
                {
                    var block = new byte[i - laststart];
                    Array.Copy(resource, laststart, block, 0, i - laststart);
                    if (laststart != 0) block[0] = 0x89;
                    File.WriteAllBytes(Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(inputFileName) + "." + laststart + (laststart == 0 ? ".txt" : ".png")), block);
                    j = 1;
                    laststart = i;
                }
            }
        }
    }
}
