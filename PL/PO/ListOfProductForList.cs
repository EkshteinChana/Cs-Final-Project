using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO;

internal class ListOfProductForList: DependencyObject
{
    public static readonly DependencyProperty listProperty = DependencyProperty.Register("List", typeof(ObservableCollection<PO.ProductForList?>), typeof(ListOfProductForList), new UIPropertyMetadata(new ObservableCollection<PO.ProductForList?>()));
    public ObservableCollection<PO.ProductForList?>List//the list of the products
    {
        get { return (ObservableCollection<PO.ProductForList?>)GetValue(listProperty); }
        set { SetValue(listProperty, value); }
    }
}
