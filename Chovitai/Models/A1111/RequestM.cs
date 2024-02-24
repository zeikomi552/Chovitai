using Chovitai.Common.Utilities;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                // 上から来たクエリをそのまま実行
                var response = await client.PostAsync(url, payload);

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
        public async void PostRequest(string uri, string outdir)
        {
            try
            {
                PostResponseM tmp = new PostResponseM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = uri + "/sdapi/v1/txt2img";
                var data = new
                {
                    prompt = this.Prompt,
                    negative_prompt = this.NegativePrompt,
                    width = this.Width,
                    height = this.Height,
                    sapler_index = this.SamplerIndex
                };

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<PostResponseM>(request = await tmp.Request(url, data.AsJson()));

                int count = 0;
                foreach (var base64string in request_model.Images)
                {
                    string path = Path.Combine(outdir, $"{DateTime.Now.ToString("yyyyMMddHHmmss-") + count.ToString()}.png");
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

        #region Prompt for Webui A1111 api[Prompt]プロパティ
        /// <summary>
        /// Prompt for Webui A1111 api[Prompt]プロパティ用変数
        /// </summary>
        string _Prompt = string.Empty;
        /// <summary>
        /// Prompt for Webui A1111 api[Prompt]プロパティ
        /// </summary>
        public string Prompt
        {
            get
            {
                return _Prompt;
            }
            set
            {
                if (_Prompt == null || !_Prompt.Equals(value))
                {
                    _Prompt = value;
                    NotifyPropertyChanged("Prompt");
                }
            }
        }
        #endregion

        #region Negative Prompt for Webui A1111 api[NegativePrompt]プロパティ
        /// <summary>
        /// Negative Prompt for Webui A1111 api[NegativePrompt]プロパティ用変数
        /// </summary>
        string _NegativePrompt = string.Empty;
        /// <summary>
        /// Negative Prompt for Webui A1111 api[NegativePrompt]プロパティ
        /// </summary>
        public string NegativePrompt
        {
            get
            {
                return _NegativePrompt;
            }
            set
            {
                if (_NegativePrompt == null || !_NegativePrompt.Equals(value))
                {
                    _NegativePrompt = value;
                    NotifyPropertyChanged("NegativePrompt");
                }
            }
        }
        #endregion

        #region Picture width[Width]プロパティ
        /// <summary>
        /// Picture width[Width]プロパティ用変数
        /// </summary>
        int _Width = 512;
        /// <summary>
        /// Picture width[Width]プロパティ
        /// </summary>
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if (!_Width.Equals(value))
                {
                    _Width = value;
                    NotifyPropertyChanged("Width");
                }
            }
        }
        #endregion

        #region Picture height[Height]プロパティ
        /// <summary>
        /// Picture height[Height]プロパティ用変数
        /// </summary>
        int _Height = 768;
        /// <summary>
        /// Picture height[Height]プロパティ
        /// </summary>
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                if (!_Height.Equals(value))
                {
                    _Height = value;
                    NotifyPropertyChanged("Height");
                }
            }
        }
        #endregion

        #region Picture Sampler[SamplerIndex]プロパティ
        /// <summary>
        /// Picture Sampler[SamplerIndex]プロパティ用変数
        /// </summary>
        string _SamplerIndex = "DPM++ 2M Karras";
        /// <summary>
        /// Picture Sampler[SamplerIndex]プロパティ
        /// </summary>
        public string SamplerIndex
        {
            get
            {
                return _SamplerIndex;
            }
            set
            {
                if (_SamplerIndex == null || !_SamplerIndex.Equals(value))
                {
                    _SamplerIndex = value;
                    NotifyPropertyChanged("SamplerIndex");
                }
            }
        }
        #endregion

        #region Steps[Steps]プロパティ
        /// <summary>
        /// Steps[Steps]プロパティ用変数
        /// </summary>
        int _Steps = 40;
        /// <summary>
        /// Steps[Steps]プロパティ
        /// </summary>
        public int Steps
        {
            get
            {
                return _Steps;
            }
            set
            {
                if (!_Steps.Equals(value))
                {
                    _Steps = value;
                    NotifyPropertyChanged("Steps");
                }
            }
        }
        #endregion

        #region Random seed[Seed]プロパティ
        /// <summary>
        /// Random seed[Seed]プロパティ用変数
        /// </summary>
        int _Seed = -1;
        /// <summary>
        /// Random seed[Seed]プロパティ
        /// </summary>
        public int Seed
        {
            get
            {
                return _Seed;
            }
            set
            {
                if (!_Seed.Equals(value))
                {
                    _Seed = value;
                    NotifyPropertyChanged("Seed");
                }
            }
        }
        #endregion


    }
}
