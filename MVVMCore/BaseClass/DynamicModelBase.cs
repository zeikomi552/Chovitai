using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.BaseClass
{
    public class DynamicModelBase<T> : DynamicObject, INotifyPropertyChanged
            where T : class
    {
        // Modelクラスのインスタンス
        private T innerModel;

        protected T InnerModel
        {
            get { return innerModel; }
        }

        public DynamicModelBase(T innerModel)
        {
            this.innerModel = innerModel;
        }

        // INotifyPropertyChangedの実装
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            var h = PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(name));
            }
        }

        // プロパティの取得
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var propertyName = binder.Name;
            var property = innerModel.GetType().GetProperty(propertyName);
            if (property == null || !property.CanRead)
            {
                // プロパティが存在しないか読み取りが出来ないので値の取得に失敗
                result = null;
                return false;
            }

            // プロパティから値を取得する
            result = property.GetValue(innerModel, null);
            return true;
        }

        // プロパティの設定
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyName = binder.Name;
            var property = innerModel.GetType().GetProperty(propertyName);
            if (property == null || !property.CanWrite)
            {
                return false;
            }

            // プロパティの値をセットしてイベントを発行する
            property.SetValue(innerModel, value, null);
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
