using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.ViewModels
{
    public class A1111SettingVM : ViewModelBase
    {
        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
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

        #region 設定保存処理
        /// <summary>
        /// 設定保存処理
        /// </summary>
        public void Save()
        {
            try
            {
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 設定キャンセル処理
        /// <summary>
        /// 設定キャンセル処理
        /// </summary>
        public void Cancel()
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region WebUI カレントディレクトリ設定処理
        /// <summary>
        /// WebUI カレントディレクトリ設定処理
        /// </summary>
        public void WebUIFolderOpen()
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

        #region イメージフォルダ設定処理
        /// <summary>
        /// イメージフォルダ設定処理
        /// </summary>
        public void ImageOutFolderOpen()
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
    }
}
