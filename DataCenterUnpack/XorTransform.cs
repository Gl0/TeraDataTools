using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DataCenterUnpack {
    class XorTransform : ICryptoTransform
    {
        //thx Caali
        private static byte[] Key = new byte[32] { 0x1C, 0x24, 0x00, 0x00, 0x1F, 0x04, 0x00, 0x00, 0x72, 0xF4, 0x00, 0x00, 0x1D, 0x62, 0x00, 0x00, 0xBD, 0xA8, 0x00, 0x00, 0xDB, 0xA7, 0x00, 0x00, 0x01, 0x30, 0x00, 0x00, 0x33, 0x27, 0x00, 0x00 };

        #region simple wrap

        public bool CanReuseTransform
        {
            get { return true; }
        }

        public bool CanTransformMultipleBlocks
        {
            get { return true; }
        }

        public int InputBlockSize
        {
            get { return Key.Length; }
        }

        public int OutputBlockSize
        {
            get { return Key.Length; }
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            for (var i = inputOffset; i < inputCount; i++)
            {
                outputBuffer[i] = (byte) (inputBuffer[i] ^ Key[i % 32]);
            }
            return inputCount;
        }

        #endregion

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            if (inputCount % 32 == 0)
            {
                var transformed = new byte[inputCount];
                TransformBlock(inputBuffer, inputOffset, inputCount, transformed, 0);
                return transformed;
            }
            else
            {
                byte[] lastBlocks = new byte[inputCount / 32 + 32];
                Buffer.BlockCopy(inputBuffer, inputOffset, lastBlocks, 0, inputCount);
                byte[] result = TransformFinalBlock(lastBlocks, 0, lastBlocks.Length);
                Debug.Assert(inputCount < result.Length);
                Array.Resize(ref result, inputCount);
                return result;
            }
        }

        public void Dispose()
        {
        }

    }
}