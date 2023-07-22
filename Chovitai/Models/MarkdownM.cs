using Chovitai.Common.Utilities;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models
{
    public class MarkdownM : ModelBase
    {
        /// <summary>
        /// マークダウンの出力処理
        /// 同時に同じ階層に出力ファイルと同じ名称のディレクトリが作成され
        /// そこにイメージをコピーします
        /// </summary>
        /// <param name="list">イメージファイルリスト</param>
        /// <param name="dirctory_path"イメージが保管されているディレクトリ></param>
        /// <param name="mk_filepath">マークダウンの出力先</param>
        public static void OutputMarkdown(List<FileInfoM> list, string dirctory_path, string mk_filepath)
        {
            // 拡張子なしファイル名取得
            var mk_filename = Path.GetFileNameWithoutExtension(mk_filepath);

            // リストの並べかえ（プロンプト順）
            var list_sort = (from x in list
                             orderby x.BasePrompt, x.FilePath
                             select x).ToList<FileInfoM>();

            StringBuilder sb = new StringBuilder();

            string base_prompt = string.Empty;
            int no = 1;
            foreach (var tmp in list_sort)
            {
                sb.AppendLine($"## No.{no++} {tmp.Prompt}");
                sb.AppendLine($"");
                sb.AppendLine($"[Google翻訳 -{tmp.Prompt}](https://translate.google.co.jp/?sl=en&tl=ja&text={tmp.Prompt.Replace(" ", "+")}&op=translate)");
                string img_file_soutai = Path.Combine(Path.GetFileNameWithoutExtension(mk_filepath), mk_filename + "_" + Path.GetFileName(tmp.FilePath));
                sb.AppendLine($"![]({img_file_soutai})");
                sb.AppendLine($"");
                sb.AppendLine($"```");
                sb.AppendLine($"{tmp.ImageText.Replace("parameters:", "prompt: ")}");
                sb.AppendLine($"```");
            }

            // ファイル出力処理
            File.WriteAllText(mk_filepath, sb.ToString());

            // ファイル名をディレクトリ名として使用する
            var img_dir = Path.Combine(PathManager.GetCurrentDirectory(mk_filepath), mk_filename);

            // ファイル移動先ディレクトリの作成
            PathManager.CreateDirectory(img_dir);

            // ファイルコピー
            MarkdownM.CopyFilesParallel(dirctory_path, img_dir, "*.png", mk_filename);
        }


        /// <summary>
        /// ファイルの並列コピー
        /// </summary>
        /// <param name="srcPath">コピー元ディレクトリ</param>
        /// <param name="dstPath">コピー先ディレクトリ</param>
        /// <param name="filter">フィルター</param>
        /// <param name="additional_file_name">ファイル名に追加する文字列(重複を避けるため)</param>
        public static void CopyFilesParallel(string srcPath, string dstPath, string filter, string additional_file_name)
        {
            // コピー元ファイルの一覧（FileInfoの配列）を作る
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(srcPath);
            System.IO.FileInfo[] files =
                dir.GetFiles(filter, System.IO.SearchOption.AllDirectories);

            // マルチスレッドでコピー
            Parallel.ForEach(files, file =>
            {
                string dst = dstPath + "\\" + additional_file_name + "_" + file.Name;
                System.IO.File.Copy(file.FullName, dst, true);
            });
        }
    }
}
