using Chovitai.Common.Utilities;
using Chovitai.Models;
using Chovitai.Views.UserControls;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using SharpVectors.Renderers.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chovitai.ViewModels
{
    public class UcFileCheckVM : ViewModelBase
    {
        #region 新しいファイルができたらそのファイルを選択するフラグ[SelectNewFolderF]プロパティ
        /// <summary>
        /// 新しいファイルができたらそのファイルを選択するフラグ[SelectNewFolderF]プロパティ用変数
        /// </summary>
        bool _SelectNewFolderF = false;
        /// <summary>
        /// 新しいファイルができたらそのファイルを選択するフラグ[SelectNewFolderF]プロパティ
        /// </summary>
        public bool SelectNewFolderF
        {
            get
            {
                return _SelectNewFolderF;
            }
            set
            {
                if (!_SelectNewFolderF.Equals(value))
                {
                    _SelectNewFolderF = value;
                    NotifyPropertyChanged("SelectNewFolderF");
                }
            }
        }
        #endregion

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

        #region 読み込んだディレクトリ[DirectoryPath]プロパティ
        /// <summary>
        /// 読み込んだディレクトリ[DirectoryPath]プロパティ用変数
        /// </summary>
        string _DirectoryPath = string.Empty;
        /// <summary>
        /// 読み込んだディレクトリ[DirectoryPath]プロパティ
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return _DirectoryPath;
            }
            set
            {
                if (_DirectoryPath == null || !_DirectoryPath.Equals(value))
                {
                    _DirectoryPath = value;
                    NotifyPropertyChanged("DirectoryPath");
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

        #region スライドショーフラグ[IsSlideshow]プロパティ
        /// <summary>
        /// スライドショーフラグ[IsSlideshow]プロパティ用変数
        /// </summary>
        bool _IsSlideshow = false;
        /// <summary>
        /// スライドショーフラグ[IsSlideshow]プロパティ
        /// </summary>
        public bool IsSlideshow
        {
            get
            {
                return _IsSlideshow;
            }
            set
            {
                if (!_IsSlideshow.Equals(value))
                {
                    _IsSlideshow = value;
                    NotifyPropertyChanged("IsSlideshow");
                }
            }
        }
        #endregion

        #region ファイルウォッチャー
        /// <summary>
        /// ファイルウォッチャー
        /// </summary>
        private System.IO.FileSystemWatcher? _Watcher = null;
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

        #region ファイルウォッチャーの終了
        /// <summary>
        /// ファイルウォッチャーの終了
        /// </summary>
        private void FinishDirectoryWatching()
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

        #region 選択行が変化した際の処理
        /// <summary>
        /// 選択行が変化した際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectedItemChanged(object sender, EventArgs e)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<UcFileCheckV>(sender) as UcFileCheckV;

                // ウィンドウが取得できた場合
                if (wnd != null && FileList.SelectedItem != null)
                {
                    ScrollbarUtility.TopRow(wnd.lvImages);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ファイルウォッチャーの変更イベント
        /// <summary>
        /// ファイルウォッチャーの変更イベント
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void watcher_Changed(System.Object source,
            System.IO.FileSystemEventArgs e)
        {
            Debug.WriteLine("test-changed");

            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed:
                    Console.WriteLine(
                        "ファイル 「" + e.FullPath + "」が変更されました。");
                    break;
                case System.IO.WatcherChangeTypes.Created:
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
        #endregion

        #region ファイル名が変更された際の処理
        /// <summary>
        /// ファイル名が変更された際の処理
        /// StabledDiffusionで生成する場合は*.tmp → *.pngに変更されるためこの処理に入る
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ev"></param>
        private void watcher_Renamed(System.Object source,
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
                            this.FileList.Items.Add(file_info); // ファイルの追加処理

                            if(this.SelectNewFolderF)
                            {
                                this.FileList.SelectedLast();       // 追加されたファイルを選択
                            }
                        }));
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion
        public void CreateMovie()
        {
            try
            {
                //// ダイアログのインスタンスを生成
                //var dialog = new SaveFileDialog();

                //// ファイルの種類を設定
                //dialog.Filter = "動画ファイル (*.mp4)|*.mp4";

                //// ダイアログを表示する
                //if (dialog.ShowDialog() == true)
                //{
                //    var outdir = Path.Combine(this.Parameter.Outdir, "Sample");
                //    var Converter = new ImageConverter();
                //    //H264を使う場合、openh264-*.dllが必要。このdllをソフトウェアと同一のフォルダに入れる。
                //    using (var Writer = new OpenCvSharp.VideoWriter(dialog.FileName, OpenCvSharp.FourCC.H264, 20, new OpenCvSharp.Size(this.Parameter.Width, this.Parameter.Height)))
                //    {
                //        // ディレクトリパス
                //        string path = this.Parameter.Outdir;

                //        // サンプルフォルダ配下
                //        path = Path.Combine(path, "samples");

                //        PathManager.CreateDirectory(path);

                //        // DirectoryInfoのインスタンスを生成する
                //        DirectoryInfo di = new DirectoryInfo(path);

                //        // ディレクトリ直下のすべてのファイル一覧を取得する
                //        FileInfo[] fiAlls = di.GetFiles("*.png");

                //        foreach (var file in fiAlls)
                //        {
                //            var image = OpenCvSharp.Mat.FromImageData((byte[])Converter.ConvertTo(Image.FromFile(file.FullName), typeof(byte[]))!);
                //            Writer.Write(image);
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        #region スライドショーの切替
        /// <summary>
        /// スライドショーの切替
        /// </summary>
        public void Slideshow()
        {
            Task.Run(() =>
            {
                // 画像が1つ以上あるかを確認
                if (this.FileList.Count <= 0)
                {
                    this.IsSlideshow = false;
                    return;
                }

                // nullチェック
                if (this.FileList.SelectedItem == null)
                {
                    this.FileList.SelectedItem = this.FileList.First();
                }

                // 音声再生インデックス取得
                int index = this.FileList.IndexOf(this.FileList.SelectedItem);

                while (this.IsSlideshow)
                {
                    if (this.FileList.Count <= 0)
                    {
                        this.IsSlideshow = false;
                        return;
                    }

                    if (index < this.FileList.Count)
                    {
                        var tmp = this.FileList.ElementAt(index);

                        this.FileList.SelectedItem = tmp;
                        System.Threading.Thread.Sleep(3000);
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
            });
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

        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Init(object sender, EventArgs ev)
        {
            try
            {

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Close(object sender, EventArgs ev)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<MainWindow>(sender) as MainWindow;

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ディレクトリ内のpngファイルを全て読み込む
        /// <summary>
        /// ディレクトリ内のpngファイルを全て読み込む
        /// </summary>
        public void ReadDirectory()
        {
            try
            {
                // ディレクトリを開く
                if (OpenDirectory())
                {
                    // ディレクトリ内のpngファイルの読み込み
                    ReadDirectory(this.DirectoryPath);
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

        #region ディレクトリ読み出しの更新処理
        /// <summary>
        /// ディレクトリ読み出しの更新処理
        /// </summary>
        public void RenewDirectory()
        {
            try
            {
                // ディレクトリを開く
                if (Directory.Exists(this.DirectoryPath))
                {
                    // ディレクトリ内のpngファイルの読み込み
                    ReadDirectory(this.DirectoryPath);
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

        #region ディレクトリを開く処理
        /// <summary>
        /// ディレクトリを開く処理
        /// </summary>
        private bool OpenDirectory()
        {
            using (var cofd = new CommonOpenFileDialog()
            {
                Title = "フォルダを選択してください",
                // フォルダ選択モードにする
                IsFolderPicker = true,
            })
            {
                // フォルダ選択ダイアログを開く
                if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    this.DirectoryPath = cofd.FileName; // フォルダパスのセット
                    return true;
                }
            }
            return false;
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
                File.Delete(this.FileList.SelectedItem.FilePath);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ファイル情報の読み取り処理
        /// <summary>
        /// ファイル情報の読み取り処理
        /// </summary>
        /// <param name="file">ファイルパス</param>
        /// <returns>ファイル情報</returns>
        private FileInfoM? GetFileInfo(string file)
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

        #region ディレクトリのファイル全て読み込み
        /// <summary>
        /// ディレクトリのファイル全て読み込み
        /// </summary>
        /// <param name="dir">ディレクトリパス</param>
        private void ReadDirectory(string dir)
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

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void OutputMarkdown()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "マークダウン (*.md)|*.md";

                string img_dir = string.Empty;
                string mk_filename = string.Empty;
                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    MarkdownM.OutputMarkdown(this.FileList.Items.ToList<FileInfoM>(), this.DirectoryPath, dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region キー入力処理の受付
        /// <summary>
        /// キー入力処理の受付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDown(object sender, EventArgs e)
        {
            try
            {
                var wnd = sender as UcFileCheckV;

                if (wnd != null)
                {
                    var key_eve = e as KeyEventArgs;

                    if (key_eve!.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || key_eve!.KeyboardDevice.IsKeyDown(Key.RightCtrl))
                    {
                        switch (key_eve.Key)
                        {
                            case Key.Left:
                                {

                                    break;
                                }
                            case Key.Right:
                                {

                                    break;
                                }

                        }
                    }

                    switch (key_eve.Key)
                    {
                        case Key.Enter:
                            {

                                break;
                            }
                        case Key.F5:
                            {
                                if (!this.ExecuteReadDirF)
                                {
                                    // フォルダの更新
                                    RenewDirectory();
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception ev)
            {
                ShowMessage.ShowErrorOK(ev.Message, "Error");
            }
        }
        #endregion
    }
}
