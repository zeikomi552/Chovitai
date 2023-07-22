using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.BaseClass
{
    public class ModelBase : INotifyPropertyChanged
    {
		#region シャローコピー
		/// <summary>
		/// シャローコピー
		/// </summary>
		/// <returns></returns>
		public T ShallowCopy<T>()
		{
			return (T)MemberwiseClone();
		}
        #endregion

        #region Clone処理
        /// <summary>
        /// Clone処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">クローン元</param>
        /// <param name="target">クローン先</param>
        public static void Clone<T>(T source, T target)
		{

			// プロパティの一覧を取得
			PropertyInfo[] propertyInfos = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propertyInfos)
            {
                var tmp = prop.GetValue(source, null);
                prop.SetValue(target, tmp, null);
            }
        }
		#endregion

		#region INotifyPropertyChanged 
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}
