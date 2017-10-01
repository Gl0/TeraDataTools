using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCenterUnpack
{
    public class BoyerMoore
    {
        private readonly int[] _jumpTable;
        private readonly byte[] _pattern;
        private readonly int _patternLength;
        public BoyerMoore(byte[] pattern)
        {
            _pattern = pattern;
            _jumpTable = new int[256];
            _patternLength = _pattern.Length;
            for (var index = 0; index < 256; index++)
                _jumpTable[index] = _patternLength;
            for (var index = 0; index < _patternLength - 1; index++)
                _jumpTable[_pattern[index]] = _patternLength - index - 1;
        }
        public unsafe int Search(byte[] byteArray, int startIndex = 0)
        {
            var index = startIndex;
            var limit = byteArray.Length - _patternLength;
            var patternLengthMinusOne = _patternLength - 1;
            fixed (byte* pointerToByteArray = byteArray)
            {
                var pointerToByteArrayStartingIndex = pointerToByteArray + startIndex;
                fixed (byte* pointerToPattern = _pattern)
                {
                    while (index <= limit)
                    {
                        var j = patternLengthMinusOne; while (j >= 0 && pointerToPattern[j] == pointerToByteArrayStartingIndex[index + j])
                            j--;
                        if (j < 0)
                            return index;
                        index += Math.Max(_jumpTable[pointerToByteArrayStartingIndex[index + j]] - _patternLength + 1 + j, 1);
                    }
                }
            }
            return -1;
        }
        public unsafe List<int> SearchAll(byte[] byteArray, int startIndex = 0)
        {
            var index = startIndex;
            var limit = byteArray.Length - _patternLength;
            var patternLengthMinusOne = _patternLength - 1;
            List<int> list = new List<int>();
            fixed (byte* pointerToByteArray = byteArray)
            {
                var pointerToByteArrayStartingIndex = pointerToByteArray + startIndex;
                fixed (byte* pointerToPattern = _pattern)
                {
                    while (index <= limit)
                    {
                        var j = patternLengthMinusOne; while (j >= 0 && pointerToPattern[j] == pointerToByteArrayStartingIndex[index + j])
                            j--;
                        if (j < 0)
                            list.Add(index);
                        index += Math.Max(_jumpTable[pointerToByteArrayStartingIndex[index + j]] - _patternLength + 1 + j, 1);
                    }
                }
            }
            return list;
        }
        public int SuperSearch(byte[] byteArray, int nth, int start = 0)
        {
            var e = start;
            var c = 0;
            do
            {
                e = Search(byteArray, e);
                if (e == -1)
                    return -1;
                c++;
                e++;
            } while (c < nth);
            return e - 1;
        }
    }
}
