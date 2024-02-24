using Chovitai.Common.Utilities;
using Chovitai.Models;
using Chovitai.Views;
using Chovitai.Views.UserControls;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using SharpVectors.Renderers.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chovitai.ViewModels
{
    public class UcFileCheckVM : FileBaseVM
    {
        #region 新しいファイルができたらそのファイルを選択するフラグ[SelectNewFolderF]プロパティ
        /// <summary>
        /// 新しいファイルができたらそのファイルを選択するフラグ[SelectNewFolderF]プロパティ用変数
        /// </summary>
        bool _SelectNewFolderF = false;
        /// <summary>
        /// 新しいファイルができたらそのファイルを選択するフラグ[SelectNewFolderF]プロパティ
        /// </summary>
        public bool SelectNewFolderF
        {
            get
            {
                return _SelectNewFolderF;
            }
            set
            {
                if (!_SelectNewFolderF.Equals(value))
                {
                    _SelectNewFolderF = value;
                    NotifyPropertyChanged("SelectNewFolderF");
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

        #region スライドショーフラグ[IsSlideshow]プロパティ
        /// <summary>
        /// スライドショーフラグ[IsSlideshow]プロパティ用変数
        /// </summary>
        bool _IsSlideshow = false;
        /// <summary>
        /// スライドショーフラグ[IsSlideshow]プロパティ
        /// </summary>
        public bool IsSlideshow
        {
            get
            {
                return _IsSlideshow;
            }
            set
            {
                if (!_IsSlideshow.Equals(value))
                {
                    _IsSlideshow = value;
                    NotifyPropertyChanged("IsSlideshow");
                }
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
                var wnd = VisualTreeHelperWrapper.GetWindow<UcFileCheckV>(sender) as UcFileCheckV;

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

        #region スライドショーの切替
        /// <summary>
        /// スライドショーの切替
        /// </summary>
        public void Slideshow()
        {
            Task.Run(() =>
            {
                // 画像が1つ以上あるかを確認
                if (this.FileList.Count <= 0)
                {
                    this.IsSlideshow = false;
                    return;
                }

                // nullチェック
                if (this.FileList.SelectedItem == null)
                {
                    this.FileList.SelectedItem = this.FileList.First();
                }

                // 音声再生インデックス取得
                int index = this.FileList.IndexOf(this.FileList.SelectedItem);

                while (this.IsSlideshow)
                {
                    if (this.FileList.Count <= 0)
                    {
                        this.IsSlideshow = false;
                        return;
                    }

                    if (index < this.FileList.Count)
                    {
                        var tmp = this.FileList.ElementAt(index);

                        this.FileList.SelectedItem = tmp;
                        System.Threading.Thread.Sleep(3000);
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
            });
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
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<MainWindow>(sender) as MainWindow;

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion


        #region ファイルの削除処理
        /// <summary>
        /// ファイルの削除処理
        /// </summary>
        public void DeleteFile()
        {
            try
            {
                File.Delete(this.FileList.SelectedItem.FilePath);
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
                                    RenewDirectory(this.DirectoryPath);
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
