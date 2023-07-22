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
    #region URLをブラウザで開くアクション 
    /// <summary> 
    /// URLをブラウザで開くアクション 
    /// </summary> 
    public class OpenURLAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty URLProperty =
        DependencyProperty.Register("URL", typeof(string), typeof(OpenURLAction), new UIPropertyMetadata());

        public string URL
        {
            get { return (string)GetValue(URLProperty); }
            set { SetValue(URLProperty, value); }
        }

        protected override void Invoke(object obj)
        {
            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo(URL);
                startInfo.UseShellExecute = true;
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
    }
    #endregion

}
