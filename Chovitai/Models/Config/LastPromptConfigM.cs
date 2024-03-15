using Chovitai.Models.A1111;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models.Config
{
    public class LastPromptConfigM : ModelBase
    {
        #region 現在のフォルダ
        /// <summary>
        /// 現在のフォルダ
        /// </summary>
        public static string CurrDir { get; set; } = "conf";
        #endregion

        #region デフォルトのファイル名
        /// <summary>
        /// デフォルトのファイル名
        /// </summary>
        public static string DefaultFile { get; set; } = "LastPrompt.conf";
        #endregion

        #region 最終実行プロンプト[LastPrompt]プロパティ
        /// <summary>
        /// 最終実行プロンプト[LastPrompt]プロパティ用変数
        /// </summary>
        Text2ImagePromptM _LastPrompt = new Text2ImagePromptM();
        /// <summary>
        /// 最終実行プロンプト[LastPrompt]プロパティ
        /// </summary>
        public Text2ImagePromptM LastPrompt
        {
            get
            {
                return _LastPrompt;
            }
            set
            {
                if (_LastPrompt == null || !_LastPrompt.Equals(value))
                {
                    _LastPrompt = value;
                    NotifyPropertyChanged("LastPrompt");
                }
            }
        }
        #endregion
    }
}
