using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Utilities
{
    public struct Chunk
    {
        public uint Length;
        public byte[] ChunkType;
        public byte[] ChunkData;
        public uint Crc;

        public Chunk(uint length, byte[] chunkType, byte[] chunkData, uint crc)
        {
            Length = length;
            ChunkType = chunkType;
            ChunkData = chunkData;
            Crc = crc;
        }
    }

    public class PngReader
    {

        public static bool ReadPngSignature(BinaryReader reader)
        {
            byte[] signature = reader.ReadBytes(8);

            if (signature[0] == 137 && signature[1] == 80 && signature[2] == 78 && signature[3] == 71 &&
                signature[4] == 13 && signature[5] == 10 && signature[6] == 26 && signature[7] == 10)
            {
                return true;
            }

            return false;
        }


        public static Chunk ReadChunk(BinaryReader reader)
        {
            byte[] bytesLength = reader.ReadBytes(4);
            byte[] bytesChunkType = reader.ReadBytes(4);

            uint length = BitConverter.ToUInt32(bytesLength.Reverse().ToArray());

            byte[] chunkData = reader.ReadBytes((int)length);

            byte[] bytesCrc = reader.ReadBytes(4);
            uint crc = BitConverter.ToUInt32(bytesCrc.Reverse().ToArray());

            return new Chunk(length, bytesChunkType, chunkData, crc);
        }


    }
}
