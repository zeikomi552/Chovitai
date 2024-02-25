using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models.A1111
{
    public class PromptM : ModelBase
    {
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
                                ret.Seed = int.TryParse(div_text, out int seed) ? seed : 20;

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
    }
}
