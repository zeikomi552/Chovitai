using Chovitai.Common;
using Chovitai.Models;
using Chovitai.Models.Bookmark;
using Chovitai.Models.Config;
using Chovitai.Models.CvsModel;
using Chovitai.Views;
using Chovitai.Views.UserControls;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsModel.CvsModelM;
using static Chovitai.Models.CvsModel.CvsModelM.CvsModelVersions;

namespace Chovitai.ViewModels
{
    public class UcModelBookmarkVM : ViewModelBase
    {
        #region ブックマーク[BookmarkConf]プロパティ
        /// <summary>
        /// ブックマーク[BookmarkConf]プロパティ
        /// </summary>
        public ConfigManager<ModelList<CvsItem>>? BookmarkConf
        {
            get
            {
                return GblValues.Instance.ModelBookmark.ModelBookmarkConf;
            }
            set
            {
                if (GblValues.Instance.ModelBookmark.ModelBookmarkConf == null || !GblValues.Instance.ModelBookmark.ModelBookmarkConf.Equals(value))
                {
                    GblValues.Instance.ModelBookmark.ModelBookmarkConf = value;
                    NotifyPropertyChanged("BookmarkConf");
                }
            }
        }
        #endregion

        #region Configファイルオブジェクト[Config]プロパティ
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ
        /// </summary>
        public ConfigManager<ConfigM>? Config
        {
            get
            {
                return GblValues.Instance.Config;
            }
            set
            {
                if (GblValues.Instance.Config == null || !GblValues.Instance.Config.Equals(value))
                {
                    GblValues.Instance.Config = value;
                    NotifyPropertyChanged("Config");
                }
            }
        }
        #endregion

        #region イメージリスト[ImageList]プロパティ
        /// <summary>
        /// イメージリスト[ImageList]プロパティ用変数
        /// </summary>
        DisplayImageM _ImageList = new DisplayImageM();
        /// <summary>
        /// イメージリスト[ImageList]プロパティ
        /// </summary>
        public DisplayImageM ImageList
        {
            get
            {
                return _ImageList;
            }
            set
            {
                if (_ImageList == null || !_ImageList.Equals(value))
                {
                    _ImageList = value;
                    NotifyPropertyChanged("ImageList");
                }
            }
        }
        #endregion

        #region ブックマーク用ディレクトリ
        /// <summary>
        /// ブックマーク用ディレクトリ
        /// </summary>
        public string BookmarkDir
        {
            get
            {
                return GblValues.Instance.ModelBookmark.BookmarkDirFullPath;
            }
        }
        #endregion

        #region ブックマークリスト
        /// <summary>
        /// ブックマークリスト
        /// </summary>
        public ModelList<ModelBookmarkM> BookmarkList
        {
            get
            {
                return GblValues.Instance.ModelBookmarkList;
            }
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Init(object sender, EventArgs ev)
        {
            try
            {
                InitBookmarkList();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ブックマークリストの初期化処理
        /// <summary>
        /// ブックマークリストの初期化処理
        /// </summary>
        private void InitBookmarkList()
        {
            // お気に入りのフォルダのパス
            var dirPath = this.BookmarkDir;

            List<ModelBookmarkM> list = new();

            // フォルダ内のファイル一覧を取得
            var fileArray = Directory.GetFiles(dirPath);
            foreach (string file in fileArray)
            {
                list.Add(new ModelBookmarkM() { BookmarkFilePath = file });
            }

            // ブックマークリストのセット
            this.BookmarkList.Items = new ObservableCollection<ModelBookmarkM>(list);

            var tmp = (from x in this.BookmarkList.Items
                       where x.BookmarkFile.Equals(this.Config!.Item.ModelBookmarkFile)
                       select x).FirstOrDefault();

            // nullチェック
            if (tmp != null)
            {
                // 最初の要素を選択
                this.BookmarkList.SelectedItem = tmp;
                BookmarkSelectionChanged();
            }

            // nullチェック
            if (this.BookmarkConf != null && this.BookmarkConf.Item != null && this.BookmarkConf.Item.Items != null && this.BookmarkConf.Item.Items.Count > 0)
            {
                // 最初の要素を選択
                this.BookmarkConf.Item.SelectedFirst();
            }
        }
        #endregion

        #region ブックマークの選択変更
        /// <summary>
        /// ブックマークの選択変更
        /// </summary>
        public void BookmarkSelectionChanged()
        {
            try
            {
                // nullチェックとファイルの存在確認
                if (this.BookmarkList.SelectedItem != null &&
                    File.Exists(this.BookmarkList.SelectedItem.BookmarkFilePath))
                {
                    // ブックマーク情報の作成
                    this.BookmarkConf = new ConfigManager<ModelList<CvsItem>>(this.Config!.Item.ModelBookmarkDir, this.BookmarkList.SelectedItem.BookmarkFile, new ModelList<CvsItem>());
                    this.BookmarkConf.LoadJSON();

                    // ブックマークの保存処理
                    SaveBookmark();

                    // 最初の要素を選択
                    this.BookmarkConf.Item.SelectedFirst();
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region リネーム画面を開く
        /// <summary>
        /// リネーム画面を開く
        /// </summary>
        public void OpenBookmarkRenameV()
        {
            try
            {
                var wnd = new BookmarkRenameV();
                var vm = wnd.DataContext as BookmarkRenameVM;

                // ディレクトリパスをセット
                vm!.DirPath = this.Config!.Item.ModelBookmarkDir;

                // nullチェック
                if (this.BookmarkList.Count > 0 && this.BookmarkList.SelectedItem != null && string.IsNullOrEmpty(this.BookmarkList.SelectedItem.BookmarkFile))
                {
                    // リネーム前のファイル名をセット
                    vm!.RenameFilename = Path.GetFileNameWithoutExtension(this.BookmarkList.SelectedItem.BookmarkFile);

                    // Windowを開く
                    if (wnd.ShowDialog() == true)
                    {
                        // ファイルパスの作成
                        string file_path = Path.Combine(this.BookmarkDir, vm!.RenameFilename + ".conf");

                        // ファイル名の変更
                        System.IO.File.Move(this.BookmarkList.SelectedItem.BookmarkFilePath, file_path);

                        // 変更後のファイル名をセット
                        this.BookmarkList.SelectedItem.BookmarkFilePath = file_path;

                        // ファイル名を変更する
                        this.Config.Item.ModelBookmarkFile = Path.GetFileName(file_path);

                        // ファイルを保存する
                        this.Config.SaveXML();
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Close(object sender, EventArgs ev)
        {
            try
            {
                this.BookmarkConf!.SaveJSON();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region モデルの選択が変更された場合の処理
        /// <summary>
        /// モデルの選択が変更された場合の処理
        /// </summary>
        public void ModelSelectionChanged(object sender, EventArgs ev)
        {
            try
            {
                // nullチェック
                if (this.BookmarkConf != null && this.BookmarkConf.Item != null && this.BookmarkConf.Item.SelectedItem != null)
                {

                    List<CvsImages> tmp_img = new List<CvsImages>();

                    // モデルバージョン分イメージをリストにセット
                    foreach (var modelver in this.BookmarkConf.Item.SelectedItem.ModelVersions)
                    {
                        // イメージをリストにセット
                        tmp_img.AddRange(modelver.Images);
                    }

                    // イメージをセットする
                    this.ImageList.SetImages(new ObservableCollection<CvsImages>(tmp_img));

                    // 最初の行を選択する
                    this.ImageList.SetFirst();

                    // ImageリストのListViewを先頭へスクロールさせる
                    ListViewTopRow(sender);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ListViewを先頭へスクロールさせる処理
        /// <summary>
        /// DataGridを先頭へスクロールさせる処理
        /// </summary>
        /// <param name="sender">画面内のコントロールオブジェクト</param>
        private void ListViewTopRow(object sender)
        {
            // ウィンドウの取得
            var wnd = (UcModelBookmarkV)VisualTreeHelperWrapper.GetWindow<UcModelBookmarkV>(sender);

            // nullチェック
            if (wnd != null)
            {
                // イメージのListViewのスクロールバーを先頭へ移動
                ScrollbarTopRow.TopRow4ListView(wnd.lvImages);
            }
        }
        #endregion

        #region Bookmarkの削除
        /// <summary>
        /// Bookmarkの削除
        /// </summary>
        public void DeleteBookmark()
        {
            try
            {
                // ブックマークが選択されている場合のみ処理
                if (this.BookmarkList.SelectedItem != null)
                {
                    if (ShowMessage.ShowQuestionYesNo($"Are you sure you want to delete '{this.BookmarkList.SelectedItem.BookmarkFile}'", "Querstion") 
                        == System.Windows.MessageBoxResult.Yes)
                    {
                        // ファイルの削除
                        File.Delete(this.BookmarkList.SelectedItem.BookmarkFilePath);

                        // ブックマークを削除
                        this.BookmarkList.Items.Remove(this.BookmarkList.SelectedItem);

                        // 最後の要素を選択する
                        this.BookmarkList.SelectedLast();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ブックマークの保存処理
        /// <summary>
        /// ブックマークの保存処理
        /// </summary>
        public void SaveBookmark()
        {
            try
            {
                // nullチェック
                if (this.BookmarkList != null && this.BookmarkList.SelectedItem != null)
                {
                    // ブックマークのセット
                    this.Config!.Item.ModelBookmarkFile = Path.GetFileName(this.BookmarkList.SelectedItem.BookmarkFilePath);

                    // JSON形式で保存
                    this.Config.SaveXML();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 新規作成
        /// <summary>
        /// 新規作成
        /// </summary>
        public void CreateNew()
        {
            try
            {
                string filename = @"bookmark.conf";

                int i = 0;
                // ファイルの存在確認
                while (File.Exists(Path.Combine(this.BookmarkDir, filename)))
                {
                    filename = $"bookmark({i++}).conf";
                }

                // ブックマーク情報の作成
                this.BookmarkConf = new ConfigManager<ModelList<CvsItem>>(this.BookmarkDir, filename, new ModelList<CvsItem>());
                this.BookmarkConf.SaveJSON();

                // お気に入りのコンボボックスに追加する
                this.BookmarkList.Items.Add(new ModelBookmarkM() { BookmarkFilePath = Path.Combine(this.BookmarkDir, filename) });
                this.BookmarkList.SelectedLast();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region Promptリスト作成画面へ移動処理
        /// <summary>
        /// Promptリスト作成画面へ移動処理
        /// </summary>
        public void CreatePromptList()
        {
            try
            {
                if (this.ImageList != null && this.BookmarkConf != null)
                {
                    var tmp = new PromptCountCollectionM();
                    tmp.InitItems(this.BookmarkConf.Item.Items.ToList<CvsItem>());

                    var wnd = new PromptWindowV();
                    var vm = wnd.DataContext as PromptWindowVM;
                    vm!.PromptItems = tmp;

                    if (wnd.ShowDialog() == true)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion


    }
}
