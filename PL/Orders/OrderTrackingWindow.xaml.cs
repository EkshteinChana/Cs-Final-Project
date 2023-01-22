using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private IBl bl;
    private PO.OrderTracking ot;
    public OrderTrackingWindow(IBl Ibl,int Id)
    {
        InitializeComponent();
        bl = Ibl;
        BO.OrderTracking bOt = bl.Order.TrackOrder(Id);

    }
}
