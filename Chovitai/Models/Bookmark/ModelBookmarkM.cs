using Chovitai.Common;
using Chovitai.Common.Utilities;
using Chovitai.Models.Config;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsModel.CvsModelM;

namespace Chovitai.Models.Bookmark
{
    public class ModelBookmarkM : BookmarkBaseM
    {
        /// <summary>
        /// ブックマーク用保存用ディレクトリ
        /// </summary>
        public static string BookmarkDir { get; set; } = string.Format(@"{0}\bookmark", ConfigM.CurrDir);

        #region お気に入りを保存しているフォルダ
        /// <summary>
        /// お気に入りを保存しているフォルダ
        /// </summary>
        public string BookmarkDirFullPath
        {
            get
            {
                return Path.Combine(PathManager.GetApplicationFolder(), BookmarkDir);
            }
        }
        #endregion
        public static string DefaultBookmarkFile { get; set; } = "bookmark.conf";

        #region モデル用ブックマークのフォルダパス[ModelBookmarkDir]プロパティ
        /// <summary>
        /// モデル用ックマークのフォルダパス[ModelBookmarkDir]プロパティ
        /// </summary>
        public string ModelBookmarkDir
        {
            get
            {
                return BookmarkDir;
            }
            set
            {
                if (BookmarkDir == null || !BookmarkDir.Equals(value))
                {
                    BookmarkDir = value;
                    NotifyPropertyChanged("ModelBookmarkDir");
                }
            }
        }
        #endregion

        #region モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ
        /// <summary>
        /// モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ用変数
        /// </summary>
        string _ModelBookmarkFile = DefaultBookmarkFile;
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

        #region ブックマーク[ModelBookmarkConf]プロパティ
        /// <summary>
        /// ブックマーク[ModelBookmarkConf]プロパティ用変数
        /// </summary>
        ConfigManager<ModelList<CvsItem>>? _ModelBookmarkConf;
        /// <summary>
        /// ブックマーク[ModelBookmarkConf]プロパティ
        /// </summary>
        public ConfigManager<ModelList<CvsItem>>? ModelBookmarkConf
        {
            get
            {
                return _ModelBookmarkConf;
            }
            set
            {
                if (_ModelBookmarkConf == null || !_ModelBookmarkConf.Equals(value))
                {
                    _ModelBookmarkConf = value;
                }
            }
        }
        #endregion


        #region ブックマークの初期化処理
        /// <summary>
        /// ブックマークの初期化処理
        /// 存在する場合は読み込み、存在しない場合は空のファイルを作成する
        /// </summary>
        public void InitBookmark()
        {

            // ブックマークファイルの存在確認
            if (File.Exists(Path.Combine(PathManager.GetApplicationFolder(), this.ModelBookmarkDir, this.ModelBookmarkFile)))
            {
                // ブックマーク情報の作成
                this.ModelBookmarkConf = new ConfigManager<ModelList<CvsItem>>(this.ModelBookmarkDir, this.ModelBookmarkFile, new ModelList<CvsItem>());
            }
            else
            {
                // ブックマーク情報の作成
                this.ModelBookmarkConf = new ConfigManager<ModelList<CvsItem>>(ModelBookmarkM.BookmarkDir,
                    ModelBookmarkM.DefaultBookmarkFile, new ModelList<CvsItem>());
            }

            // ブックマークファイルの読み込み
            this.ModelBookmarkConf.LoadJSON();
        }
        #endregion

        #region ブックマークの状態と画面表示のブックマーク状態を合致させる
        /// <summary>
        /// ブックマークの状態と画面表示のブックマーク状態を合致させる
        /// </summary>
        public static void AdjustBookmark(ModelList<CvsItem> items)
        {

            // ブックマーク用のコンフィグを取得
            var bookmark_confg = GblValues.Instance.ModelBookmark.ModelBookmarkConf;

            // ブックマーク登録されている場合はブックマーク情報をセットする
            if (bookmark_confg != null && bookmark_confg.Item != null && bookmark_confg.Item.Items != null)
            {
                // モデル全数分回す
                foreach (var cvitem in items)
                {
                    // ブックマークに登録されているIDならセット
                    cvitem.IsBookmark = (from x in bookmark_confg.Item.Items
                                         where x.Id.Equals(cvitem.Id)
                                         select x).Any();
                }
            }
        }
        #endregion
    }
}
