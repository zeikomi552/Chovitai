using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Utilities
{
    public static class Crc32
    {
        private static uint[] _crcTable = MakeCrcTable();

        private static uint[] MakeCrcTable()
        {
            uint[] a = new uint[256];

            for (uint i = 0; i < a.Length; ++i)
            {
                uint c = i;
                for (int j = 0; j < 8; ++j)
                {
                    c = ((c & 1) != 0) ? (0xedb88320 ^ (c >> 1)) : (c >> 1);
                }

                a[i] = c;
            }

            return a;
        }

        private static uint Calculate(uint crc, byte[] buffer)
        {
            uint c = crc;

            for (int i = 0; i < buffer.Length; ++i)
            {
                c = _crcTable[(c ^ buffer[i]) & 0xff] ^ (c >> 8);
            }

            return c;
        }

        public static uint Hash(uint crc, byte[] buffer)
        {
            crc ^= 0xffffffff;
            return Calculate(crc, buffer) ^ 0xffffffff;
        }
    }
}
