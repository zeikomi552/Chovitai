using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Converters
{
    /// <summary>
    /// NullやString.Emptyの場合Bool型のFalseを返却するコンバーター
    /// </summary>
    [System.Windows.Data.ValueConversion(typeof(object), typeof(bool))]
    public class NullToBooleanFalseConverter : System.Windows.Data.IValueConverter
    {

        #region IValueConverter メンバ
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var target = (object)value;
            if (target == null || target.ToString() == string.Empty)
            {
                // ここに処理を記述する
                return false;
            }
            else
            {
                return true;
            }
        }

        // TwoWayの場合に使用する
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
