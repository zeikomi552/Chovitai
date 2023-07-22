using Microsoft.Xaml.Behaviors;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chovitai.Common.Actions
{
    #region Clipboardにコピーするアクションアクション 
    /// <summary> 
    /// Clipboardにコピーするアクションアクション 
    /// </summary> 
    public class ClipboardCopyAction : TriggerAction<FrameworkElement>
    {

        public static readonly DependencyProperty CopyTextProperty =
        DependencyProperty.Register("CopyText", typeof(string), typeof(ClipboardCopyAction), new UIPropertyMetadata());

        public string CopyText
        {
            get { return (string)GetValue(CopyTextProperty); }
            set { SetValue(CopyTextProperty, value); }
        }

        protected override void Invoke(object obj)
        {
            try
            {
                if (CopyText != null)
                {
                    Clipboard.SetText(CopyText);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
    }
    #endregion

}
