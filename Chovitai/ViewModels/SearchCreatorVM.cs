using Chovitai.Models.CvsCreator;
using Chovitai.Models.CvsTag;
using Chovitai.Views;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.ViewModels
{
    public class SearchCreatorVM : ViewModelBase
    {
        #region 選択アイテム[SelectedTagItem]プロパティ
        /// <summary>
        /// 選択アイテム[SelectedTagItem]プロパティ用変数
        /// </summary>
        CvsCreatorM.CvsItem _SelectedTagItem = new CvsCreatorM.CvsItem();
        /// <summary>
        /// 選択アイテム[SelectedTagItem]プロパティ
        /// </summary>
        public CvsCreatorM.CvsItem SelectedTagItem
        {
            get
            {
                return _SelectedTagItem;
            }
            set
            {
                if (_SelectedTagItem == null || !_SelectedTagItem.Equals(value))
                {
                    _SelectedTagItem = value;
                    NotifyPropertyChanged("SelectedTagItem");
                }
            }
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
            try
            {
                var uc = sender as SearchCreatorV;
                if (uc != null)
                {
                    var vm = uc.ucSearchCreatorV.DataContext as UcSearchCreatorVM;

                    if (vm != null)
                    {
                        vm.ParentVM = uc.DataContext as SearchCreatorVM;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Close(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 選択を押してクローズ
        /// <summary>
        /// 選択を押してクローズ
        /// </summary>
        public void SelectClose()
        {
            try
            {
                this.DialogResult = true;   // 画面をtrueでクローズ
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region キャンセルを押してクローズ
        /// <summary>
        /// キャンセルを押してクローズ
        /// </summary>
        public void CancelClose()
        {
            try
            {
                this.DialogResult = false;  // 画面をfalseでクローズ
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 閉じている最中の処理
        /// <summary>
        /// 閉じている最中の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public void Closing(object sender, EventArgs ev)
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

    }
}
