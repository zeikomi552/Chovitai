using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models
{


    public class PromptCountM: ModelBase
    {
        #region プロンプト[Prompt]プロパティ
        /// <summary>
        /// プロンプト[Prompt]プロパティ用変数
        /// </summary>
        string _Prompt = string.Empty;
        /// <summary>
        /// プロンプト[Prompt]プロパティ
        /// </summary>
        public string Prompt
        {
            get
            {
                string text = _Prompt;

                if (_Prompt.Contains(':'))
                {
                    text = $"({_Prompt})";
                    if (_Prompt.Contains('<') || _Prompt.Contains('>'))
                    {
                        text = _Prompt;
                    }
                }
                else
                {
                    text = _Prompt;
                }
                return text;
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

        #region 出現回数[Count]プロパティ
        /// <summary>
        /// 出現回数[Count]プロパティ用変数
        /// </summary>
        int _Count = 0;
        /// <summary>
        /// 出現回数[Count]プロパティ
        /// </summary>
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                if (!_Count.Equals(value))
                {
                    _Count = value;
                    NotifyPropertyChanged("Count");
                }
            }
        }
        #endregion
    }
}
