using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO;
/// <summary>
/// PO Order Tracking entity for the Order Tracking screen
/// </summary>
public class OrderTracking: DependencyObject
{
    public static readonly DependencyProperty idProperty = DependencyProperty.Register("Id", typeof(int), typeof(OrderTracking), new UIPropertyMetadata(0));
    public static readonly DependencyProperty statusProperty = DependencyProperty.Register("Status", typeof(BO.eOrderStatus), typeof(OrderTracking), new UIPropertyMetadata(BO.eOrderStatus.confirmed));
    public static readonly DependencyProperty orderStatusByDateProperty = DependencyProperty.Register("OrderStatusByDate", typeof(ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>), typeof(OrderTracking), new UIPropertyMetadata( new ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>()));

    public int Id
    {
        get { return (int)GetValue(idProperty); }
        set { SetValue(idProperty, value); }
    }
    public BO.eOrderStatus? Status
    {
        get { return (BO.eOrderStatus)GetValue(statusProperty); }
        set { SetValue(statusProperty, value); }
    }
    public ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>? OrderStatusByDate//List of pairs (date, description of order progress )
    {
        get { return (ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>?)GetValue(orderStatusByDateProperty); }
        set { SetValue(orderStatusByDateProperty, value); }
    }

    public override string ToString() => $@"
        ID: {Id}
        status: {Status}
        ";
}


