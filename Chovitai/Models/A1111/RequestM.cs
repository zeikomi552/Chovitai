using Chovitai.Common.Utilities;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chovitai.Models.A1111
{
    public class RequestM: ModelBase
    {
        #region 接続用クライアントの作成
        /// <summary>
        /// 接続用クライアントの作成
        /// </summary>
        /// <param name="url">パラメータ</param>
        /// <returns>Task</returns>
        public async Task<string> Request(string url, StringContent payload)
        {
            using (var client = new HttpClient())
            {
                // タイムアウト無制限
                client.Timeout = new TimeSpan(0, 0, 0, 0, Timeout.Infinite);

                // 上から来たクエリをそのまま実行
                var response = await client.PostAsync(url, payload);

                // レスポンスを返却
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion

        #region 接続用クライアントの作成
        /// <summary>
        /// 接続用クライアントの作成
        /// </summary>
        /// <param name="url">パラメータ</param>
        /// <returns>Task</returns>
        public async Task<string> Request(string url)
        {
            using (var client = new HttpClient())
            {
                // 上から来たクエリをそのまま実行
                var response = await client.GetAsync(url);

                // レスポンスを返却
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion

        #region POSTのリクエスト実行処理
        /// <summary>
        /// POSTのリクエスト実行処理
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="outdir">出力先ディレクトリ</param>
        public async Task<bool> PostRequest(string uri, string outdir, Text2ImagePromptM prompt)
        {
            try
            {
                PostResponseM tmp = new PostResponseM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = uri + "/sdapi/v1/txt2img";
                
                StringContent payload = prompt.GetPayload();    // Payloadの取得
                request = await tmp.Request(url, payload);      // Requestの実行

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<PostResponseM>(request);

                int count = 0;
                foreach (var base64string in request_model.Images)
                {
                    string path = Path.Combine(outdir, $"{DateTime.Now.ToString("yyyyMMddHHmmss-") + count.ToString()}.png");
                    SaveByteArrayAsImage(path, base64string);
                    count++;
                }

                return true;
            }
            catch (JSONDeserializeException e)
            {
                string msg = e.Message + "\r\n" + e.JSON;
                ShowMessage.ShowErrorOK(msg, "Error");
                return false;
            }
            finally
            {

            }
        }
        #endregion
        #region POSTのリクエスト実行処理
        /// <summary>
        /// POSTのリクエスト実行処理
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="outdir">出力先ディレクトリ</param>
        public async Task<bool> PostRequest(string uri, string outdir, Img2ImgPromptM prompt)
        {
            try
            {
                PostResponseM tmp = new PostResponseM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = uri + "/sdapi/v1/img2img";

                StringContent payload = prompt.GetPayload();    // Payloadの取得
                request = await tmp.Request(url, payload);      // Requestの実行

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<PostResponseM>(request);

                int count = 0;
                foreach (var base64string in request_model.Images)
                {
                    string path = Path.Combine(outdir, $"{DateTime.Now.ToString("yyyyMMddHHmmss-") + count.ToString()}.png");
                    SaveByteArrayAsImage(path, base64string);
                    count++;
                }

                return true;
            }
            catch (JSONDeserializeException e)
            {
                string msg = e.Message + "\r\n" + e.JSON;
                ShowMessage.ShowErrorOK(msg, "Error");
                return false;
            }
            finally
            {

            }
        }
        #endregion
        #region POSTのリクエスト実行処理
        /// <summary>
        /// POSTのリクエスト実行処理
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="outdir">出力先ディレクトリ</param>
        public async Task<bool> GetModels(string uri)
        {
            try
            {
                PostResponseM tmp = new PostResponseM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = uri + "/sdapi/v1/sd-models";

                var ret = await tmp.Request(url);      // Requestの実行

                return true;
            }
            catch (JSONDeserializeException e)
            {
                string msg = e.Message + "\r\n" + e.JSON;
                ShowMessage.ShowErrorOK(msg, "Error");
                return false;
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
            // ディレクトリがない場合は作成する
            PathManager.CreateCurrentDirectory(fullOutputPath);

            // Base64文字列をbyteに分解
            byte[] bytes = Convert.FromBase64String(base64String);

            // ファイルに保存する
            File.WriteAllBytes(fullOutputPath, bytes);
        }
        #endregion

        #region プロンプト要素[PromptItem]プロパティ
        /// <summary>
        /// プロンプト要素[PromptItem]プロパティ用変数
        /// </summary>
        Text2ImagePromptM _PromptItem = new Text2ImagePromptM();
        /// <summary>
        /// プロンプト要素[PromptItem]プロパティ
        /// </summary>
        public Text2ImagePromptM PromptItem
        {
            get
            {
                return _PromptItem;
            }
            set
            {
                if (_PromptItem == null || !_PromptItem.Equals(value))
                {
                    _PromptItem = value;
                    NotifyPropertyChanged("PromptItem");
                }
            }
        }
        #endregion

        #region Img2Img prompt[Img2ImgPrompt]プロパティ
        /// <summary>
        /// Img2Img prompt[Img2ImgPrompt]プロパティ用変数
        /// </summary>
        Img2ImgPromptM _Img2ImgPrompt = new Img2ImgPromptM();
        /// <summary>
        /// Img2Img prompt[Img2ImgPrompt]プロパティ
        /// </summary>
        public Img2ImgPromptM Img2ImgPrompt
        {
            get
            {
                return _Img2ImgPrompt;
            }
            set
            {
                if (_Img2ImgPrompt == null || !_Img2ImgPrompt.Equals(value))
                {
                    _Img2ImgPrompt = value;
                    NotifyPropertyChanged("Img2ImgPrompt");
                }
            }
        }
        #endregion


    }
}
