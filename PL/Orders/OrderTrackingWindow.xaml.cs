using BlApi;
using System;
using System.Collections.ObjectModel;
using System.Windows;

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
    /// constractor of OrderTrackingWindow which imports OrderTracking entity for a specific order
    /// </summary>
    public OrderTrackingWindow(IBl Ibl, int Id)
    {
        InitializeComponent();
        bl = Ibl;
        try
        {
            orderId = Id;
            BO.OrderTracking bOt = bl.Order.TrackOrder(Id);
            ot = convertBoOrdTrckToPoOrdTrck(bOt);
            DataContext = ot;
            Simulator.Simulator.registerChangeStatusEvent(refreshStatus);
        }       
        catch (InvalidValueException exc)
        {          
            throw exc;
        }
        catch (DataErrorException dataError)
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
        Simulator.Simulator.unregisterChangeStatusEvent(refreshStatus);
        Close();
    }

    /// <summary>
    /// A function that opens the OrderWindow for watching the details of the current order.
    /// </summary>
    private void OrderDetailsBtn_Click(object sender, RoutedEventArgs e)
    {       
        new OrderWindow(bl, this, ot.Id).Show();
        Hide();
    }

    /// <summary>
    /// A function that update the order in the screan if its status changed in the simulator. 
    /// </summary>
    private void refreshStatus(object sender, EventArgs e)
    {
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(refreshStatus, sender, e);
        }
        else
        {
            int? oId = (e as Simulator.Num)?.id ?? null;
            if (oId == orderId)
            {
                try
                {
                    BO.OrderTracking bOt = bl.Order.TrackOrder(orderId);
                    ot = convertBoOrdTrckToPoOrdTrck(bOt);
                    DataContext = ot;
                }
                catch (InvalidValueException exc)
                {
                    throw exc;
                }
                catch (DataErrorException dataError)
                {
                    throw dataError;
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        } 
    }

    /// <summary>
    /// A private help function to convert Bo.OrderTracking entity to PO.OrderEntity entity.
    /// </summary>
    private PO.OrderTracking convertBoOrdTrckToPoOrdTrck(BO.OrderTracking bOt)
    {
        PO.OrderTracking pOt = new()
        {
            Id = bOt.Id,
            Status = (BO.eOrderStatus?)bOt.status,
            OrderStatusByDate = new ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>(bOt.OrderStatusByDate)
        };
        return pOt;
    }
}

