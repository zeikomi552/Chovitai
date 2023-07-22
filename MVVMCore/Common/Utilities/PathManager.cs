using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.Common.Utilities
{
    public class PathManager
    {
        #region アプリケーションフォルダの取得
        /// <summary>
        /// アプリケーションフォルダの取得
        /// </summary>
        /// <returns>アプリケーションフォルダパス</returns>
        public static string GetApplicationFolder()
        {
            var fv = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fv.CompanyName, fv.ProductName);
        }
        #endregion

        #region ディレクトリを再帰的に作成する
        /// <summary>
        /// ディレクトリを再帰的に作成する
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string dir_path)
        {
            if (!Directory.Exists(dir_path))
            {
                string parent = Directory.GetParent(dir_path).FullName;
                CreateDirectory(parent);
                Directory.CreateDirectory(dir_path);
            }
        }
        #endregion

        #region ファイルのカレントディレクトリを作成する
        /// <summary>
        /// ファイルのカレントディレクトリを作成する
        /// </summary>
        /// <param name="file_path">ファイルパス</param>
        public static void CreateCurrentDirectory(string file_path)
        {
            string parent = Directory.GetParent(file_path).FullName;
            if (!Directory.Exists(parent))
            {
                CreateDirectory(parent);
            }
        }
        #endregion

        #region カレントディレクトリパスを取得する
        /// <summary>
        /// カレントディレクトリパスを取得する
        /// </summary>
        /// <param name="file_path">ファイルパス</param>
        /// <returns>カレントディレクトリパス</returns>
        public static string GetCurrentDirectory(string file_path)
        {
            return Directory.GetParent(file_path).FullName;
        }
        #endregion

        #region 最後の文字列が指定した文字と同じなら最後の文字列を削除する
        /// <summary>
        /// 最後の文字列が指定した文字と同じなら最後の文字列を削除する
        /// </summary>
        /// <param name="text">対象文字列</param>
        /// <param name="lasttext">最後も文字列</param>
        /// <returns>削除後の文字列</returns>
        public static string TrimLastText(string text, string lasttext)
        {
            if (text.Length >= lasttext.Length)
            {
                var last = text.Substring(text.Length - lasttext.Length);

                if (last.Equals(lasttext))
                {
                    return text.Substring(0, text.Length - lasttext.Length);
                }
            }
            return text;
        }
        #endregion
    }
}
