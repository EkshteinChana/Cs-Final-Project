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

    public OrderTrackingWindow(IBl Ibl, int Id)
    {
        bl = Ibl;
        try {           
            BO.OrderTracking bOt = bl.Order.TrackOrder(Id);
            InitializeComponent();
            ot = convertBoOrdTrckToPoOrdTrck(bOt);
            DataContext = ot;
        }
        catch (InvalidValue exc)
        {
            MessageBox.Show(exc.Message);
            new MainWindow();
            this.Close();
        }
        catch (DataError dataError)
        {
            MessageBox.Show(dataError.Message + " " + dataError?.InnerException?.Message);
            new MainWindow();
            this.Close();
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message);
            new MainWindow();
            this.Close();
        }
    }
    private void ReturnToCatalogBtn_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
    private void OrderDetailsBtn_Click(object sender, RoutedEventArgs e)
    {
        new OrderWindow(bl, this, ot.Id).Show();
        this.Hide();
    }
}

