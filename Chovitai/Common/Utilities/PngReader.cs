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

        #region ChunkDataの読み込み処理
        /// <summary>
        /// ChunkDataの読み込み処理
        /// </summary>
        /// <param name="reader">バイナリーデータ</param>
        /// <returns>チャンク</returns>
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

        public static Chunk ReadChunk(byte[] data)
        {
            byte[] bytesLength = new byte[4];
            Array.Copy(data, 0, bytesLength, 0, 4);

            byte[] bytesChunkType = new byte[4];
            Array.Copy(data, 4, bytesChunkType, 0, 4);

            uint length = BitConverter.ToUInt32(bytesLength.Reverse().ToArray());

            byte[] chunkData = new byte[(int)length];
            Array.Copy(data, 8, chunkData, 0, (int)length);

            byte[] bytesCrc = new byte[4];
            Array.Copy(data, 8+ (int)length, bytesCrc, 0, 4);
            uint crc = BitConverter.ToUInt32(bytesCrc.Reverse().ToArray());

            return new Chunk(length, bytesChunkType, chunkData, crc);
        }
        #endregion
        private Encoding _latin1 = Encoding.GetEncoding(28591);

        public byte[] CreateTextChunkData(string text)
        {
            // `tEXt` はASCIIエンコーディング
            byte[] chunkTypeData = Encoding.ASCII.GetBytes("tEXt");

            // keywordはLatin1エンコーディング
            byte[] keywordData = _latin1.GetBytes("Comment");

            // 区切り用の `0` を配列で確保
            byte[] separatorData = new byte[] { 0 };

            // data部分はLatin1エンコーディング
            byte[] textData = _latin1.GetBytes(text);

            int headerSize = sizeof(byte) * (chunkTypeData.Length + sizeof(int));
            int footerSize = sizeof(byte) * 4; // CRC
            int chunkDataSize = keywordData.Length + separatorData.Length + textData.Length;

            // チャンクデータ部分を生成
            byte[] chunkData = new byte[chunkDataSize];
            Array.Copy(keywordData, 0, chunkData, 0, keywordData.Length);
            Array.Copy(separatorData, 0, chunkData, keywordData.Length, separatorData.Length);
            Array.Copy(textData, 0, chunkData, keywordData.Length + separatorData.Length, textData.Length);

            // Length用データ
            byte[] lengthData = BitConverter.GetBytes(chunkDataSize);

            // CRCを計算（※）
            uint crc = Crc32.Hash(0, chunkTypeData);
            crc = Crc32.Hash(crc, chunkData);
            byte[] crcData = BitConverter.GetBytes(crc);

            // 全体のデータを確保
            byte[] data = new byte[headerSize + chunkDataSize + footerSize];

            // LengthとCRCはビッグエンディアンにする必要があるのかReverseする必要がある（※）
            Array.Reverse(lengthData);
            Array.Reverse(crcData);

            Array.Copy(lengthData, 0, data, 0, lengthData.Length);
            Array.Copy(chunkTypeData, 0, data, lengthData.Length, chunkTypeData.Length);
            Array.Copy(chunkData, 0, data, lengthData.Length + chunkTypeData.Length, chunkData.Length);
            Array.Copy(crcData, 0, data, lengthData.Length + chunkTypeData.Length + chunkData.Length, crcData.Length);

            return data;
        }
    }
}
