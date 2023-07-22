using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.ViewModels
{
    public class BookmarkRenameVM : ViewModelBase
    {
        #region ディレクトリパス[DirPath]プロパティ
        /// <summary>
        /// ディレクトリパス[DirPath]プロパティ用変数
        /// </summary>
        string _DirPath = string.Empty;
        /// <summary>
        /// ディレクトリパス[DirPath]プロパティ
        /// </summary>
        public string DirPath
        {
            get
            {
                return _DirPath;
            }
            set
            {
                if (_DirPath == null || !_DirPath.Equals(value))
                {
                    _DirPath = value;
                    NotifyPropertyChanged("DirPath");
                }
            }
        }
        #endregion

        #region リネーム後のファイル名[RenameFilename]プロパティ
        /// <summary>
        /// リネーム後のファイル名[RenameFilename]プロパティ用変数
        /// </summary>
        string _RenameFilename = string.Empty;
        /// <summary>
        /// リネーム後のファイル名[RenameFilename]プロパティ
        /// </summary>
        public string RenameFilename
        {
            get
            {
                return _RenameFilename;
            }
            set
            {
                if (_RenameFilename == null || !_RenameFilename.Equals(value))
                {
                    _RenameFilename = value;
                    NotifyPropertyChanged("RenameFilename");
                }
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
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region Okボタンが押された
        /// <summary>
        /// Okボタンが押された
        /// </summary>
        public void OnOk()
        {
            try
            {
                // ファイルの存在確認
                if (File.Exists(Path.Combine(this.DirPath, this.RenameFilename)))
                {
                    ShowMessage.ShowNoticeOK("Same file already exist!", "Notice");
                }
                else
                {
                    this.DialogResult = true;
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region キャンセルボタンが押された
        /// <summary>
        /// キャンセルボタンが押された
        /// </summary>
        public void OnCancel()
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion
    }
}
