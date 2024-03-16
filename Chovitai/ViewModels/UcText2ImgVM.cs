using Chovitai.Common.Utilities;
using Chovitai.Common;
using Chovitai.Models.A1111;
using Chovitai.Views.UserControls;
using Chovitai.Views;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chovitai.Models.Config;
using System.Windows.Threading;
using System.Windows;

namespace Chovitai.ViewModels
{
    public class UcText2ImgVM : WebUIBaseM
    {
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

        #region お気に入りフォルダ選択中[SelectedFavoriteFolderF]プロパティ
        /// <summary>
        /// お気に入りフォルダ選択中[SelectedFavoriteFolderF]プロパティ用変数
        /// </summary>
        bool _SelectedFavoriteFolderF = false;
        /// <summary>
        /// お気に入りフォルダ選択中[SelectedFavoriteFolderF]プロパティ
        /// </summary>
        public bool SelectedFavoriteFolderF
        {
            get
            {
                return _SelectedFavoriteFolderF;
            }
            set
            {
                if (!_SelectedFavoriteFolderF.Equals(value))
                {
                    _SelectedFavoriteFolderF = value;
                    NotifyPropertyChanged("SelectedFavoriteFolderF");
                }
            }
        }
        #endregion

        #region プロンプトの実行処理[ExecutePrompt]プロパティ
        /// <summary>
        /// プロンプトの実行処理[ExecutePrompt]プロパティ用変数
        /// </summary>
        static bool _ExecutePromptF = false;
        /// <summary>
        /// プロンプトの実行処理[ExecutePrompt]プロパティ
        /// </summary>
        public bool ExecutePromptF
        {
            get
            {
                return _ExecutePromptF;
            }
            set
            {
                if (!_ExecutePromptF.Equals(value))
                {
                    _ExecutePromptF = value;
                    NotifyPropertyChanged("ExecutePrompt");
                }
            }
        }
        #endregion

        #region 初期化完了フラグ
        /// <summary>
        /// 初期化完了フラグ
        /// </summary>
        static bool bInit = false;
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
                if (!bInit)
                {
                    string dir = this.A1111Config.ImageOutDirectory;
                    if (Directory.Exists(dir))
                    {
                        // ディレクトリの読み込み処理
                        ReadDirectory(dir);

                        // ファイルウォッチャーをいったん終了
                        FinishDirectoryWatching();

                        // ファイルウォッチャーの開始
                        StartDirectoryWatching(dir, "*.png");
                    }

                    this.Request.PromptItem = this.LastPromptConfig.LastPrompt;

                    // WebUIの実行
                    WebUIExecute();

                    bInit = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 最終プロンプト
        /// <summary>
        /// 最終プロンプト
        /// </summary>
        public LastPromptConfigM LastPromptConfig
        {
            get
            {
                return GblValues.Instance.LastPrompt!.Item;
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
                var wnd = VisualTreeHelperWrapper.GetWindow<UcText2ImgV>(sender) as UcText2ImgV;

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

        #region 設定画面を開く処理
        /// <summary>
        /// 設定画面を開く処理
        /// </summary>
        public void OpenSetting()
        {
            try
            {
                A1111SettingV wnd = new A1111SettingV();
                var vm = wnd.DataContext as A1111SettingVM;

                string dir = this.A1111Config.ImageOutDirectory;
                if (wnd.ShowDialog() == true)
                {
                    GblValues.Instance.A1111Setting!.LoadXML();

                    if (!dir.Equals(this.A1111Config.ImageOutDirectory))
                    {
                        ReadDirectory(this.A1111Config.ImageOutDirectory);

                        // ファイルウォッチャーをいったん終了
                        FinishDirectoryWatching();

                        // ファイルウォッチャーの開始
                        StartDirectoryWatching(dir, "*.png");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        public async void GetModels()
        {
            try
            {
                var ret2 = await this.Request.GetModels(this.A1111Config.URL);
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        #region お気に入りフォルダに切り替える
        /// <summary>
        /// お気に入りフォルダに切り替える
        /// </summary>
        public void ChangeFolder()
        {
            try
            {
                if (this.SelectedFavoriteFolderF)
                {
                    ReadDirectory(this.A1111Config.FavoriteDirectory);
                }
                else
                {
                    ReadDirectory(this.A1111Config.ImageOutDirectory);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        Random _Rand = new Random();

        #region Promptの実行処理
        /// <summary>
        /// Promptの実行処理
        /// </summary>
        public async void ClickPromptStartRepeat()
        {
            try
            {
                while (this.ExecutePromptF)
                {
                    await ExecutePrompt();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region Promptの実行処理
        /// <summary>
        /// Promptの実行処理
        /// </summary>
        public async void ClickPromptStart()
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
        #endregion

        #region Promptの実行処理
        /// <summary>
        /// Promptの実行処理
        /// </summary>
        private async Task<bool> ExecutePrompt()
        {
            try
            {
                var ret = await this.Request.PostRequest(this.A1111Config.URL, this.A1111Config.ImageOutDirectory, this.Request.PromptItem);

                // スレッドセーフの呼び出し
                await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() =>
                    {
                        this.FileList.SelectedLast();       // 追加されたファイルを選択
                                                            // プロンプト実行履歴
                                                            // 最終実行プロンプトのセット
                        this.LastPromptConfig.LastPrompt = this.Request.PromptItem;
                        GblValues.Instance.LastPrompt!.SaveXML();   // 最終プロンプトの保存
                    }));
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
                return false;
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
        public override void watcher_Changed(System.Object source,
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

                base.watcher_Changed(source, e);
                switch (e.ChangeType)
                {
                    case System.IO.WatcherChangeTypes.Changed:
                        {
                            base.watcher_Changed(source, e);

                            ////Debug.WriteLine("test-rename");
                            var file_info = GetFileInfo(e.FullPath);
                            if (file_info != null)
                            {
                                // スレッドセーフの呼び出し
                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                    new Action(() =>
                                    {
                                        this.FileList.SelectedLast();       // 追加されたファイルを選択
                                    }));
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region Commandへセット
        /// <summary>
        /// Commandへセット
        /// </summary>
        public void SetCommand()
        {
            try
            {
                this.Request.PromptItem
                    = Text2ImagePromptM.CreateCommandFromImageText(this.FileList.SelectedItem.ImageText);
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region お気に入りフォルダへ移動
        /// <summary>
        /// お気に入りフォルダへ移動
        /// </summary>
        public void MoveFavorite()
        {
            try
            {
                string dir = this.A1111Config.FavoriteDirectory;     // ファイルディレクトリ
                string filename = System.IO.Path.GetFileName(this.FileList.SelectedItem.FilePath);      // ファイル名

                // フォルダの作成
                PathManager.CreateDirectory(this.A1111Config.FavoriteDirectory);

                // ファイルの移動処理
                File.Move(this.FileList.SelectedItem.FilePath, Path.Combine(dir, filename));
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region -1をセットする
        /// <summary>
        /// -1をセットする
        /// </summary>
        public void RandomSet()
        {
            try
            {
                this.Request.PromptItem.Seed = -1;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region プロンプトの高さと幅を入れ替える
        /// <summary>
        /// プロンプトの高さと幅を入れ替える
        /// </summary>
        public void ChangeWH()
        {
            try
            {
                var tmp = this.Request.PromptItem.Width;
                this.Request.PromptItem.Width = this.Request.PromptItem.Height;
                Request.PromptItem.Height = tmp;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region プロンプトの実行処理停止
        /// <summary>
        /// プロンプトの実行処理停止
        /// </summary>
        public static void StopPrompt()
        {
            _ExecutePromptF = false;
        }
        #endregion
    }
}
