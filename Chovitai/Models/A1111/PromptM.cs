using Chovitai.Common.Enums;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chovitai.Common.Utilities;
using System.ComponentModel;
using System.Net.Http;
using System.Diagnostics;

namespace Chovitai.Models.A1111
{
    public class PromptM : ModelBase
    {
        Random _Rand = new Random();


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

        #region cfg scale value[CfgScale]プロパティ
        /// <summary>
        /// cfg scale value[CfgScale]プロパティ用変数
        /// </summary>
        decimal _CfgScale = 7;
        /// <summary>
        /// cfg scale value[CfgScale]プロパティ
        /// </summary>
        public decimal CfgScale
        {
            get
            {
                return _CfgScale;
            }
            set
            {
                if (!_CfgScale.Equals(value))
                {
                    if (value >= 0 && value <= 30)
                    {
                        _CfgScale = value;
                        NotifyPropertyChanged("CfgScale");
                    }
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

        #region n_iter value[N_iter]プロパティ
        /// <summary>
        /// n_iter value[N_iter]プロパティ用変数
        /// </summary>
        int _N_iter = 1;
        /// <summary>
        /// n_iter value[N_iter]プロパティ
        /// </summary>
        public int N_iter
        {
            get
            {
                return _N_iter;
            }
            set
            {
                if (!_N_iter.Equals(value))
                {
                    if (value >= 1 && value <= 100)
                    {
                        _N_iter = value;
                        NotifyPropertyChanged("N_iter");
                    }
                }
            }
        }
        #endregion

        #region Batch size value[BatchSize]プロパティ
        /// <summary>
        /// Batch size value[BatchSize]プロパティ用変数
        /// </summary>
        int _BatchSize = 1;
        /// <summary>
        /// Batch size value[BatchSize]プロパティ
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _BatchSize;
            }
            set
            {
                if (!_BatchSize.Equals(value))
                {
                    if (value >= 1 && value <= 8)
                    {
                        _BatchSize = value;
                        NotifyPropertyChanged("BatchSize");
                    }
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

        #region 一致確認
        /// <summary>
        /// 一致確認
        /// </summary>
        /// <param name="req">リクエスト</param>
        /// <returns>true:一致する false:不一致</returns>
        public bool Equals(PromptM req)
        {
            if (req.Prompt.Equals(this.Prompt)
                && req.NegativePrompt.Equals(this.NegativePrompt)
                && req.Height.Equals(this.Height)
                && req.Width.Equals(this.Width)
                && req.Steps.Equals(this.Steps))
                return true;
            else return false;
        }
        #endregion

        #region Commandへセット
        /// <summary>
        /// Commandへセット
        /// </summary>
        public static PromptM CreateCommandFromImageText(string image_text)
        {
            try
            {
                string[] text = image_text.Split("\n");
                PromptM ret = new PromptM();

                foreach (var tmp in text)
                {
                    string div_text = string.Empty;
                    if (CheckAndDiv(tmp, "parameters:", out div_text))
                    {
                        ret.Prompt = div_text;
                    }
                    else if (CheckAndDiv(tmp, "Negative prompt:", out div_text))
                    {
                        ret.NegativePrompt = div_text;
                    }
                    else
                    {
                        string[] tmp2 = tmp.Split(",");

                        foreach (var item in tmp2)
                        {
                            if (CheckAndDiv(item, "Steps:", out div_text))
                            {
                                ret.Steps = int.TryParse(div_text, out int steps) ? steps : 20;
                            }
                            else if (CheckAndDiv(item, "Sampler:", out div_text))
                            {
                                ret.SamplerIndex = div_text;
                            }
                            else if (CheckAndDiv(item, "Seed:", out div_text))
                            {
                                ret.Seed = Int64.TryParse(div_text, out Int64 seed) ? seed : 20;
                            }
                            else if (CheckAndDiv(item, "Model:", out div_text))
                            {
                                ret.CheckPoint = div_text;
                            }
                            else if (CheckAndDiv(item, "Size:", out div_text))
                            {
                                string[] wh = div_text.Split("x");

                                if (wh.Length >= 2)
                                {
                                    ret.Width = int.TryParse(wh[0], out int width) ? width : 512;
                                    ret.Height = int.TryParse(wh[1], out int height) ? height : 512;
                                }
                            }
                        }

                    }
                }
                return ret;

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
                return new PromptM();
            }
        }
        #endregion

        #region 512x768のような文字列を幅と高さに分割する
        /// <summary>
        /// 512x768のような文字列を幅と高さに分割する
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <param name="w">幅</param>
        /// <param name="h">高さ</param>
        /// <returns>true:分割成功 false:分割失敗(デフォルト 512x768)</returns>
        public static bool SizeToWH(string size, out int w, out int h)
        {
            string[] wh = size.Split('x');

            if (wh.Length >= 2)
            {
                int tmp;
                w = int.TryParse(wh[0], out tmp) ? tmp : 512;
                h = int.TryParse(wh[1], out tmp) ? tmp : 768;
                return true;
            }
            else
            {
                w = 512;
                h = 768;
                return false;
            }

        }
        #endregion

        #region Imageに含まれるテキストの prameter:などを確認し後続の文字列のみ取り出す関数
        /// <summary>
        /// Imageに含まれるテキストの prameter:などを確認し後続の文字列のみ取り出す関数
        /// </summary>
        /// <param name="image_text">Imageに含まれるテキスト</param>
        /// <param name="check_text">parameter:など</param>
        /// <param name="div_text">戻り値：後続の文字列</param>
        /// <returns>true:チェックする文字列が存在した false:チェックする文字列が存在しなかった</returns>
        private static bool CheckAndDiv(string image_text, string check_text, out string div_text)
        {
            if (image_text.Contains(check_text))
            {
                div_text = image_text.Replace(check_text, "").Trim();
                return true;
            }
            else
            {
                div_text = string.Empty;
                return false;
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
                prompt = prompt.Prompt,
                negative_prompt = prompt.NegativePrompt,
                steps = prompt.Steps,
                width = prompt.Width,
                height = prompt.Height,
                cfg_scale = prompt.CfgScale,
                sampler_index = prompt.SamplerIndex,
                n_iter = prompt.N_iter,
                batch_size = prompt.BatchSize,
                seed = prompt.Seed < 0 ? this.SeedBackup : prompt.Seed,
                override_settings = new
                {
                    sd_model_checkpoint = prompt.CheckPoint
                }
            };

            Debug.WriteLine(data.ToString());

            return data.AsJson();
        }
        #endregion
    }
}
