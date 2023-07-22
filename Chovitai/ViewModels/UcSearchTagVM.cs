using Chovitai.Models.CvsImage;
using Chovitai.Models;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chovitai.Models.CvsTag;
using System.ComponentModel.Design;

namespace Chovitai.ViewModels
{
    public class UcSearchTagVM : ViewModelBase
    {
        public SearchTagVM? ParentVM { get; set; }

        #region タグ要素[CvsTag]プロパティ
        /// <summary>
        /// タグ要素[CvsTag]プロパティ用変数
        /// </summary>
        CvsTagM _CvsTag = new CvsTagM();
        /// <summary>
        /// タグ要素[CvsTag]プロパティ
        /// </summary>
        public CvsTagM CvsTag
        {
            get
            {
                return _CvsTag;
            }
            set
            {
                if (_CvsTag == null || !_CvsTag.Equals(value))
                {
                    _CvsTag = value;
                    NotifyPropertyChanged("CvsTag");
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
                    this.ParentVM.SelectedTagItem = this.CvsTag.SelectedItem;
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
                    url = CvsTagM.Endpoint + query;
                }
                else
                {
                    // エンドポイント + パラメータ
                    url = query;
                }

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<CvsTagM>(request = await tmp.Request(url));

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsTag = request_model; // ModelListへ変換
                    this.CvsTag!.Rowdata = request;               // 生データの保持
                    this.CvsTag!.RequestURL = url;               // 生データの保持

                    // 1つ以上要素が存在する場合
                    if (this.CvsTag.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsTag.SelectedItem = this.CvsTag.Items.ElementAt(0);
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
                if (this.CvsTag != null)
                {
                    // 次のページが最終ページより前である場合
                    if (this.CvsTag.Metadata.CurrentPage + 1 <= CvsTag.Metadata.TotalPages)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsTag.Metadata.NextPage, false);
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
                if (this.CvsTag != null)
                {
                    // 前のページが1より大きい場合
                    if (this.CvsTag.Metadata.CurrentPage - 1 >= 1)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsTag.Metadata.PrevPage, false);
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
