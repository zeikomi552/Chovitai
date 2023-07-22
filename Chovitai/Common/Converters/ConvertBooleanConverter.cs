using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Common.Converters
{
    [System.Windows.Data.ValueConversion(typeof(bool), typeof(bool))]
    public class ConvertBooleanConverter : System.Windows.Data.IValueConverter
    {

        #region IValueConverter メンバ
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        // TwoWayの場合に使用する
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
        #endregion
    }

}
