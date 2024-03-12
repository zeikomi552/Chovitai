using Chovitai.Common;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Chovitai.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        internal const int CTRL_C_EVENT = 0;
        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate? HandlerRoutine, bool Add);
        // Delegate type to be used as the Handler Routine for SCCH
        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

        #region デバッグフラグ[DebugF]プロパティ
        /// <summary>
        /// デバッグフラグ[DebugF]プロパティ
        /// </summary>
        public bool DebugF
        {
            get
            {
                return GblValues.Instance.Config!.Item.DebugF;
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
                // プロセスの解放処理
                ProcessRelease();
            }
            catch
            {

            }
        }
        #endregion

        #region プロセスの解放処理
        /// <summary>
        /// プロセスの解放処理
        /// </summary>
        public void ProcessRelease()
        {
            try
            {
                // アプリケーションのシャットダウン
                GblValues.AppShutdown();

                Process? p = GblValues.Instance.A1111Proc;

                if (p == null)
                {
                    return;
                }

                if (AttachConsole((uint)p.Id))
                {
                    SetConsoleCtrlHandler(null, true);
                    try
                    {
                        if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0))
                            return;
                        p.WaitForExit();
                    }
                    finally
                    {
                        SetConsoleCtrlHandler(null, false);
                        FreeConsole();
                    }
                }
            }
            catch
            {

            }
        }
        #endregion
    }
}
