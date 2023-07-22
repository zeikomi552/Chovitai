using Chovitai.Common.Enums;
using Chovitai.Models.Bookmark;
using Chovitai.Models.Config;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsModel.CvsModelM;

namespace Chovitai.Common
{
    public sealed class GblValues
    {
        private static GblValues _Instance = new GblValues();

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private GblValues()
        {
            // コンフィグファイルの初期化処理
            ConfigInit();
        }
        #endregion

        #region インスタンス
        /// <summary>
        /// インスタンス
        /// </summary>
        public static GblValues Instance
        {
            get
            {
                return _Instance;
            }
        }
        #endregion

        #region モデル用ブックマークオブジェクト[ModelBookmark]プロパティ
        /// <summary>
        /// モデル用ブックマークオブジェクト[ModelBookmark]プロパティ用変数
        /// </summary>
        ModelBookmarkM _ModelBookmark = new ModelBookmarkM();
        /// <summary>
        /// モデル用ブックマークオブジェクト[ModelBookmark]プロパティ
        /// </summary>
        public ModelBookmarkM ModelBookmark
        {
            get
            {
                return _ModelBookmark;
            }
            set
            {
                if (_ModelBookmark == null || !_ModelBookmark.Equals(value))
                {
                    _ModelBookmark = value;
                }
            }
        }
        #endregion

        #region イメージ用ブックマークオブジェクト[ImageBookmark]プロパティ
        /// <summary>
        /// イメージ用ブックマークオブジェクト[ImageBookmark]プロパティ用変数
        /// </summary>
        ImageBookmarkM _ImageBookmark = new ImageBookmarkM();
        /// <summary>
        /// イメージ用ブックマークオブジェクト[ImageBookmark]プロパティ
        /// </summary>
        public ImageBookmarkM ImageBookmark
        {
            get
            {
                return _ImageBookmark;
            }
            set
            {
                if (_ImageBookmark == null || !_ImageBookmark.Equals(value))
                {
                    _ImageBookmark = value;
                }
            }
        }
        #endregion

        #region Configファイルオブジェクト[Config]プロパティ
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ用変数
        /// </summary>
        ConfigManager<ConfigM>? _Config;
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ
        /// </summary>
        public ConfigManager<ConfigM>? Config
        {
            get
            {
                return _Config;
            }
            set
            {
                if (_Config == null || !_Config.Equals(value))
                {
                    _Config = value;
                }
            }
        }
        #endregion

        #region Model用ブックマークリスト[ModelBookmarkList]プロパティ
        /// <summary>
        /// Model用ブックマークリスト[ModelBookmarkList]プロパティ用変数
        /// </summary>
        ModelList<ModelBookmarkM> _ModelBookmarkList = new ModelList<ModelBookmarkM>();
        /// <summary>
        /// Model用ブックマークリスト[ModelBookmarkList]プロパティ
        /// </summary>
        public ModelList<ModelBookmarkM> ModelBookmarkList
        {
            get
            {
                return _ModelBookmarkList;
            }
            set
            {
                if (_ModelBookmarkList == null || !_ModelBookmarkList.Equals(value))
                {
                    _ModelBookmarkList = value;
                }
            }
        }
        #endregion

        #region イメージ用ブックマークリスト[ImageBookmarkList]プロパティ
        /// <summary>
        /// イメージ用ブックマークリスト[ImageBookmarkList]プロパティ用変数
        /// </summary>
        ModelList<ImageBookmarkM> _ImageBookmarkList = new ModelList<ImageBookmarkM>();
        /// <summary>
        /// イメージ用ブックマークリスト[ImageBookmarkList]プロパティ
        /// </summary>
        public ModelList<ImageBookmarkM> ImageBookmarkList
        {
            get
            {
                return _ImageBookmarkList;
            }
            set
            {
                if (_ImageBookmarkList == null || !_ImageBookmarkList.Equals(value))
                {
                    _ImageBookmarkList = value;
                }
            }
        }
        #endregion

        #region コンフィグファイルの初期化処理
        /// <summary>
        /// コンフィグファイルの初期化処理
        /// </summary>
        public void ConfigInit()
        {
            // コンフィグファイルの作成
            this.Config = new ConfigManager<ConfigM>(ConfigM.CurrDir, ConfigM.DefaultFile, new ConfigM());

            // コンフィグファイルの読み込み
            this.Config.LoadXML();

            // モデル用ブックマークの初期化処理
            this.ModelBookmark.InitBookmark();

            // イメージ用ブックマークの初期化処理
            this.ImageBookmark.InitBookmark();
        }
        #endregion
    }
}
