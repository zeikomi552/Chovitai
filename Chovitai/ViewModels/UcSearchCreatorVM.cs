using Chovitai.Models.CvsTag;
using Chovitai.Models;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chovitai.Models.CvsCreator;

namespace Chovitai.ViewModels
{
    public class UcSearchCreatorVM : ViewModelBase
    {
        public SearchCreatorVM? ParentVM { get; set; }

        #region クリエイター要素[CvsCreator]プロパティ
        /// <summary>
        /// クリエイター要素[CvsCreator]プロパティ用変数
        /// </summary>
        CvsCreatorM _CvsCreator = new CvsCreatorM();
        /// <summary>
        /// クリエイター要素[CvsCreator]プロパティ
        /// </summary>
        public CvsCreatorM CvsCreator
        {
            get
            {
                return _CvsCreator;
            }
            set
            {
                if (_CvsCreator == null || !_CvsCreator.Equals(value))
                {
                    _CvsCreator = value;
                    NotifyPropertyChanged("CvsCreator");
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

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Init(object sender, EventArgs ev)
        {
            try
            {
                Search(sender, ev);
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 画面閉じる処理
        /// <summary>
        /// 画面閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Close(object sender, EventArgs ev)
        {
            try
            {
                if (this.ParentVM != null)
                {
                    this.ParentVM.SelectedTagItem = this.CvsCreator.SelectedItem;
                    this.ParentVM.SelectClose();
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region Execute GET REST API
        /// <summary>
        /// Execute GET REST API
        /// </summary>
        public void Search(object sender, EventArgs ev)
        {
            try
            {
                // GET クエリの実行
                GETQuery(sender, "?limit=100&page=1");
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
                    url = CvsCreatorM.Endpoint + query;
                }
                else
                {
                    // エンドポイント + パラメータ
                    url = query;
                }

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<CvsCreatorM>(request = await tmp.Request(url));

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsCreator = request_model; // ModelListへ変換
                    this.CvsCreator!.Rowdata = request;               // 生データの保持
                    this.CvsCreator!.RequestURL = url;               // 生データの保持

                    // 1つ以上要素が存在する場合
                    if (this.CvsCreator.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsCreator.SelectedItem = this.CvsCreator.Items.ElementAt(0);
                    }
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
                if (this.CvsCreator != null)
                {
                    // 次のページが最終ページより前である場合
                    if (this.CvsCreator.Metadata.CurrentPage + 1 <= CvsCreator.Metadata.TotalPages)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsCreator.Metadata.NextPage, false);
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
                if (this.CvsCreator != null)
                {
                    // 前のページが1より大きい場合
                    if (this.CvsCreator.Metadata.CurrentPage - 1 >= 1)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsCreator.Metadata.PrevPage, false);
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
