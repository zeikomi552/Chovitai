using Chovitai.Common;
using Chovitai.Models.Bookmark;
using Microsoft.VisualBasic;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsModel.CvsModelM;

namespace Chovitai.Models.Config
{
    public class ConfigM : ModelBase
    {
        /// <summary>
        /// 現在のフォルダ
        /// </summary>
        public static string CurrDir { get; set; } = "conf";

        /// <summary>
        /// デフォルトのファイル名
        /// </summary>
        public static string DefaultFile { get; set; } = "CvSearchTool.conf";

        #region モデル用ブックマークのフォルダパス[ModelBookmarkDir]プロパティ
        /// <summary>
        /// モデル用ックマークのフォルダパス[ModelBookmarkDir]プロパティ
        /// </summary>
        public string ModelBookmarkDir
        {
            get
            {
                return ModelBookmarkM.BookmarkDir;
            }
            set
            {
                if (ModelBookmarkM.BookmarkDir == null || !ModelBookmarkM.BookmarkDir.Equals(value))
                {
                    ModelBookmarkM.BookmarkDir = value;
                    NotifyPropertyChanged("ModelBookmarkDir");
                }
            }
        }
        #endregion

        #region モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ
        /// <summary>
        /// モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ用変数
        /// </summary>
        string _ModelBookmarkFile = ModelBookmarkM.DefaultBookmarkFile;
        /// <summary>
        /// モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ
        /// </summary>
        public string ModelBookmarkFile
        {
            get
            {
                return _ModelBookmarkFile;
            }
            set
            {
                if (_ModelBookmarkFile == null || !_ModelBookmarkFile.Equals(value))
                {
                    _ModelBookmarkFile = value;
                    NotifyPropertyChanged("ModelBookmarkFile");
                }
            }
        }
        #endregion

        #region イメージ用ブックマークディレクトリ[ImageBookmarkDir]プロパティ
        /// <summary>
        /// イメージ用ブックマークディレクトリ[ImageBookmarkDir]プロパティ
        /// </summary>
        public string ImageBookmarkDir
        {
            get
            {
                return ImageBookmarkM.BookmarkDir;
            }
            set
            {
                if (ImageBookmarkM.BookmarkDir == null || !ImageBookmarkM.BookmarkDir.Equals(value))
                {
                    ImageBookmarkM.BookmarkDir = value;
                    NotifyPropertyChanged("ImageBookmarkDir");
                }
            }
        }
        #endregion

        #region イメージ用ブックマークファイル[ImageBookmarkFile]プロパティ
        /// <summary>
        /// イメージ用ブックマークファイル[ImageBookmarkFile]プロパティ用変数
        /// </summary>
        string _ImageBookmarkFile = ImageBookmarkM.DefaultBookmarkFile;
        /// <summary>
        /// イメージ用ブックマークファイル[ImageBookmarkFile]プロパティ
        /// </summary>
        public string ImageBookmarkFile
        {
            get
            {
                return _ImageBookmarkFile;
            }
            set
            {
                if (_ImageBookmarkFile == null || !_ImageBookmarkFile.Equals(value))
                {
                    _ImageBookmarkFile = value;
                    NotifyPropertyChanged("ImageBookmarkFile");
                }
            }
        }
        #endregion

    }
}
