using BO;
using System;
using System.Collections.Generic;
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
    public static readonly DependencyProperty statusProperty = DependencyProperty.Register("status", typeof(BO.eOrderStatus), typeof(OrderTracking), new UIPropertyMetadata(BO.eOrderStatus.confirmed));
    public static readonly DependencyProperty orderStatusByDateProperty = DependencyProperty.Register("OrderStatusByDate", typeof(Dictionary<DateTime, eOrderStatus?>), typeof(OrderTracking), new UIPropertyMetadata( new Dictionary<DateTime, eOrderStatus?>()));

    public int Id
    {
        get { return (int)GetValue(idProperty); }
        set { SetValue(idProperty, value); }
    }
    public eOrderStatus? status
    {
        get { return (PO.eOrderStatus)GetValue(statusProperty); }
        set { SetValue(statusProperty, value); }
    }
    public Dictionary<DateTime, eOrderStatus?> OrderStatusByDate//List of pairs (date, description of order progress )
    {
        get { return (Dictionary<DateTime, eOrderStatus?>)GetValue(orderStatusByDateProperty); }
        set { SetValue(orderStatusByDateProperty, value); }
    }

    public override string ToString() => $@"
        ID: {Id}
        status: {status}
        ";
}


