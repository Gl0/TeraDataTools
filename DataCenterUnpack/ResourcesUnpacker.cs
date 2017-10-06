using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DataCenterUnpack
{
    class ResourcesUnpacker
    {
        struct catEntry
        {
            public Int32 size;
            public Int32 offset;
            public string fileName;
        }

        private static string ReadString(BinaryReader reader)
        {
            var builder = new StringBuilder();
            while (true)
            {
                var c = reader.ReadChar();
                if (c == 0) return builder.ToString();
                builder.Append(c);
            }
        }

        public static void Unpack(string inputFileName, string outputDirectory)
        {
            var xorTransform = new XorTransform();
            var stream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var reader = new BinaryReader(stream, Encoding.Unicode);
            reader.BaseStream.Position = stream.Length - 8;
            var offset = reader.ReadInt32();
            reader.BaseStream.Position = stream.Length - offset - 8;
            var files = new List<catEntry>();
            using (MemoryStream catStream = new MemoryStream(reader.ReadBytes(offset)))
            {
                var decrypted = new MemoryStream();
                using (CryptoStream catCryptoStream = new CryptoStream(catStream, xorTransform, CryptoStreamMode.Read)) { catCryptoStream.CopyTo(decrypted); }
                var catReader = new BinaryReader(decrypted, Encoding.Unicode);
                catReader.BaseStream.Position = 0;
                while (catReader.BaseStream.Position < catReader.BaseStream.Length-1)
                {
                    catEntry file;
                    file.size = catReader.ReadInt32();
                    file.offset = catReader.ReadInt32();
                    file.fileName = ReadString(catReader);
                    files.Add(file);
                }
            }
            foreach (var file in files)
            {
                reader.BaseStream.Position = file.offset;
                using (MemoryStream fileStream = new MemoryStream(reader.ReadBytes(file.size)))
                {
                    var decrypted = new MemoryStream();
                    using (CryptoStream catCryptoStream = new CryptoStream(fileStream, xorTransform, CryptoStreamMode.Read)) { catCryptoStream.CopyTo(decrypted); }
                    File.WriteAllBytes(Path.Combine(outputDirectory,file.fileName),decrypted.ToArray());
                }
            }
        }
    }
}
