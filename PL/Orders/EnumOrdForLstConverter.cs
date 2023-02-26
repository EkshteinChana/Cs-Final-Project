using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Orders;
public class EnumOrdForLstConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string EnumString;
        try
        {
            EnumString = Enum.GetName((value as PO.OrderForList).status.GetType(), (value as PO.OrderForList)?.status ?? throw new Exception());
            return EnumString;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
