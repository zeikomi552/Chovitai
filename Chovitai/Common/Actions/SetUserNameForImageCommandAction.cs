using Chovitai.ViewModels;
using Microsoft.Xaml.Behaviors;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Chovitai.Common.Actions
{
    #region UserNameセットしImage検索を行うアクション 
    /// <summary> 
    /// UserNameセットしImage検索を行うアクション 
    /// </summary> 
    public class SetUserNameForImageCommandAction : TriggerAction<FrameworkElement>
    {

        public static readonly DependencyProperty UserNameProperty =
        DependencyProperty.Register("UserName", typeof(string), typeof(SetUserNameForImageCommandAction), new UIPropertyMetadata());

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        protected override void Invoke(object obj)
        {
            try
            {
                // ModelIdのセット
                GblValues.Instance.ImageSearchCondition.Username = (string)UserName;
                GblValues.Instance.ImageSearchCondition.ModelId = null;     // ModelIdのクリア

                var tmp = App.Current.MainWindow as MainWindow; // Mainwindowを取得

                // ViewModelを取得
                var vm = tmp!.ucSearchImage.DataContext as UcSearchImageVM;
                vm!.Search();   // 検索の実行

                // 一瞬待たせる
                System.Threading.Thread.Sleep(200);

                // タブの切替(Image検索画面へ遷移)
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
