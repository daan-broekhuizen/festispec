using Festispec.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Festispec.Utility.Converters
{
    public class ConfigurationToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableDictionary<EnumChartConfiguration, object> dictionary)
            {
                EnumChartConfiguration param = (EnumChartConfiguration)int.Parse((string)parameter);

                return dictionary.ContainsKey(param) && dictionary[param] != null ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
