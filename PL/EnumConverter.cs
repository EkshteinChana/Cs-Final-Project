using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
namespace PL;

public class EnumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string EnumString;
        try
        {
            EnumString = PO.eOrderStatus.GetName((((value as PL.PO.OrderForList).status).GetType()),(value as PL.PO.OrderForList)?.status ?? throw new Exception());
            return EnumString;
        }
        catch
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
