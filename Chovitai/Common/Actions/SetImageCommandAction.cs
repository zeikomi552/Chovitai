using Chovitai.ViewModels;
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
    public class SetImageCommandAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ParameterProperty =
       DependencyProperty.Register("Parameter", typeof(string), typeof(SetImageCommandAction), new UIPropertyMetadata());

        public string Parameter
        {
            get { return (string)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(string), typeof(SetImageCommandAction), new UIPropertyMetadata());

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        protected override void Invoke(object obj)
        {
            try
            {
                // 条件のクリア
                GblValues.Instance.ImageSearchCondition.ConditionClear();

                switch (Parameter.ToLower())
                {
                    case "username":
                        {
                            GblValues.Instance.ImageSearchCondition.Username = Value;    // UserNameのセット
                            break;
                        }
                    case "modelid":
                        {
                            GblValues.Instance.ImageSearchCondition.ModelId = int.Parse(Value);    // UserNameのセット
                            break;
                        }
                    case "modelversionid":
                        {
                            GblValues.Instance.ImageSearchCondition.ModelVersionId = int.Parse(Value);    // UserNameのセット
                            break;
                        }
                    default:
                        break;

                }

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
}
