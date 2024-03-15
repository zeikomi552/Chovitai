using Chovitai.Common.Enums;
using Chovitai.Common;
using Chovitai.Models;
using Chovitai.Models.Condition;
using Chovitai.Models.CvsImage;
using Chovitai.Models.CvsModel;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayImageM = Chovitai.Models.CvsImage.DisplayImageM;
using static Chovitai.Models.CvsModel.CvsModelM.CvsModelVersions;
using System.Collections.ObjectModel;
using static Chovitai.Models.CvsImage.CvsImageM;
using System.Windows;
using Chovitai.Views;
using Chovitai.Models.Bookmark;
using System.ComponentModel;
using System.Reflection;
using Chovitai.Models.A1111;

namespace Chovitai.ViewModels
{
    public class UcSearchImageVM : ViewModelBase
    {

        #region 検索条件[SearchCondition]プロパティ
        /// <summary>
        /// 検索条件[SearchCondition]プロパティ
        /// </summary>
        public CvsImageGetConditionM SearchCondition
        {
            get
            {
                return GblValues.Instance.ImageSearchCondition;
            }
            set
            {
                if (GblValues.Instance.ImageSearchCondition == null || !GblValues.Instance.ImageSearchCondition.Equals(value))
                {
                    GblValues.Instance.ImageSearchCondition = value;
                    NotifyPropertyChanged("SearchCondition");
                }
            }
        }
        #endregion

        #region イメージモデル[CvsImage]プロパティ
        /// <summary>
        /// イメージモデル[CvsImage]プロパティ用変数
        /// </summary>
        CvsImageExM? _CvsImage = null;
        /// <summary>
        /// イメージモデル[CvsImage]プロパティ
        /// </summary>
        public CvsImageExM? CvsImage
        {
            get
            {
                return _CvsImage;
            }
            set
            {
                if (_CvsImage == null || !_CvsImage.Equals(value))
                {
                    _CvsImage = value;
                    NotifyPropertyChanged("CvsImage");
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

        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
            try
            {
                if (this.CvsImage != null)
                {
                    // 画面とブックマークを合致させる
                    ImageBookmarkM.AdjustBookmark(this.CvsImage.Items);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

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


        #region Execute GET REST API
        /// <summary>
        /// Execute GET REST API
        /// </summary>
        public void Search()
        {
            try
            {
                // カーソル情報のクリア
                this.SearchCondition.CursorClear();

                // GET クエリの実行
                GETQuery(this.SearchCondition.GetConditionQuery);

                // nullチェック
                if (this.CvsImage != null)
                {
                    // カーソルの追加
                    this.SearchCondition.AddCursor(this.CvsImage);
                }
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
        private async void GETQuery(string query, bool add_endpoint = true)
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
                    url = CvsImageM.Endpoint + query;
                }
                else
                {
                    // エンドポイント + パラメータ
                    url = query;
                }

                // 実行してJSON形式をデシリアライズ
                //var request_model = JSONUtil.DeserializeFromText<CvsImageM>(request = await tmp.Request(url));
                var request_tmp = await tmp.Request(url);
                var request_model = JSONUtil.DeserializeFromText<CvsImageM>(request_tmp);

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsImage = new CvsImageExM(request_model); // ModelListへ変換
                    this.CvsImage!.Rowdata = request;               // 生データの保持
                    this.CvsImage!.RequestURL = url;               // 生データの保持

                    // 1つ以上要素が存在する場合
                    if (this.CvsImage.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsImage.Items.SelectedItem = this.CvsImage.Items.ElementAt(0);
                    }

                    // 画面とブックマークを合致させる
                    ImageBookmarkM.AdjustBookmark(this.CvsImage.Items);

                    ImageChanged();
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

        #region 次のページへ移動
        /// <summary>
        /// 次のページへ移動
        /// </summary>
        public void MoveNext(object sender, EventArgs ev)
        {
            try
            {
                // null check
                if (this.CvsImage != null)
                {
                    // 次のページが最終ページより前である場合
                    if (!string.IsNullOrEmpty(this.CvsImage.Metadata.NextPage))
                    {
                        // Execute GET Query
                        GETQuery(this.CvsImage.Metadata.NextPage, false);

                        // nullチェック
                        if (this.CvsImage != null)
                        {
                            // カーソルの追加
                            this.SearchCondition.AddCursor(this.CvsImage);
                        }
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
                if (this.CvsImage != null)
                {
                    // 前のページが1より大きい場合
                    if (this.SearchCondition.CursorList.Count > 1)
                    {
                        this.SearchCondition.RemoveLastCursor();
                        // Execute GET Query
                        GETQuery(this.SearchCondition.CursorList.LastOrDefault().Value, false);
                    }
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
        public void ImageChanged()
        {
            try
            {
                // nullチェック
                if (this.CvsImage != null && this.CvsImage.Items != null && this.CvsImage.Items.SelectedItem != null)
                {
                    // イメージをセットする
                    this.ImageList.SetImages(new ObservableCollection<CvsItem>(this.CvsImage.Items.Items));

                    // 最初の行を選択する
                    this.ImageList.SetFirst();

                    // ImageリストのListViewを先頭へスクロールさせる
                    //ListViewTopRow(sender);
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
                if (this.CvsImage != null)
                {
                    var tmp = new PromptCountCollectionM();
                    tmp.InitItems(this.CvsImage);

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

        #region プロンプトツールの起動
        /// <summary>
        /// プロンプトツールの起動
        /// </summary>
        public void OpenPromptToolSingle()
        {
            try
            {
                if (this.CvsImage != null)
                {
                    var tmp = new PromptCountCollectionM();
                    var list_items = new List<CvsItem>();
                    tmp.InitItems(this.CvsImage.Items.SelectedItem);

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

        #region JSONリクエストのコピー
        /// <summary>
        /// JSONリクエストのコピー
        /// </summary>
        public void CopyRequest()
        {
            try
            {
                // nullチェック
                if (this.CvsImage != null)
                {
                    // クリップボードにコピー
                    Clipboard.SetText(this.CvsImage.RequestURL);
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

                // nullチェック
                if (vm != null)
                {
                    vm.SelectedTagItem.Username = this.SearchCondition.Username!;
                    if (wnd.ShowDialog() == true)
                    {
                        this.SearchCondition.Username = vm!.SelectedTagItem.Username;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 一気にブックマークする
        /// <summary>
        /// 一気にブックマークする
        /// </summary>
        public void AllBookmark()
        {
            try
            {
                if (this.CvsImage != null)
                {
                    foreach (var img in this.CvsImage.Items)
                    {
                        img.IsBookmark = true;
                        img.ChangeBookmark();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region コマンドのセット処理
        /// <summary>
        /// コマンドのセット処理
        /// </summary>
        public void SetCommand()
        {
            try
            {
                if (this.ImageList != null && this.ImageList.SelectedImage != null && this.ImageList.SelectedImage.Meta != null)
                {
                    GblValues.Instance.Request.PromptItem.Prompt = this.ImageList.SelectedImage.Meta.Prompt;                // プロンプトのセット
                    GblValues.Instance.Request.PromptItem.NegativePrompt = ImageList.SelectedImage.Meta.NegativePrompt;     // ネガティブプロンプトのセット
                    long tmp_seed = -1;
                    GblValues.Instance.Request.PromptItem.Seed = long.TryParse( ImageList.SelectedImage.Meta.Seed!.ToString(), out tmp_seed) ? tmp_seed : -1;     // シードのセット

                    int w, h;
                    Text2ImagePromptM.SizeToWH(ImageList.SelectedImage.Meta.Size, out w, out h);
                    GblValues.Instance.Request.PromptItem.Width = w;
                    GblValues.Instance.Request.PromptItem.Height = h;

                    // Samplerのセット
                    foreach (SamplerIndexEnum value in Enum.GetValues(typeof(SamplerIndexEnum)))
                    {
                        FieldInfo fieldInfo = value.GetType().GetField(value.ToString())!;
                        Attribute attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute))!;
                        if (attr != null)
                        {
                            DescriptionAttribute descAttr = (DescriptionAttribute)attr;

                            if (ImageList.SelectedImage.Meta.Sampler.Equals(descAttr.Description))
                            {
                                GblValues.Instance.Request.PromptItem.Sampler = value;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion
    }
}
