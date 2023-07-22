using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVVMCore.Common.Utilities
{
    /// <summary>
    /// XMLのユーティリティクラス
    /// </summary>
    public class XMLUtil
    {
        #region シリアライズ処理
        /// <summary>
        /// シリアライズ処理
        /// </summary>
        /// <typeparam name="T">シリアライズする型</typeparam>
        /// <param name="filename">出力先ファイルパス</param>
        /// <param name="data">シリアライズするデータ</param>
        /// <returns>入力データそのまま</returns>
        public static T Seialize<T>(string filename, T data)
        {
            // ファイルを作成
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, data);             // シリアライズ
            }

            return data;
        }
        /// <summary>
        /// シリアライズ処理
        /// </summary>
        /// <typeparam name="T">シリアライズする型</typeparam>
        /// <param name="stream">シリアライズするStream</param>
        /// <returns>入力データそのまま</returns>
        public static T Seialize<T>(Stream stream)
        {
            T data = default(T);
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);             // シリアライズ

            return data;
        }
        #endregion

        #region デシリアライズ処理
        /// <summary>
        /// デシリアライズ処理
        /// </summary>
        /// <typeparam name="T">デシリアライズする型</typeparam>
        /// <param name="filename">入力ファイルパス</param>
        /// <returns>デシリアライズ後のデータ</returns>
        public static T Deserialize<T>(string filename)
        {
            // ファイルを開く
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                // デシリアライズ
                return Deserialize<T>(stream);
            }
        }

        /// <summary>
        /// デシリアライズ
        /// </summary>
        /// <typeparam name="T">デシリアライズする型</typeparam>
        /// <param name="stream">デシリアライズするstream</param>
        /// <returns>デシリアライズ後のデータ</returns>
        public static T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);       // デシリアライズ
        }
        #endregion



    }
}
