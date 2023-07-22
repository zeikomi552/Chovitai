using Chovitai.Models;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.ViewModels
{
    public class PromptWindowVM : ViewModelBase
    {
        #region プロンプト情報[PromptItems]プロパティ
        /// <summary>
        /// プロンプト情報[PromptItems]プロパティ用変数
        /// </summary>
        PromptCountCollectionM? _PromptItems;
        /// <summary>
        /// プロンプト情報[PromptItems]プロパティ
        /// </summary>
        public PromptCountCollectionM? PromptItems
        {
            get
            {
                return _PromptItems;
            }
            set
            {
                if (_PromptItems == null || !_PromptItems.Equals(value))
                {
                    _PromptItems = value;
                    NotifyPropertyChanged("PromptItems");
                }
            }
        }
        #endregion

        #region プロンプト作成用変数[PromptText]プロパティ
        /// <summary>
        /// プロンプト作成用変数[PromptText]プロパティ用変数
        /// </summary>
        string _PromptText = string.Empty;
        /// <summary>
        /// プロンプト作成用変数[PromptText]プロパティ
        /// </summary>
        public string PromptText
        {
            get
            {
                return _PromptText;
            }
            set
            {
                if (_PromptText == null || !_PromptText.Equals(value))
                {
                    _PromptText = value;
                    NotifyPropertyChanged("PromptText");
                }
            }
        }
        #endregion

        #region ネガティブプロンプト作成用変数[NegativePromptText]プロパティ
        /// <summary>
        /// ネガティブプロンプト作成用変数[NegativePromptText]プロパティ用変数
        /// </summary>
        string _NegativePromptText = string.Empty;
        /// <summary>
        /// ネガティブプロンプト作成用変数[NegativePromptText]プロパティ
        /// </summary>
        public string NegativePromptText
        {
            get
            {
                return _NegativePromptText;
            }
            set
            {
                if (_NegativePromptText == null || !_NegativePromptText.Equals(value))
                {
                    _NegativePromptText = value;
                    NotifyPropertyChanged("NegativePromptText");
                }
            }
        }
        #endregion



        public override void Init(object sender, EventArgs e)
        {
            try
            {
                this.PromptItems = new PromptCountCollectionM();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

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

        #region プロンプトテキストへのセット
        /// <summary>
        /// プロンプトテキストへのセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetPromptText()
        {
            try
            {

                if (this.PromptItems != null && this.PromptItems.PromptItems != null &&
                     this.PromptItems.PromptItems.SelectedItem != null)
                {
                    if (string.IsNullOrEmpty(this.PromptText))
                    {
                        this.PromptText += this.PromptItems.PromptItems.SelectedItem.Prompt;
                    }
                    else
                    {
                        this.PromptText += ", " + this.PromptItems.PromptItems.SelectedItem.Prompt;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ネガティブプロンプトテキストへのセット
        /// <summary>
        /// ネガティブプロンプトテキストへのセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetNegativePromptText()
        {
            try
            {

                if (this.PromptItems != null && this.PromptItems.NegativePromptItems != null &&
                     this.PromptItems.NegativePromptItems.SelectedItem != null)
                {
                    if (string.IsNullOrEmpty(this.NegativePromptText))
                    {
                        this.NegativePromptText += this.PromptItems.NegativePromptItems.SelectedItem.Prompt;
                    }
                    else
                    {
                        this.NegativePromptText += ", " + this.PromptItems.NegativePromptItems.SelectedItem.Prompt;
                    }
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
