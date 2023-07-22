using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVVMCore.Common.Utilities
{
    public class JSONDeserializeException : Exception
    {
        public JSONDeserializeException(string message, Exception e, string json) : base(message, e)
        {
            this.JSON = json;
        }

        /// <summary>
        /// JSON文字列
        /// </summary>
        public string JSON { get; set; } = string.Empty;
    }

    public static class JSONUtil
    {
        /// <summary>
        /// JSONファイルからの読み込み
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="path">ファイルパス</param>
        /// <returns>JSONオブジェクト</returns>
        public static T DeserializeFromText<T>(string jsontext)
        {
            try
            {
                // デシリアライズオブジェクト関数に読み込んだデータを渡して、
                // 指定されたデータ用のクラス型で値を返す。
                return JsonSerializer.Deserialize<T>(new MemoryStream(Encoding.UTF8.GetBytes(jsontext)));
            }
            catch (Exception e)
            {
                throw new JSONDeserializeException(e.Message, e, jsontext);
            }
        }

        /// <summary>
        /// JSONファイルからの読み込み
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="filepath">ファイルパス</param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string filepath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filepath, Encoding.UTF8))
                {
                    string str = sr.ReadToEnd();
                    // デシリアライズオブジェクト関数に読み込んだデータを渡して、
                    // 指定されたデータ用のクラス型で値を返す。
                    return JsonSerializer.Deserialize<T>(str);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// JSONファイルへの書き出し
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="elem">要素</param>
        /// <param name="filepath">ファイルパス</param>
        public static void SerializeFromFile<T>(T elem, string filepath)
        {
            try
            {
                var json = JsonSerializer.Serialize<T>(elem);
                File.WriteAllText(filepath, json);
            }
            catch
            {
                throw;
            }
        }
    }
}
