using Chovitai.Common.Utilities;
using Chovitai.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Chovitai.ViewModels
{
    public class FileBaseVM : ViewModelBase
    {

        #region ディレクトリ読み込み処理実行中[ExecuteReadDirF]プロパティ
        /// <summary>
        /// ディレクトリ読み込み処理実行中[ExecuteReadDirF]プロパティ用変数
        /// </summary>
        bool _ExecuteReadDirF = false;
        /// <summary>
        /// ディレクトリ読み込み処理実行中[ExecuteReadDirF]プロパティ
        /// </summary>
        public bool ExecuteReadDirF
        {
            get
            {
                return _ExecuteReadDirF;
            }
            set
            {
                if (!_ExecuteReadDirF.Equals(value))
                {
                    _ExecuteReadDirF = value;
                    NotifyPropertyChanged("ExecuteReadDirF");
                }
            }
        }
        #endregion

        #region ファイルリスト[FileList]プロパティ
        /// <summary>
        /// ファイルリスト[FileList]プロパティ用変数
        /// </summary>
        ModelList<FileInfoM> _FileList = new ModelList<FileInfoM>();
        /// <summary>
        /// ファイルリスト[FileList]プロパティ
        /// </summary>
        public ModelList<FileInfoM> FileList
        {
            get
            {
                return _FileList;
            }
            set
            {
                if (_FileList == null || !_FileList.Equals(value))
                {
                    _FileList = value;
                    NotifyPropertyChanged("FileList");
                }
            }
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Close(object sender, EventArgs e)
        {

        }
        #endregion

        #region ファイルウォッチャー
        /// <summary>
        /// ファイルウォッチャー
        /// </summary>
        protected System.IO.FileSystemWatcher? _Watcher = null;
        #endregion

        #region ファイルディレクトリの監視処理開始
        /// <summary>
        /// ファイルディレクトリの監視処理開始
        /// </summary>
        /// <param name="dir">ディレクトリパス</param>
        /// <param name="filePattern">監視対象のファイルパターン</param>
        public void StartDirectoryWatching(string dir, string filePattern)
        {
            if (_Watcher != null) return;

            _Watcher = new System.IO.FileSystemWatcher();

            //監視するディレクトリを指定
            _Watcher.Path = dir;
            //最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
            // 監視パラメータの設定
            _Watcher.NotifyFilter = (NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName
                | NotifyFilters.Attributes
                | NotifyFilters.CreationTime
                | NotifyFilters.Size
                | NotifyFilters.LastAccess
                | NotifyFilters.Security);

            //すべてのファイルを監視
            _Watcher.Filter = filePattern;
            //UIのスレッドにマーシャリングする
            //コンソールアプリケーションでの使用では必要ない
            //watcher.SynchronizingObject = this;

            //イベントハンドラの追加
            _Watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            _Watcher.Error += new ErrorEventHandler(watcher_Error);
            _Watcher.Deleted += new FileSystemEventHandler(watcher_Changed);
            _Watcher.Renamed += new RenamedEventHandler(watcher_Renamed);

            //監視を開始する
            _Watcher.EnableRaisingEvents = true;
            Console.WriteLine("監視を開始しました。");
        }
        #endregion

        #region ファイルウォッチャーの変更イベント
        /// <summary>
        /// ファイルウォッチャーの変更イベント
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void watcher_Changed(System.Object source,
            System.IO.FileSystemEventArgs e)
        {
            try
            {


                switch (e.ChangeType)
                {
                    case System.IO.WatcherChangeTypes.Changed:
                        {
                            Console.WriteLine(
                                "ファイル 「" + e.FullPath + "」が変更されました。");

                            var file_info = GetFileInfo(e.FullPath);
                            if (file_info != null)
                            {
                                // スレッドセーフの呼び出し
                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                    new Action(() =>
                                    {
                                        var bfind = (from x in this.FileList.Items
                                                     where x.FilePath == file_info.FilePath
                                                     select x).Any();

                                        if (!bfind)
                                        {
                                            this.FileList.Items.Add(file_info);
                                        }
                                    }));
                            }
                            break;
                        }
                    case System.IO.WatcherChangeTypes.Created:
                        {
                            var file_info = GetFileInfo(e.FullPath);
                            if (file_info != null)
                            {
                                // スレッドセーフの呼び出し
                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                    new Action(() =>
                                    {
                                        this.FileList.Items.Add(file_info);
                                    }));
                            }

                            // memo : StableDiffusionで作成時は*.tmpファイルが作られてそのあと*.pngになるためリネーム扱いとなる
                            //        したがって、この処理には入らない。本処理はコピペなどで追加した場合用

                            break;
                        }
                    case System.IO.WatcherChangeTypes.Deleted:
                        // スレッドセーフの呼び出し
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() =>
                            {
                                var tmp = (from x in this.FileList.Items
                                           where x.FilePath.Equals(e.FullPath)
                                           select x).FirstOrDefault();

                                if (tmp != null)
                                {
                                    this.FileList.Items.Remove(tmp);
                                }

                            }));

                        //Console.WriteLine(
                        //    "ファイル 「" + e.FullPath + "」が削除されました。");
                        break;
                }
            }
            catch { }
        }
        #endregion

        #region ファイル名が変更された際の処理
        /// <summary>
        /// ファイル名が変更された際の処理
        /// StabledDiffusionで生成する場合は*.tmp → *.pngに変更されるためこの処理に入る
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ev"></param>
        public virtual void watcher_Renamed(System.Object source,
            System.IO.RenamedEventArgs ev)
        {
            try
            {
                //Debug.WriteLine("test-rename");
                var file_info = GetFileInfo(ev.FullPath);
                if (file_info != null)
                {
                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            var old_file = (from x in this.FileList.Items
                                           where x.FilePath.Equals(ev.OldFullPath)
                                           select x).FirstOrDefault();

                            if (old_file != null)
                            {
                                this.FileList.Items.Remove(old_file); // 名称変更前のファイルをリストから削除
                            }

                            this.FileList.Items.Add(file_info); // ファイルの追加処理

                        }));
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ファイルウォッチャーでエラーが発生した場合のイベント
        /// <summary>
        /// ファイルウォッチャーでエラーが発生した場合のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_Error(object sender, ErrorEventArgs e)
        {
            //tbMessage.Text += "Error" + e.GetException().Message + Environment.NewLine;
        }
        #endregion

        #region ファイル情報の読み取り処理
        /// <summary>
        /// ファイル情報の読み取り処理
        /// </summary>
        /// <param name="file">ファイルパス</param>
        /// <returns>ファイル情報</returns>
        public FileInfoM? GetFileInfo(string file)
        {
            int count = 0;
            int max_count = 5;

            // 5回以内にファイルが解放されなければループを抜ける
            while (count < max_count)
            {
                try
                {
                    // バイナリで開く
                    using (var reader = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read)))
                    {
                        // Pngファイルのシグニチャ読み込み
                        if (PngReader.ReadPngSignature(reader))
                        {
                            var ihdrchunk = PngReader.ReadChunk(reader);    // IHDチャンクの読み込み
                            var itextchunk = PngReader.ReadChunk(reader);   // ITextチャンクの読み込み

                            // データがutf - 8の場合
                            var msg = System.Text.Encoding.UTF8.GetString(itextchunk.ChunkData).Replace("\0", ":");

                            var msg_list = msg.Split("\n"); // 分割
                            var prompt = msg_list.ElementAt(0).Replace("parameters:", "");  // Parameterの文字を消す

                            var file_info = new FileInfoM() { FilePath = file, ImageText = msg, Prompt = prompt, BasePrompt = prompt.Split(",").Last().Trim() };

                            return file_info;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch
                {
                    count++;
                    System.Threading.Thread.Sleep(100); // 100ミリ秒待たせる
                }
            }
            return null;
        }
        #endregion

        #region ファイルウォッチャーの終了
        /// <summary>
        /// ファイルウォッチャーの終了
        /// </summary>
        public void FinishDirectoryWatching()
        {
            if (_Watcher != null)
            {
                //監視を終了
                _Watcher.EnableRaisingEvents = false;
                _Watcher.Dispose();
                _Watcher = null;
                Console.WriteLine("監視を終了しました。");
            }
        }
        #endregion


        #region ディレクトリ読み出しの更新処理
        /// <summary>
        /// ディレクトリ読み出しの更新処理
        /// </summary>
        public void RenewDirectory(string dir)
        {
            try
            {
                // ディレクトリを開く
                if (Directory.Exists(dir))
                {
                    // ディレクトリ内のpngファイルの読み込み
                    ReadDirectory(dir);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
            finally
            {
            }
        }
        #endregion



        #region ディレクトリのファイル全て読み込み
        /// <summary>
        /// ディレクトリのファイル全て読み込み
        /// </summary>
        /// <param name="dir">ディレクトリパス</param>
        public void ReadDirectory(string dir)
        {
            try
            {
                // ファイル情報のセット
                this.FileList.Items.Clear();

                Task.Run(() =>
                {
                    // 読み込み中の場合は無視
                    if (this.ExecuteReadDirF)
                        return;

                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.ExecuteReadDirF = true;    // 読み込み処理実行
                        }));

                    // フォルダ内のファイル一覧を取得
                    var fileArray = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);

                    foreach (string file in fileArray)
                    {
                        var file_info = GetFileInfo(file);

                        if (file_info != null)
                        {
                            // スレッドセーフの呼び出し
                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                new Action(() =>
                                {
                                    this.FileList.Items.Add(file_info);
                                }));
                        }
                    }
                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.FileList.SelectedLast();

                            // ファイルウォッチャーをいったん終了
                            FinishDirectoryWatching();

                            // ファイルウォッチャーの開始
                            StartDirectoryWatching(dir, "*.png");
                        }));

                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.ExecuteReadDirF = false;    // 読み込み処理終了
                        }));

                });
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ファイルの削除処理
        /// <summary>
        /// ファイルの削除処理
        /// </summary>
        public void DeleteFile()
        {
            try
            {
                // インデックスの確認
                int index = this.FileList.IndexOf(this.FileList.SelectedItem);

                // 存在する場合
                if (index >= 0)
                {
                    string path = this.FileList.SelectedItem.FilePath;  // 選択しているファイルパスの取得
                    this.FileList.SelectedItemDelete();                 // リストからファイルを削除
                    File.Delete(path);                                  // ファイルの実態を削除

                    // 選択位置を確認（リスト上にそのインデックスがあるのかの確認）
                    if (this.FileList.Count > index)
                    {
                        // フォーカスをあてる
                        this.FileList.SelectedItem = this.FileList.ElementAt(index);
                    }
                    else
                    {
                        // 最後の要素にフォーカスをあてる
                        this.FileList.SelectedLast();
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

    }
}
