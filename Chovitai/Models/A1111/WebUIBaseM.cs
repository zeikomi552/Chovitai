using Chovitai.Common;
using Chovitai.Models.Config;
using Chovitai.ViewModels;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Chovitai.Models.A1111
{
    public class WebUIBaseM : FileBaseVM
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

        #region WebUIプロセス実行中[ExecuteProcessF]プロパティ
        /// <summary>
        /// WebUIプロセス実行中[ExecuteProcessF]プロパティ
        /// </summary>
        public bool ExecuteProcessF
        {
            get
            {
                return GblValues.Instance.ExecA111ProcF;
            }
            set
            {
                if (!GblValues.Instance.ExecA111ProcF.Equals(value))
                {
                    GblValues.Instance.ExecA111ProcF = value;
                    NotifyPropertyChanged("ExecuteProcessF");
                }
            }
        }
        #endregion

        #region リダイレクトメッセージ[RedirectMessage]プロパティ
        /// <summary>
        /// リダイレクトメッセージ[RedirectMessage]プロパティ用変数
        /// </summary>
        RedirectMessageM _RedirectMessage = new RedirectMessageM();
        /// <summary>
        /// リダイレクトメッセージ[RedirectMessage]プロパティ
        /// </summary>
        public RedirectMessageM RedirectMessage
        {
            get
            {
                return _RedirectMessage;
            }
            set
            {
                if (_RedirectMessage == null || !_RedirectMessage.Equals(value))
                {
                    _RedirectMessage = value;
                    NotifyPropertyChanged("RedirectMessage");
                }
            }
        }
        #endregion

        #region A111のプロセスを実行する
        /// <summary>
        /// A111のプロセスを実行する
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> RunCommand()
        {
            if (!ExecuteProcessF)
            {
                ExecuteProcessF = true;
                var curr_dir_path = GblValues.Instance.A1111Setting?.Item.CurrentDirectory;

                GblValues.Instance.A1111Proc = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                //info.Arguments = "/c " + $"python {curr_dir_path}\\launch.py --nowebui --xformers";//引数
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true; // コンソール・ウィンドウを開かない
                GblValues.Instance.A1111Proc.StartInfo = info;
                GblValues.Instance.A1111Proc.Start();

                using (StreamWriter sw = GblValues.Instance.A1111Proc.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine("cd {0}", curr_dir_path);
                        sw.WriteLine("python launch.py --nowebui --xformers");
                    }
                }
                string line;

                // プロセス実行中は常にコンソールの値を読み込み続ける
                while (ExecuteProcessF && (line = GblValues.Instance.A1111Proc.StandardOutput.ReadLine()!) != null)
                {
                    yield return line;
                }
            }
        }
        #endregion

        #region A1111プロセスの終了処理
        /// <summary>
        /// A1111プロセスの終了処理
        /// </summary>
        public static void WebUIProcessEnd()
        {
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
        #endregion

        #region WebUI A1111の実行
        /// <summary>
        /// WebUI A1111の実行
        /// </summary>
        public void WebUIExecute()
        {
            try
            {
                string file_path = Path.Combine(this.A1111Config.CurrentDirectory, "launch.py");

                // 未設定の場合
                if (string.IsNullOrEmpty(this.A1111Config.CurrentDirectory))
                {
                    return; // エラーを出さずに抜ける
                }

                // ファイルパスが見つからない場合
                if (!File.Exists(file_path))
                {
                    ShowMessage.ShowNoticeOK($"Not found {file_path}", "Notice");   // エラーを出して抜ける
                    return;
                }

                Task.Run(() =>
                {
                    try
                    {
                        foreach (var msg in RunCommand())
                        {
                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() =>
                            {
                                if (!string.IsNullOrEmpty(msg))
                                {
                                    this.RedirectMessage.Add(msg);
                                }

                                if (!ExecuteProcessF)
                                    return;
                            }));
                        }
                    }
                    catch
                    {
                        ExecuteProcessF = false;
                    }
                });
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion
    }
}
