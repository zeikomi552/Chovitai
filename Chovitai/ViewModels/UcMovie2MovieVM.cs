using Chovitai.Common;
using Chovitai.Models;
using Chovitai.Models.A1111;
using Chovitai.Models.Config;
using Microsoft.Win32;
using MVVMCore.Common.Utilities;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace Chovitai.ViewModels
{
    public class UcMovie2MovieVM : WebUIBaseM
    {
        #region ファイルパス[FilePath]プロパティ
        /// <summary>
        /// ファイルパス[FilePath]プロパティ用変数
        /// </summary>
        string _FilePath = string.Empty;
        /// <summary>
        /// ファイルパス[FilePath]プロパティ
        /// </summary>
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (_FilePath == null || !_FilePath.Equals(value))
                {
                    _FilePath = value;
                    NotifyPropertyChanged("FilePath");
                }
            }
        }
        #endregion

        #region ファイルリスト[FileList2]プロパティ
        /// <summary>
        /// ファイルリスト[FileList]プロパティ用変数
        /// </summary>
        ModelList<FileInfoM> _FileList2 = new ModelList<FileInfoM>();
        /// <summary>
        /// ファイルリスト[FileList]プロパティ
        /// </summary>
        public ModelList<FileInfoM> FileList2
        {
            get
            {
                return _FileList2;
            }
            set
            {
                if (_FileList2 == null || !_FileList2.Equals(value))
                {
                    _FileList2 = value;
                    NotifyPropertyChanged("FileList2");
                }
            }
        }
        #endregion

        #region A1111 Request[Request]プロパティ
        /// <summary>
        /// A1111 Request[Request]プロパティ用変数
        /// </summary>
        RequestM _Request = new RequestM();
        /// <summary>
        /// A1111 Request[Request]プロパティ
        /// </summary>
        public RequestM Request
        {
            get
            {
                return GblValues.Instance.Request;
            }
            set
            {
                if (GblValues.Instance.Request == null || !GblValues.Instance.Request.Equals(value))
                {
                    GblValues.Instance.Request = value;
                    NotifyPropertyChanged("Request");
                }
            }
        }
        #endregion


        #region ファイルウォッチャー
        /// <summary>
        /// ファイルウォッチャー
        /// </summary>
        protected System.IO.FileSystemWatcher? _Watcher2 = null;
        #endregion

        #region ファイルディレクトリの監視処理開始
        /// <summary>
        /// ファイルディレクトリの監視処理開始
        /// </summary>
        /// <param name="dir">ディレクトリパス</param>
        /// <param name="filePattern">監視対象のファイルパターン</param>
        public void StartDirectoryWatching2(string dir, string filePattern)
        {
            if (_Watcher2 != null) return;

            _Watcher2 = new System.IO.FileSystemWatcher();

            //監視するディレクトリを指定
            _Watcher2.Path = dir;
            //最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
            // 監視パラメータの設定
            _Watcher2.NotifyFilter = (NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName
                | NotifyFilters.Attributes
                | NotifyFilters.CreationTime
                | NotifyFilters.Size
                | NotifyFilters.LastAccess
                | NotifyFilters.Security);

            //すべてのファイルを監視
            _Watcher2.Filter = filePattern;
            //UIのスレッドにマーシャリングする
            //コンソールアプリケーションでの使用では必要ない
            //watcher.SynchronizingObject = this;

            //イベントハンドラの追加
            _Watcher2.Changed += new FileSystemEventHandler(watcher2_Changed);
            _Watcher2.Error += new ErrorEventHandler(watcher2_Error);
            _Watcher2.Deleted += new FileSystemEventHandler(watcher2_Changed);
            _Watcher2.Renamed += new RenamedEventHandler(watcher2_Renamed);

            //監視を開始する
            _Watcher2.EnableRaisingEvents = true;
            Console.WriteLine("監視を開始しました。");
        }
        #endregion

        #region ファイルウォッチャーの変更イベント
        /// <summary>
        /// ファイルウォッチャーの変更イベント
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void watcher2_Changed(System.Object source,
            System.IO.FileSystemEventArgs e)
        {
            try
            {
                // シャットダウンフラグの確認
                if (GblValues.ShutdownF)
                {
                    // シャットダウン中のためファイルウォッチャーを解放し抜ける
                    DisposeFileWatcher();
                    return;
                }

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
                                        try
                                        {
                                            var bfind = (from x in this.FileList2.Items
                                                         where x.FilePath == file_info.FilePath
                                                         select x).Any();

                                            if (!bfind)
                                            {
                                                this.FileList2.Items.Add(file_info);
                                            }
                                        }
                                        catch { }
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
                                        try
                                        {
                                            this.FileList2.Items.Add(file_info);
                                        }
                                        catch { }
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
                                try
                                {
                                    var tmp = (from x in this.FileList2.Items
                                               where x.FilePath.Equals(e.FullPath)
                                               select x).FirstOrDefault();

                                    if (tmp != null)
                                    {
                                        this.FileList.Items.Remove(tmp);
                                    }
                                }
                                catch { }
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
        public virtual void watcher2_Renamed(System.Object source,
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
                            var old_file = (from x in this.FileList2.Items
                                            where x.FilePath.Equals(ev.OldFullPath)
                                            select x).FirstOrDefault();

                            if (old_file != null)
                            {
                                this.FileList2.Items.Remove(old_file); // 名称変更前のファイルをリストから削除
                            }

                            this.FileList2.Items.Add(file_info); // ファイルの追加処理

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
        private void watcher2_Error(object sender, ErrorEventArgs e)
        {
            //tbMessage.Text += "Error" + e.GetException().Message + Environment.NewLine;
        }
        #endregion
        #region ディレクトリのファイル全て読み込み
        /// <summary>
        /// ディレクトリのファイル全て読み込み
        /// </summary>
        /// <param name="dir">ディレクトリパス</param>
        public void ReadDirectory2(string dir)
        {
            try
            {
                // シャットダウンフラグの確認
                if (GblValues.ShutdownF)
                {
                    // シャットダウン中のためファイルウォッチャーを解放し抜ける
                    DisposeFileWatcher();
                    return;
                }

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
                            try
                            {
                                this.ExecuteReadDirF = true;    // 読み込み処理実行
                            }
                            catch { }
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
                                    try
                                    {
                                        this.FileList.Items.Add(file_info);
                                    }
                                    catch { }
                                }));
                        }
                    }
                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            try
                            {
                                this.FileList.SelectedLast();

                                // ファイルウォッチャーをいったん終了
                                FinishDirectoryWatching2();

                                // ファイルウォッチャーの開始
                                StartDirectoryWatching2(dir, "*.png");
                            }
                            catch { }
                        }));

                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            try
                            {
                                this.ExecuteReadDirF = false;    // 読み込み処理終了
                            }
                            catch { }
                        }));

                });
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ファイルウォッチャーの終了
        /// <summary>
        /// ファイルウォッチャーの終了
        /// </summary>
        public void FinishDirectoryWatching2()
        {
            if (_Watcher2 != null)
            {
                //監視を終了
                _Watcher2.EnableRaisingEvents = false;
                _Watcher2.Dispose();
                _Watcher2 = null;
                Console.WriteLine("監視を終了しました。");
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

        public void FileOpen()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "MP4ファイル (*.mp4)|*.mp4";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    // 選択されたファイル名 (ファイルパス) をメッセージボックスに表示
                    this.FilePath = dialog.FileName;

                    CaptureFrame(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }

        }

        public string WorkDir { get; set; } = string.Empty;
        /// <summary>
        /// フレームの保存処理
        /// </summary>
        private void CaptureFrame(string filepath)
        {
            try
            {
                // 出力先フォルダが指定されていない場合は処理を抜ける
                if (GblValues.Instance.A1111Setting == null 
                    || string.IsNullOrEmpty(GblValues.Instance.A1111Setting.Item.MovieDirectoryPath))
                {
                    return;
                }

                var dir = GblValues.Instance.A1111Setting.Item.MovieDirectoryPath;

                if (File.Exists(filepath))
                {
                    // 拡張子なしのファイル名を取得しフォルダ名とする
                    string fileName = Path.GetFileNameWithoutExtension(filepath);
                    // カレントディレクトリの保持
                    this.WorkDir = Path.Combine(GblValues.Instance.A1111Setting!.Item.MovieDirectoryPath, fileName);

                    // ディレクトリを作成する
                    PathManager.CreateDirectory(this.WorkDir);

                    // カレントディレクトリにファイルをコピーする
                    File.Copy(filepath, Path.Combine(this.WorkDir, Path.GetFileName(filepath)));

                    // Frameフォルダを保持する
                    string frame_path = Path.Combine(GblValues.Instance.A1111Setting!.Item.MovieDirectoryPath, fileName, "Frame");

                    // Outputフォルダを保持する
                    string outut_path = Path.Combine(GblValues.Instance.A1111Setting!.Item.MovieDirectoryPath, fileName, "Output");


                    // ディレクトリを作成する
                    PathManager.CreateDirectory(frame_path);
                    // ディレクトリを作成する
                    PathManager.CreateDirectory(outut_path);

                    using (var capture = new VideoCapture(this.FilePath))
                    {
                        for (int i = 0; i < capture.FrameCount - 2; i++)
                        {
                            string out_file_path = Path.Combine(frame_path, $"{frame_path}\\Frame-{i:00000}.png");

                            if (i == 0)
                            {
                                this.Request.Img2ImgPrompt.InitImage = out_file_path;
                            }

                            var img = new Mat();
                            capture.PosFrames = i;

                            this.Request.Img2ImgPrompt.Width = capture.FrameWidth;
                            this.Request.Img2ImgPrompt.Height = capture.FrameHeight;

                            capture.Read(img);
                            BitmapConverter.ToBitmap(img).Save(out_file_path, ImageFormat.Png);
                        }
                        ReadDirectory(frame_path);
                        ReadDirectory2(outut_path);
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        public async void ExecutePromptSingle()
        {
            try
            {
                await ExecutePrompt();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        #region Promptの実行処理
        /// <summary>
        /// Promptの実行処理
        /// </summary>
        private async Task<bool> ExecutePrompt()
        {
            try
            {
                string movie_dir = Path.Combine(this.WorkDir, "Output");    // 出力先フォルダパスの作成
                string frame_dir = Path.Combine(this.WorkDir, "Frame");    // 出力先フォルダパスの作成

                // DirectoryInfoのインスタンスを生成する
                DirectoryInfo di = new DirectoryInfo(frame_dir);

                // ディレクトリ直下のすべてのファイル一覧を取得する
                FileInfo[] fiAlls = di.GetFiles();
                foreach (var fi in fiAlls)
                {
                    this.Request.Img2ImgPrompt.InitImage = fi.FullName;
                    var ret = await this.Request.PostRequest(this.A1111Config.URL, movie_dir, this.Request.Img2ImgPrompt);

                    // スレッドセーフの呼び出し
                    await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.FileList2.SelectedLast();       // 追加されたファイルを選択
                                                                 // プロンプト実行履歴
                                                                 // 最終実行プロンプトのセット
                                                                 //this.LastPromptConfig.LastPrompt = this.Request.PromptItem;
                                                                 //GblValues.Instance.LastPrompt!.SaveXML();   // 最終プロンプトの保存
                        }));
                }

                
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
                return false;
            }
        }
        #endregion
    }
}
