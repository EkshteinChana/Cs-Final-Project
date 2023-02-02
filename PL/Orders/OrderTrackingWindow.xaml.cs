using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private int orderId;
    /// <summary>
    /// A private help function to convert Bo.OrderTracking entity to PO.OrderEntity entity.
    /// </summary>
    private PO.OrderTracking convertBoOrdTrckToPoOrdTrck(BO.OrderTracking bOt)
    {
        PO.OrderTracking pOt = new()
        {
            Id = bOt.Id,
            Status = (BO.eOrderStatus ?)bOt.status,
            OrderStatusByDate = new ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>(bOt.OrderStatusByDate)
        };
        return pOt;
    }
    /// <summary>
    /// constractor of OrderTrackingWindow which imports OrderTracking entity for a specific order
    /// </summary>
    public OrderTrackingWindow(IBl Ibl, int Id,Window w)
    {
        bl = Ibl;
        try {
            orderId = Id;
            BO.OrderTracking bOt = bl.Order.TrackOrder(Id);
            InitializeComponent();
            ot = convertBoOrdTrckToPoOrdTrck(bOt);
            DataContext = ot;
        }
        catch (InvalidValue exc)
        {          
            throw exc;
        }
        catch (DataError dataError)
        {
            throw dataError;
        }
        catch (Exception exc)
        {
            throw exc;
        }
    }
    /// <summary>
    /// A function for returning to the ProductCatalogWindow.
    /// </summary>
    private void ReturnToMainWindowBtn_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
    /// <summary>
    /// A function that opens the OrderWindow for watching the details of the current order.
    /// </summary>
    private void OrderDetailsBtn_Click(object sender, RoutedEventArgs e)
    {
        BO.Order bo = bl.Order.ReadOrd(orderId);
        //po = convertBoOrdToPoOrd(bo);
        PO.Cart cart = new()
        {
            CustomerName = bo.CustomerName,
            CustomerEmail= bo.CustomerEmail,    
            CustomerAddress= bo.CustomerAddress,    
            TotalPrice= bo.TotalPrice
        };   

        new Cart.CartWindow(bl, this, cart);
        //new OrderWindow(bl, this, ot.Id).Show();
        Hide();
    }
}

