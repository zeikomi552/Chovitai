using Chovitai.Common.Utilities;
using Chovitai.Models;
using Chovitai.Views.UserControls;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chovitai.ViewModels
{
    public class UcFileCheckVM : ViewModelBase
    {
        #region ディレクトリ読み込み処理実行中[ExecuteReadDirF]プロパティ
        /// <summary>
        /// ディレクトリ読み込み処理実行中[ExecuteReadDirF]プロパティ用変数
        /// </summary>
        bool _ExecuteReadDirF = false;
        /// <summary>
        /// ディレクトリ読み込み処理実行中[ExecuteReadDirF]プロパティ
        /// </summary>
        public bool ExecuteReadDirF
        {
            get
            {
                return _ExecuteReadDirF;
            }
            set
            {
                if (!_ExecuteReadDirF.Equals(value))
                {
                    _ExecuteReadDirF = value;
                    NotifyPropertyChanged("ExecuteReadDirF");
                }
            }
        }
        #endregion


        #region 読み込んだディレクトリ[DirectoryPath]プロパティ
        /// <summary>
        /// 読み込んだディレクトリ[DirectoryPath]プロパティ用変数
        /// </summary>
        string _DirectoryPath = string.Empty;
        /// <summary>
        /// 読み込んだディレクトリ[DirectoryPath]プロパティ
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return _DirectoryPath;
            }
            set
            {
                if (_DirectoryPath == null || !_DirectoryPath.Equals(value))
                {
                    _DirectoryPath = value;
                    NotifyPropertyChanged("DirectoryPath");
                }
            }
        }
        #endregion

        #region ファイルリスト[FileList]プロパティ
        /// <summary>
        /// ファイルリスト[FileList]プロパティ用変数
        /// </summary>
        ModelList<FileInfoM> _FileList = new ModelList<FileInfoM>();
        /// <summary>
        /// ファイルリスト[FileList]プロパティ
        /// </summary>
        public ModelList<FileInfoM> FileList
        {
            get
            {
                return _FileList;
            }
            set
            {
                if (_FileList == null || !_FileList.Equals(value))
                {
                    _FileList = value;
                    NotifyPropertyChanged("FileList");
                }
            }
        }
        #endregion

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

        #region ディレクトリ内のpngファイルを全て読み込む
        /// <summary>
        /// ディレクトリ内のpngファイルを全て読み込む
        /// </summary>
        public void ReadDirectory()
        {
            try
            {
                // ディレクトリを開く
                if (OpenDirectory())
                {
                    // ディレクトリ内のpngファイルの読み込み
                    ReadDirectory(this.DirectoryPath);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
            finally
            {
            }
        }
        #endregion

        #region ディレクトリ読み出しの更新処理
        /// <summary>
        /// ディレクトリ読み出しの更新処理
        /// </summary>
        public void RenewDirectory()
        {
            try
            {
                // ディレクトリを開く
                if (Directory.Exists(this.DirectoryPath))
                {
                    // ディレクトリ内のpngファイルの読み込み
                    ReadDirectory(this.DirectoryPath);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
            finally
            {
            }
        }
        #endregion

        #region ディレクトリを開く処理
        /// <summary>
        /// ディレクトリを開く処理
        /// </summary>
        private bool OpenDirectory()
        {
            using (var cofd = new CommonOpenFileDialog()
            {
                Title = "フォルダを選択してください",
                // フォルダ選択モードにする
                IsFolderPicker = true,
            })
            {
                // フォルダ選択ダイアログを開く
                if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    this.DirectoryPath = cofd.FileName; // フォルダパスのセット
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region ディレクトリのファイル全て読み込み
        /// <summary>
        /// ディレクトリのファイル全て読み込み
        /// </summary>
        /// <param name="dir">ディレクトリパス</param>
        private void ReadDirectory(string dir)
        {
            try
            {
                // ファイル情報のセット
                this.FileList.Items.Clear();

                Task.Run(() =>
                {
                    // 読み込み中の場合は無視
                    if (this.ExecuteReadDirF)
                        return;

                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.ExecuteReadDirF = true;    // 読み込み処理実行
                        }));

                    // フォルダ内のファイル一覧を取得
                    var fileArray = Directory.GetFiles(dir, "*.png");
                    foreach (string file in fileArray)
                    {
                        // バイナリで開く
                        using (var reader = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read)))
                        {
                            // Pngファイルのシグニチャ読み込み
                            if (PngReader.ReadPngSignature(reader))
                            {
                                var ihdrchunk = PngReader.ReadChunk(reader);    // IHDチャンクの読み込み
                                var itextchunk = PngReader.ReadChunk(reader);   // ITextチャンクの読み込み

                                // データがutf - 8の場合
                                var msg = System.Text.Encoding.UTF8.GetString(itextchunk.ChunkData).Replace("\0", ":");

                                var msg_list = msg.Split("\n"); // 分割
                                var prompt = msg_list.ElementAt(0).Replace("parameters:", "");  // Parameterの文字を消す

                                var file_info = new FileInfoM() { FilePath = file, ImageText = msg, Prompt = prompt, BasePrompt = prompt.Split(",").Last().Trim() };

                                // スレッドセーフの呼び出し
                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                    new Action(() =>
                                    {
                                        this.FileList.Items.Add(file_info);
                                    }));
                            }
                        }
                    }
                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.FileList.SelectedLast();
                        }));

                    // スレッドセーフの呼び出し
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            this.ExecuteReadDirF = false;    // 読み込み処理終了
                        }));

                });
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void OutputMarkdown()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "マークダウン (*.md)|*.md";

                string img_dir = string.Empty;
                string mk_filename = string.Empty;
                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    MarkdownM.OutputMarkdown(this.FileList.Items.ToList<FileInfoM>(), this.DirectoryPath, dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region キー入力処理の受付
        /// <summary>
        /// キー入力処理の受付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDown(object sender, EventArgs e)
        {
            try
            {
                var wnd = sender as UcFileCheckV;

                if (wnd != null)
                {
                    var key_eve = e as KeyEventArgs;

                    if (key_eve!.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || key_eve!.KeyboardDevice.IsKeyDown(Key.RightCtrl))
                    {
                        switch (key_eve.Key)
                        {
                            case Key.Left:
                                {

                                    break;
                                }
                            case Key.Right:
                                {

                                    break;
                                }

                        }
                    }

                    switch (key_eve.Key)
                    {
                        case Key.Enter:
                            {

                                break;
                            }
                        case Key.F5:
                            {
                                if (!this.ExecuteReadDirF)
                                {
                                    // フォルダの更新
                                    RenewDirectory();
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception ev)
            {
                ShowMessage.ShowErrorOK(ev.Message, "Error");
            }
        }
        #endregion
    }
}
