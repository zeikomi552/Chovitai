using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.BaseClass
{
    public abstract class ViewModelBase : INotifyPropertyChanged
	{
		#region ダイアログ結果[DialogResult]プロパティ
		/// <summary>
		/// ダイアログ結果[DialogResult]プロパティ用変数
		/// </summary>
		bool? _DialogResult = null;
		/// <summary>
		/// ダイアログ結果[DialogResult]プロパティ
		/// </summary>
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				if (_DialogResult == null || !_DialogResult.Equals(value))
				{
					_DialogResult = value;
					NotifyPropertyChanged("DialogResult");
				}
			}
		}
		#endregion

		#region 初期化処理
		/// <summary>
		/// 初期化処理
		/// </summary>
		public abstract void Init(object sender, EventArgs e);
		#endregion

		#region クローズ処理
		/// <summary>
		/// クローズ処理
		/// </summary>
		public abstract void Close(object sender, EventArgs e);
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
