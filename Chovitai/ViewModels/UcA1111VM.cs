using Chovitai.Common;
using Chovitai.Models.A1111;
using Chovitai.Models.Config;
using Chovitai.Views;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chovitai.Common.Utilities;
using System.Drawing;
using Chovitai.Models;
using System.Windows.Threading;
using Chovitai.Views.UserControls;
using MVVMCore.Common.Wrapper;

namespace Chovitai.ViewModels
{
    public class UcA1111VM : FileBaseVM
    {
        #region A1111 Request[Request]プロパティ
        /// <summary>
        /// A1111 Request[Request]プロパティ用変数
        /// </summary>
        RequestM _Request = new RequestM();
        /// <summary>
        /// A1111 Request[Request]プロパティ
        /// </summary>
        public RequestM Request
        {
            get
            {
                return _Request;
            }
            set
            {
                if (_Request == null || !_Request.Equals(value))
                {
                    _Request = value;
                    NotifyPropertyChanged("Request");
                }
            }
        }
        #endregion

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
                string dir = this.A1111Config.ImageOutDirectory;
                if (Directory.Exists(dir))
                {
                    ReadDirectory(dir);

                    // ファイルウォッチャーをいったん終了
                    FinishDirectoryWatching();

                    // ファイルウォッチャーの開始
                    StartDirectoryWatching(dir, "*.png");
                }

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 選択行が変化した際の処理
        /// <summary>
        /// 選択行が変化した際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectedItemChanged(object sender, EventArgs e)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<UcA1111V>(sender) as UcA1111V;

                // ウィンドウが取得できた場合
                if (wnd != null && FileList.SelectedItem != null)
                {
                    ScrollbarUtility.TopRow(wnd.lvImages);
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

        }
        #endregion

        #region 設定画面を開く処理
        /// <summary>
        /// 設定画面を開く処理
        /// </summary>
        public void OpenSetting()
        {
            try
            {
                A1111SettingV wnd = new A1111SettingV();
                var vm = wnd.DataContext as A1111SettingVM;

                if (wnd.ShowDialog() == true)
                {
                    GblValues.Instance.A1111Setting!.LoadXML();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region WebUI A1111の実行
        /// <summary>
        /// WebUI A1111の実行
        /// </summary>
        public void WebUIExecute()
        {
            try
            {
                var batpath = GblValues.Instance.A1111Setting?.Item.BatPath;

                Process p = new Process();
                p.StartInfo.WorkingDirectory = PathManager.GetCurrentDirectory(batpath);
                p.StartInfo.FileName = batpath;
                p.StartInfo.Verb = "RunAs"; //管理者として実行する場合
                p.Start();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region コンフィグデータ
        /// <summary>
        /// コンフィグデータ
        /// </summary>
        public A1111SettingConfigM A1111Config
        {
            get
            {
                return GblValues.Instance.A1111Setting!.Item;
            }
        }
        #endregion

        #region Promptの実行処理
        /// <summary>
        /// Promptの実行処理
        /// </summary>
        public void ClickPromptStart()
        {
            try
            {
                this.Request.PostRequest(this.A1111Config.URL, this.A1111Config.ImageOutDirectory);
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion
    }
}
