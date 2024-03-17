using Chovitai.Common.Enums;
using Chovitai.Common.Utilities;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chovitai.Models.A1111
{
    public class Img2ImgPromptM : ModelBase
    {
        Random _Rand = new Random();

        #region 初期ファイルパス[InitImage]プロパティ
        /// <summary>
        /// 初期ファイルパス[InitImage]プロパティ用変数
        /// </summary>
        string _InitImage = string.Empty;
        /// <summary>
        /// 初期ファイルパス[InitImage]プロパティ
        /// </summary>
        public string InitImage
        {
            get
            {
                return _InitImage;
            }
            set
            {
                if (_InitImage == null || !_InitImage.Equals(value))
                {
                    _InitImage = value;
                    NotifyPropertyChanged("InitImage");
                    NotifyPropertyChanged("InitImageBase64");
                }
            }
        }
        #endregion

        public string InitImageBase64
        {
            get
            {
                using (Bitmap bitmap = new Bitmap(_InitImage))
                {
                    var tmp = BitmapToBase64String(bitmap);
                    return tmp;
                }
            }
        }
        string BitmapToBase64String(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bites = ms.ToArray();
                return Convert.ToBase64String(bites, Base64FormattingOptions.None);
            }
        }
        #region チェックポイント[CheckPoint]プロパティ
        /// <summary>
        /// チェックポイント[CheckPoint]プロパティ用変数
        /// </summary>
        string _CheckPoint = string.Empty;
        /// <summary>
        /// チェックポイント[CheckPoint]プロパティ
        /// </summary>
        public string CheckPoint
        {
            get
            {
                return _CheckPoint;
            }
            set
            {
                if (_CheckPoint == null || !_CheckPoint.Equals(value))
                {
                    _CheckPoint = value;
                    NotifyPropertyChanged("CheckPoint");
                }
            }
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

        #region denoising strength parameter[Denoising_Strength]プロパティ
        /// <summary>
        /// denoising strength parameter[Denoising_Strength]プロパティ用変数
        /// </summary>
        decimal _Denoising_Strength = (decimal)0.3;
        /// <summary>
        /// denoising strength parameter[Denoising_Strength]プロパティ
        /// </summary>
        public decimal Denoising_Strength
        {
            get
            {
                return _Denoising_Strength;
            }
            set
            {
                if (!_Denoising_Strength.Equals(value))
                {
                    _Denoising_Strength = value;
                    NotifyPropertyChanged("Denoising_Strength");
                }
            }
        }
        #endregion

        #region Random seed[Seed]プロパティ
        /// <summary>
        /// Random seed[Seed]プロパティ用変数
        /// </summary>
        Int64 _Seed = -1;
        /// <summary>
        /// Random seed[Seed]プロパティ
        /// </summary>
        public Int64 Seed
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

        #region Seed値のバックアップ（最後に実行したSeed値）[SeedBackup]プロパティ
        /// <summary>
        /// Seed値のバックアップ（最後に実行したSeed値）[SeedBackup]プロパティ用変数
        /// </summary>
        Int64 _SeedBackup = -1;
        /// <summary>
        /// Seed値のバックアップ（最後に実行したSeed値）[SeedBackup]プロパティ
        /// </summary>
        public Int64 SeedBackup
        {
            get
            {
                return _SeedBackup;
            }
            set
            {
                if (!_SeedBackup.Equals(value))
                {
                    _SeedBackup = value;
                    NotifyPropertyChanged("SeedBackup");
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
                    NotifyPropertyChanged("Sampler");
                }
            }
        }
        #endregion

        #region Picture Sampler[Sampler]プロパティ
        /// <summary>
        /// Picture Sampler[Sampler]プロパティ
        /// </summary>
        public SamplerIndexEnum? Sampler
        {
            get
            {
                // 値を列挙してコンソールに出力する
                var samplers = Enum.GetValues(typeof(SamplerIndexEnum));

                // SamplerIndexの列挙を確認
                foreach (SamplerIndexEnum sampler in samplers)
                {
                    DescriptionAttribute? t = new DescriptionAttribute();   // Description属性を取得する
                    sampler.TryGetAttribute<DescriptionAttribute>(out t);

                    // SamplerIndexとの一致確認
                    if (t != null && t.Description.Equals(this.SamplerIndex))
                    {
                        return sampler;
                    }
                }
                return null;

            }
            set
            {
                if (_SamplerIndex == null || !_SamplerIndex.Equals(value))
                {
                    DescriptionAttribute? t = new DescriptionAttribute();   // Description属性を取得する

                    if (value != null && value.TryGetAttribute<DescriptionAttribute>(out t))
                    {
                        _SamplerIndex = t!.Description;
                        NotifyPropertyChanged("Sampler");
                    }
                    else
                    {
                        _SamplerIndex = string.Empty;
                        NotifyPropertyChanged("Sampler");
                    }
                }
            }
        }
        #endregion

        #region Payloadの作成処理
        /// <summary>
        /// Payloadの作成処理
        /// </summary>
        /// <returns></returns>
        public StringContent GetPayload()
        {
            var prompt = this;
            this.SeedBackup = _Rand.Next(); // ランダムのSeed値を作成しておく
            var data = new
            {
                init_images = "[" + prompt.InitImageBase64 + "]",
                prompt = prompt.Prompt,
                negative_prompt = prompt.NegativePrompt,
                steps = prompt.Steps,
                width = prompt.Width,
                height = prompt.Height,
                denoising_strength = prompt.Denoising_Strength,
                ////cfg_scale = prompt.CfgScale,
                sampler_index = prompt.SamplerIndex,
                ////n_iter = prompt.N_iter,
                ////batch_size = prompt.BatchSize,
                seed = prompt.Seed < 0 ? this.SeedBackup : prompt.Seed,
                //override_settings = new
                //{
                //    sd_model_checkpoint = prompt.CheckPoint
                //}
            };
            var tmp = JsonConvert.SerializeObject(data);
            tmp = tmp.Replace("\"[", "[\"").Replace("]\"", "\"]");

            var json = new StringContent(tmp, Encoding.UTF8, "application/json");
            Debug.WriteLine(tmp);

            return json;
        }
        #endregion

        #region 動画の縦・横からImg2Imgの高さと幅をセットする
        /// <summary>
        /// 動画の縦・横からImg2Imgの高さと幅をセットする
        /// </summary>
        /// <param name="mov_path">動画ファイルサイズ</param>
        public void SetMovieWH(string mov_path)
        {
            using (var capture = new VideoCapture(mov_path))
            {
                this.Width = capture.FrameWidth;
                this.Height = capture.FrameHeight;
            }
        }
        #endregion
    }
}
