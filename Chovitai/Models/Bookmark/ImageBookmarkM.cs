using Chovitai.Common;
using Chovitai.Models.Config;
using Chovitai.Models.CvsImage;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsImage.CvsImageM;

namespace Chovitai.Models.Bookmark
{
    public class ImageBookmarkM : BookmarkBaseM
    {

        /// <summary>
        /// ブックマーク用保存用ディレクトリ
        /// </summary>
        public static string BookmarkDir { get; set; } = string.Format(@"{0}\imgbookmark", ConfigM.CurrDir);

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
        
        /// <summary>
        /// デフォルトブックマークファイルパス
        /// </summary>
        public static string DefaultBookmarkFile { get; set; } = "ImageBookmark.conf";


        #region イメージ用ブックマークディレクトリ[ImageBookmarkDir]プロパティ
        /// <summary>
        /// イメージ用ブックマークディレクトリ[ImageBookmarkDir]プロパティ
        /// </summary>
        public string ImageBookmarkDir
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
                    NotifyPropertyChanged("ImageBookmarkDir");
                }
            }
        }
        #endregion

        #region イメージ用ブックマークファイル[ImageBookmarkFile]プロパティ
        /// <summary>
        /// イメージ用ブックマークファイル[ImageBookmarkFile]プロパティ用変数
        /// </summary>
        string _ImageBookmarkFile = DefaultBookmarkFile;
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

        #region イメージ用コンフィグデータ[ImageBookmarkConf]プロパティ
        /// <summary>
        /// イメージ用コンフィグデータ[ImageBookmarkConf]プロパティ用変数
        /// </summary>
        ConfigManager<ModelList<CvsImageM.CvsItem>>? _ImageBookmarkConf = null;
        /// <summary>
        /// イメージ用コンフィグデータ[ImageBookmarkConf]プロパティ
        /// </summary>
        public ConfigManager<ModelList<CvsImageM.CvsItem>>? ImageBookmarkConf
        {
            get
            {
                return _ImageBookmarkConf;
            }
            set
            {
                if (_ImageBookmarkConf == null || !_ImageBookmarkConf.Equals(value))
                {
                    _ImageBookmarkConf = value;
                }
            }
        }
        #endregion

        #region ブックマークファイルの初期化
        /// <summary>
        /// ブックマークファイルの初期化
        /// 存在する場合は読み込み、存在しない場合は空のファイルを作成する
        /// </summary>
        public void InitBookmark()
        {

            // ブックマークファイルの存在確認
            if (File.Exists(Path.Combine(PathManager.GetApplicationFolder(), this.ImageBookmarkDir, this.ImageBookmarkFile)))
            {
                // イメージファイルのブックマーク情報の作成
                this.ImageBookmarkConf = new ConfigManager<ModelList<CvsImageM.CvsItem>>(this.ImageBookmarkDir,
                    this.ImageBookmarkFile,
                    new ModelList<CvsImageM.CvsItem>());
            }
            else
            {
                // イメージファイルのブックマーク情報の作成
                this.ImageBookmarkConf = new ConfigManager<ModelList<CvsImageM.CvsItem>>(ImageBookmarkM.BookmarkDir,
                    ImageBookmarkM.DefaultBookmarkFile, new ModelList<CvsImageM.CvsItem>());
            }

            // メージファイルのブックマーク情報の読み込み
            this.ImageBookmarkConf.LoadJSON();
        }
        #endregion

        #region ブックマークの状態と画面表示のブックマーク状態を合致させる
        /// <summary>
        /// ブックマークの状態と画面表示のブックマーク状態を合致させる
        /// </summary>
        public static void AdjustBookmark(ModelList<CvsItem> items)
        {

            // ブックマーク用のコンフィグを取得
            var bookmark_confg = GblValues.Instance.ImageBookmark.ImageBookmarkConf;

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
