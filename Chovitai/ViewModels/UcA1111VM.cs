using Chovitai.Common;
using Chovitai.Models.A1111;
using Chovitai.Models.Config;
using Chovitai.Views;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chovitai.Common.Utilities;
using System.Drawing;

namespace Chovitai.ViewModels
{
    public class UcA1111VM : ViewModelBase
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
                return _Request;
            }
            set
            {
                if (_Request == null || !_Request.Equals(value))
                {
                    _Request = value;
                    NotifyPropertyChanged("Request");
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

                if (wnd.ShowDialog() == true)
                {
                    GblValues.Instance.A1111Setting!.LoadXML();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region WebUI A1111の実行
        /// <summary>
        /// WebUI A1111の実行
        /// </summary>
        public void WebUIExecute()
        {
            try
            {
                var batpath = GblValues.Instance.A1111Setting?.Item.BatPath;

                Process p = new Process();
                p.StartInfo.WorkingDirectory = PathManager.GetCurrentDirectory(batpath);
                p.StartInfo.FileName = batpath;
                p.StartInfo.Verb = "RunAs"; //管理者として実行する場合

                p.Start();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region コンフィグデータ
        /// <summary>
        /// コンフィグデータ
        /// </summary>
        public A1111SettingConfigM A1111Config
        {
            get
            {
                return GblValues.Instance.A1111Setting!.Item;
            }
        }
        #endregion

        #region Promptの実行処理
        /// <summary>
        /// Promptの実行処理
        /// </summary>
        public void ClickPromptStart()
        {
            try
            {
                GETQuery();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region GETクエリの実行処理
        /// <summary>
        /// GETクエリの実行処理
        /// </summary>
        /// <param name="query"></param>
        /// <param name="add_endpoint"></param>
        private async void GETQuery()
        {
            try
            {
                //this.ExecuteGetAPI = true;
                PostResponseM tmp = new PostResponseM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = this.A1111Config.URL + "/sdapi/v1/txt2img";
                var data = new
                {
                    prompt = this.Request.Prompt,
                    negative_prompt = this.Request.NegativePrompt,
                    width = this.Request.Width,
                    height = this.Request.Height,
                    sapler_index = this.Request.SamplerIndex
                };

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<PostResponseM>(request = await tmp.Request(url, data.AsJson()));

                int count = 0;
                foreach (var base64string in request_model.Images)
                {
                    string path = Path.Combine(this.A1111Config.ImageOutDirectory, $"{DateTime.Now.ToString("yyyyMMddHHmmss-") + count.ToString()}.png");
                    SaveByteArrayAsImage(path, base64string);
                }
            }
            catch (JSONDeserializeException e)
            {
                string msg = e.Message + "\r\n" + e.JSON;
                ShowMessage.ShowErrorOK(msg, "Error");
            }
            finally
            {

            }
        }
        #endregion

        #region Base64文字列をファイルに保存する処理
        /// <summary>
        /// Base64文字列をファイルに保存する処理
        /// </summary>
        /// <param name="fullOutputPath">出力先ファイルパス</param>
        /// <param name="base64String">Base64文字列</param>
        private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Png);
        }
        #endregion
    }
}
