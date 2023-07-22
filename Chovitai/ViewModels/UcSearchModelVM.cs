using Chovitai.Common.Enums;
using Chovitai.Common.Utilities;
using Chovitai.Common;
using Chovitai.Models.Condition;
using Chovitai.Models;
using Chovitai.Views.UserControls;
using Chovitai.Views;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsModel.CvsModelM.CvsModelVersions;
using static Chovitai.Models.CvsModel.CvsModelM;
using System.Windows;
using Chovitai.Models.CvsModel;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using System.Diagnostics;
using Chovitai.Models.Config;
using Chovitai.Models.Bookmark;
using Chovitai.Common.Commands;

namespace Chovitai.ViewModels
{
    public class UcSearchModelVM : ViewModelBase
    {
        #region イメージリスト[ImageList]プロパティ
        /// <summary>
        /// イメージリスト[ImageList]プロパティ用変数
        /// </summary>
        DisplayImageM _ImageList = new DisplayImageM();
        /// <summary>
        /// イメージリスト[ImageList]プロパティ
        /// </summary>
        public DisplayImageM ImageList
        {
            get
            {
                return _ImageList;
            }
            set
            {
                if (_ImageList == null || !_ImageList.Equals(value))
                {
                    _ImageList = value;
                    NotifyPropertyChanged("ImageList");
                }
            }
        }
        #endregion

        #region API実行中フラグ(true:実行中 false:実行中でない)[ExecuteGetAPI]プロパティ
        /// <summary>
        /// API実行中フラグ(true:実行中 false:実行中でない)[ExecuteGetAPI]プロパティ用変数
        /// </summary>
        bool _ExecuteGetAPI = false;
        /// <summary>
        /// API実行中フラグ(true:実行中 false:実行中でない)[ExecuteGetAPI]プロパティ
        /// </summary>
        public bool ExecuteGetAPI
        {
            get
            {
                return _ExecuteGetAPI;
            }
            set
            {
                if (!_ExecuteGetAPI.Equals(value))
                {
                    _ExecuteGetAPI = value;
                    NotifyPropertyChanged("ExecuteGetAPI");
                }
            }
        }
        #endregion

        #region stablediffusion model object[CvsModel]プロパティ
        /// <summary>
        /// stablediffusion model object[CvsModel]プロパティ用変数
        /// </summary>
        CvsModelExM? _CvsModel = null;
        /// <summary>
        /// stablediffusion model object[CvsModel]プロパティ
        /// </summary>
        public CvsModelExM? CvsModel
        {
            get
            {
                return _CvsModel;
            }
            set
            {
                if (_CvsModel == null || !_CvsModel.Equals(value))
                {
                    _CvsModel = value;
                    NotifyPropertyChanged("CvsModel");
                }
            }
        }
        #endregion

        #region Configファイルオブジェクト[Config]プロパティ
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ
        /// </summary>
        public ConfigManager<ConfigM>? Config
        {
            get
            {
                return GblValues.Instance.Config;
            }
            set
            {
                if (GblValues.Instance.Config == null || !GblValues.Instance.Config.Equals(value))
                {
                    GblValues.Instance.Config = value;
                    NotifyPropertyChanged("Config");
                }
            }
        }
        #endregion

        #region GET Query Condtion[GetCondition]プロパティ
        /// <summary>
        /// GET Query Condtion[GetCondition]プロパティ用変数
        /// </summary>
        CvsModelGetConditionM _GetCondition = new CvsModelGetConditionM();
        /// <summary>
        /// GET Query Condtion[GetCondition]プロパティ
        /// </summary>
        public CvsModelGetConditionM GetCondition
        {
            get
            {
                return _GetCondition;
            }
            set
            {
                if (_GetCondition == null || !_GetCondition.Equals(value))
                {
                    _GetCondition = value;
                    NotifyPropertyChanged("GetCondition");
                }
            }
        }
        #endregion

        #region Current page[CurrentPage]プロパティ
        /// <summary>
        /// Current page[CurrentPage]プロパティ用変数
        /// </summary>
        int _CurrentPage = 0;
        /// <summary>
        /// Current page[CurrentPage]プロパティ
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                if (!_CurrentPage.Equals(value))
                {
                    _CurrentPage = value;
                    NotifyPropertyChanged("CurrentPage");
                }
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UcSearchModelVM()
        {
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void Output()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null)
                {
                    // マークダウン出力1
                    this.CvsModel.MarkdownOutput1();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void Output2()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null)
                {
                    // マークダウン出力2
                    this.CvsModel.MarkdownOutput2();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region Execute GET REST API
        /// <summary>
        /// Execute GET REST API
        /// </summary>
        public void GETQuery(object sender, EventArgs ev)
        {
            try
            {
                // GET クエリの実行
                GETQuery(sender, this.GetCondition.GetConditionQuery);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region GETクエリの実行処理
        /// <summary>
        /// GETクエリの実行処理
        /// </summary>
        /// <param name="query"></param>
        /// <param name="add_endpoint"></param>
        private async void GETQuery(object sender, string query, bool add_endpoint = true)
        {
            try
            {
                this.ExecuteGetAPI = true;
                GetModelReqestM tmp = new GetModelReqestM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = string.Empty;
                if (add_endpoint)
                {
                    // エンドポイント + パラメータ
                    url = CvsModelM.Endpoint + query;
                }
                else
                {
                    // エンドポイント + パラメータ
                    url = query;
                }

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<CvsModelM>(request = await tmp.Request(url));

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsModel = new CvsModelExM(request_model); // ModelListへ変換
                    this.CvsModel!.Rowdata = request;               // 生データの保持
                    this.CvsModel!.RequestURL = url;               // 生データの保持

                    // 1つ以上要素が存在する場合
                    if (this.CvsModel.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsModel.Items.SelectedItem = this.CvsModel.Items.ElementAt(0);

                        // DataGridを先頭へスクロールさせる
                        DataGridTopRow(sender);

                        if (this.CvsModel.Items.SelectedItem.ModelVersions.Count > 0)
                        {
                            // 1つ目の要素をセットする
                            CvsModel.Items.SelectedItem.SelectedModelVersion = this.CvsModel.Items.SelectedItem.ModelVersions.ElementAt(0);
                        }
                    }

                    // 画面とブックマークを合致させる
                    ModelBookmarkM.AdjustBookmark(this.CvsModel.Items);
                }
            }
            catch (JSONDeserializeException e)
            {
                string msg = e.Message + "\r\n" + e.JSON;
                ShowMessage.ShowErrorOK(msg, "Error");
            }
            finally
            {
                this.ExecuteGetAPI = false;
            }
        }
        #endregion

        #region モデルの選択が変更された場合の処理
        /// <summary>
        /// モデルの選択が変更された場合の処理
        /// </summary>
        public void ModelSelectionChanged(object sender, EventArgs ev)
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null)
                {
                    if (this.CvsModel.Items.SelectedItem.ModelVersions.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        CvsModel.Items.SelectedItem.SelectedModelVersion = this.CvsModel.Items.SelectedItem.ModelVersions.ElementAt(0);
                    }
                    ////// 最初の行を選択する
                    //this.ImageList.SetFirst();

                    ////// ImageリストのListViewを先頭へスクロールさせる
                    //ListViewTopRow(sender);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region モデルの選択が変更された場合の処理
        /// <summary>
        /// モデルの選択が変更された場合の処理
        /// </summary>
        public void ModelVersionSelectionChanged(object sender, EventArgs ev)
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null && this.CvsModel.Items != null
                    && this.CvsModel.Items.SelectedItem != null
                    && this.CvsModel.Items.SelectedItem.SelectedModelVersion != null)
                {
                    // 対象行をセット
                    this.ImageList.SetImages(this.CvsModel.Items.SelectedItem.SelectedModelVersion.Images);

                    // 最初の行を選択する
                    this.ImageList.SetFirst();

                    // ImageリストのListViewを先頭へスクロールさせる
                    ListViewTopRow(sender);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ListViewを先頭へスクロールさせる処理
        /// <summary>
        /// DataGridを先頭へスクロールさせる処理
        /// </summary>
        /// <param name="sender">画面内のコントロールオブジェクト</param>
        private void ListViewTopRow(object sender)
        {
            // ウィンドウの取得
            var wnd = (UcSearchModelV)VisualTreeHelperWrapper.GetWindow<UcSearchModelV>(sender);

            // イメージのListViewのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4ListView(wnd.lvImages);
        }
        #endregion

        #region モデル行のダブルクリック
        /// <summary>
        /// モデル行のダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenURL(object sender, EventArgs e)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<MainWindow>(sender) as MainWindow;

                // nullチェック
                if (wnd != null && this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null)
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo($"https://civitai.com/models/{this.CvsModel!.Items.SelectedItem.Id}");
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region モデル行のダブルクリック
        /// <summary>
        /// モデル行のダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenURLModelVersion(object sender, EventArgs e)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<MainWindow>(sender) as MainWindow;

                // nullチェック
                if (wnd != null && this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null && this.CvsModel!.Items.SelectedItem.SelectedModelVersion != null)
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo($"https://civitai.com/models/{this.CvsModel!.Items.SelectedItem.Id}" + $"?modelVersionId={this.CvsModel!.Items.SelectedItem.SelectedModelVersion.Id}");
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 次のページへ移動
        /// <summary>
        /// 次のページへ移動
        /// </summary>
        public void MoveNext(object sender, EventArgs ev)
        {
            try
            {
                // null check
                if (this.CvsModel != null)
                {
                    // 次のページが最終ページより前である場合
                    if (this.CvsModel.Metadata.CurrentPage + 1 <= CvsModel.Metadata.TotalPages)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsModel.Metadata.NextPage, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 前のページへ移動
        /// <summary>
        /// 前のページへ移動
        /// </summary>
        public void MovePrev(object sender, EventArgs ev)
        {
            try
            {
                // null check
                if (this.CvsModel != null)
                {
                    // 前のページが1より大きい場合
                    if (this.CvsModel.Metadata.CurrentPage - 1 >= 1)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsModel.Metadata.PrevPage, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region DataGridを先頭へスクロールさせる処理
        /// <summary>
        /// DataGridを先頭へスクロールさせる処理
        /// </summary>
        /// <param name="sender">画面内のコントロールオブジェクト</param>
        private void DataGridTopRow(object sender)
        {
            // ウィンドウの取得
            var wnd = (UcSearchModelV)VisualTreeHelperWrapper.GetWindow<UcSearchModelV>(sender);

            // モデルのDataGridのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4DataGrid(wnd.dgModel);

            // モデルバージョンのDataGridのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4DataGrid(wnd.dgModelVersions);
        }
        #endregion

        #region ファイルのプロンプト情報抜き出し
        /// <summary>
        /// ファイルのプロンプト情報抜き出し
        /// </summary>
        public void ImageFileRead()
        {

            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "画像ファイル (*.png)|*.png|全てのファイル (*.*)|*.*";

                string message = string.Empty;
                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {

                    using (var reader = new BinaryReader(File.Open(dialog.FileName, FileMode.Open, FileAccess.Read)))
                    {
                        if (PngReader.ReadPngSignature(reader))
                        {
                            var ihdrchunk = PngReader.ReadChunk(reader);
                            var itextchunk = PngReader.ReadChunk(reader);

                            // データがutf - 8の場合
                            message = System.Text.Encoding.UTF8.GetString(itextchunk.ChunkData).Replace("\0", ":");
                        }
                    }

                }

                if (!string.IsNullOrEmpty(message))
                {
                    ShowMessage.ShowNoticeOK(message, "Notice");
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region JSONリクエストのコピー
        /// <summary>
        /// JSONリクエストのコピー
        /// </summary>
        public void CopyRequest()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null)
                {
                    // クリップボードにコピー
                    Clipboard.SetText(this.CvsModel.RequestURL);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region タグの検索画面へ移動処理
        /// <summary>
        /// タグの検索画面へ移動処理
        /// </summary>
        public void SearchTag()
        {
            try
            {
                var wnd = new SearchTagV();
                var vm = wnd.DataContext as SearchTagVM;

                if (wnd.ShowDialog() == true)
                {
                    this.GetCondition.Tag = vm!.SelectedTagItem.Name;
                }

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region クリエイター検索画面へ移動処理
        /// <summary>
        /// クリエイター検索画面へ移動処理
        /// </summary>
        public void SearchCreator()
        {
            try
            {
                var wnd = new SearchCreatorV();
                var vm = wnd.DataContext as SearchCreatorVM;

                if (wnd.ShowDialog() == true)
                {
                    this.GetCondition.Username = vm!.SelectedTagItem.Username;
                }

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region Promptリスト作成画面へ移動処理
        /// <summary>
        /// Promptリスト作成画面へ移動処理
        /// </summary>
        public void CreatePromptList()
        {
            try
            {
                if (this.CvsModel != null)
                {
                    var tmp = new PromptCountCollectionM();
                    tmp.InitItems(this.CvsModel);

                    var wnd = new PromptWindowV();
                    var vm = wnd.DataContext as PromptWindowVM;
                    vm!.PromptItems = tmp;

                    if (wnd.ShowDialog() == true)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
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
                if (this.CvsModel != null)
                {
                    // 画面とブックマークを合致させる
                    ModelBookmarkM.AdjustBookmark(this.CvsModel.Items);
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
            try
            {

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

    }
}
