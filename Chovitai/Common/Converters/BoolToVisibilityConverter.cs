using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chovitai.Common.Converters
{
    [System.Windows.Data.ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : System.Windows.Data.IValueConverter
    {

        #region IValueConverter メンバ
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var target = (bool)value;
            // ここに処理を記述する
            return target ? Visibility.Visible : Visibility.Collapsed;
        }

        // TwoWayの場合に使用する
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
