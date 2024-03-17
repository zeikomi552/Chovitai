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

        #region お気に入りフォルダパス[FavoriteDirectory]プロパティ
        /// <summary>
        /// お気に入りフォルダパス[FavoriteDirectory]プロパティ用変数
        /// </summary>
        string _FavoriteDirectory = string.Empty;
        /// <summary>
        /// お気に入りフォルダパス[FavoriteDirectory]プロパティ
        /// </summary>
        public string FavoriteDirectory
        {
            get
            {
                return _FavoriteDirectory;
            }
            set
            {
                if (_FavoriteDirectory == null || !_FavoriteDirectory.Equals(value))
                {
                    _FavoriteDirectory = value;
                    NotifyPropertyChanged("FavoriteDirectory");
                }
            }
        }
        #endregion

        #region 動画保存パス[MovieDirectoryPath]プロパティ
        /// <summary>
        /// 動画保存パス[MovieDirectoryPath]プロパティ用変数
        /// </summary>
        string _MovieDirectoryPath = string.Empty;
        /// <summary>
        /// 動画保存パス[MovieDirectoryPath]プロパティ
        /// </summary>
        public string MovieDirectoryPath
        {
            get
            {
                return _MovieDirectoryPath;
            }
            set
            {
                if (_MovieDirectoryPath == null || !_MovieDirectoryPath.Equals(value))
                {
                    _MovieDirectoryPath = value;
                    NotifyPropertyChanged("MovieDirectoryPath");
                }
            }
        }
        #endregion


        #region URL[URL]プロパティ
        /// <summary>
        /// URL[URL]プロパティ用変数
        /// </summary>
        string _URL = "http://127.0.0.1:7860";
        /// <summary>
        /// URL[URL]プロパティ
        /// </summary>
        public string URL
        {
            get
            {
                return _URL;
            }
            set
            {
                if (_URL == null || !_URL.Equals(value))
                {
                    _URL = value;
                    NotifyPropertyChanged("URL");
                }
            }
        }
        #endregion
    }
}
