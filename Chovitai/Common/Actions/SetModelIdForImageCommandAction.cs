using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            GblValues.Instance.ImageSearchCondition.ModelId = (int)ModelId;
        }
    }
    #endregion

}
