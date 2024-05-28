using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Supermarket.Model;

namespace Supermarket.Converters
{
    public class UserConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length >= 3 && values[0] != null && values[1] != null && values[2] != null)
            {
                return new User()
                {
                    NumeUtilizator = values[0].ToString(),
                    Parola = values[1].ToString(),
                    TipUtilizator = values[2].ToString()
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
