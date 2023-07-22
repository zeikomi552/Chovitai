using Chovitai.Models.CvsTag;
using Chovitai.Views;
using Chovitai.Views.UserControls;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.ViewModels
{
    public class SearchTagVM : ViewModelBase
    {
        #region 選択アイテム[SelectedTagItem]プロパティ
        /// <summary>
        /// 選択アイテム[SelectedTagItem]プロパティ用変数
        /// </summary>
        CvsTagM.CvsItem _SelectedTagItem = new CvsTagM.CvsItem();
        /// <summary>
        /// 選択アイテム[SelectedTagItem]プロパティ
        /// </summary>
        public CvsTagM.CvsItem SelectedTagItem
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
                var uc = sender as SearchTagV;
                if (uc != null)
                {
                    var vm = uc.ucSearchTagV.DataContext as UcSearchTagVM;

                    if(vm != null)
                    {
                        vm.ParentVM = uc.DataContext as SearchTagVM;
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
