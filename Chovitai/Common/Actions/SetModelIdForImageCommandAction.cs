using Chovitai.ViewModels;
using Chovitai.Views.UserControls;
using Microsoft.Xaml.Behaviors;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Chovitai.Common.Actions
{
    #region ModelIdをセットするアクション 
    /// <summary> 
    /// ModelIdをセットするアクション 
    /// </summary> 
    public class SetModelIdForImageCommandAction : TriggerAction<FrameworkElement>
    {

        public static readonly DependencyProperty ModelIdProperty =
        DependencyProperty.Register("ModelId", typeof(int), typeof(SetModelIdForImageCommandAction), new UIPropertyMetadata());

        public int ModelId
        {
            get { return (int)GetValue(ModelIdProperty); }
            set { SetValue(ModelIdProperty, value); }
        }

        protected override void Invoke(object obj)
        {
            try
            {
                GblValues.Instance.ImageSearchCondition.ModelId = (int)ModelId;
                var tmp = App.Current.MainWindow as MainWindow;

                var vm = tmp!.ucSearchImage.DataContext as UcSearchImageVM;
                vm!.Search();

                System.Threading.Thread.Sleep(100);
                tmp!.Maintab.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
    }
    #endregion

}
