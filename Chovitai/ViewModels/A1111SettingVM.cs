using Chovitai.Common;
using Chovitai.Models.Config;
using Chovitai.Views;
using Chovitai.Views.UserControls;
using Microsoft.WindowsAPICodePack.Dialogs;
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
    public class A1111SettingVM : ViewModelBase
    {
        #region A1111コンフィグ[A1111Config]プロパティ
        /// <summary>
        /// A1111コンフィグ[A1111Config]プロパティ用変数
        /// </summary>
        A1111SettingConfigM _A1111Config = new A1111SettingConfigM();
        /// <summary>
        /// A1111コンフィグ[A1111Config]プロパティ
        /// </summary>
        public A1111SettingConfigM A1111Config
        {
            get
            {
                return _A1111Config;
            }
            set
            {
                if (_A1111Config == null || !_A1111Config.Equals(value))
                {
                    _A1111Config = value;
                    NotifyPropertyChanged("A1111Config");
                }
            }
        }
        #endregion

        A1111SettingV? _Wnd;
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
                // ウィンドウを取得
                this._Wnd = VisualTreeHelperWrapper.GetWindow<A1111SettingV>(sender) as A1111SettingV;


                this.A1111Config = GblValues.Instance.A1111Setting!.Item.ShallowCopy<A1111SettingConfigM>();
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
                GblValues.Instance.A1111Setting!.Item = this.A1111Config.ShallowCopy<A1111SettingConfigM>();
                GblValues.Instance.A1111Setting!.SaveXML();
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
                using (var cofd = new CommonOpenFileDialog()
                {
                    Title = "フォルダを選択してください",
                    // フォルダ選択モードにする
                    IsFolderPicker = true,
                    InitialDirectory = this.A1111Config.CurrentDirectory
                    
                })
                {
                    if (cofd.ShowDialog(this._Wnd) != CommonFileDialogResult.Ok)
                    {
                        return;
                    }
                    this.A1111Config.CurrentDirectory = cofd.FileName;
                }
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
                using (var cofd = new CommonOpenFileDialog()
                {
                    Title = "フォルダを選択してください",
                    // フォルダ選択モードにする
                    IsFolderPicker = true,
                    InitialDirectory = this.A1111Config.ImageOutDirectory
                })
                {
                    if (cofd.ShowDialog(this._Wnd) != CommonFileDialogResult.Ok)
                    {
                        return;
                    }
                    this.A1111Config.ImageOutDirectory = cofd.FileName;
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
