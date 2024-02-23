using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models.Config
{
    public class A1111SettingConfigM : ModelBase
    {
        /// <summary>
        /// 現在のフォルダ
        /// </summary>
        public static string CurrDir { get; set; } = "conf";

        /// <summary>
        /// デフォルトのファイル名
        /// </summary>
        public static string DefaultFile { get; set; } = "A1111Setting.conf";

        #region WebUI A1111のカレントディレクトリ[CurrentDirectory]プロパティ
        /// <summary>
        /// WebUI A1111のカレントディレクトリ[CurrentDirectory]プロパティ用変数
        /// </summary>
        string _CurrentDirectory = string.Empty;
        /// <summary>
        /// WebUI A1111のカレントディレクトリ[CurrentDirectory]プロパティ
        /// </summary>
        public string CurrentDirectory
        {
            get
            {
                return _CurrentDirectory;
            }
            set
            {
                if (_CurrentDirectory == null || !_CurrentDirectory.Equals(value))
                {
                    _CurrentDirectory = value;
                    NotifyPropertyChanged("CurrentDirectory");
                }
            }
        }
        #endregion

        #region イメージ出力先ディレクトリ[ImageOutDirectory]プロパティ
        /// <summary>
        /// イメージ出力先ディレクトリ[ImageOutDirectory]プロパティ用変数
        /// </summary>
        string _ImageOutDirectory = string.Empty;
        /// <summary>
        /// イメージ出力先ディレクトリ[ImageOutDirectory]プロパティ
        /// </summary>
        public string ImageOutDirectory
        {
            get
            {
                return _ImageOutDirectory;
            }
            set
            {
                if (_ImageOutDirectory == null || !_ImageOutDirectory.Equals(value))
                {
                    _ImageOutDirectory = value;
                    NotifyPropertyChanged("ImageOutDirectory");
                }
            }
        }
        #endregion


    }
}
