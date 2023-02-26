using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL.PO;
/// <summary>
/// PO entity of order 
/// for order detail screens and actions on a order
/// </summary>
public class Order : DependencyObject
{
    public static readonly DependencyProperty itemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<PO.OrderItem?>), typeof(Order), new UIPropertyMetadata(new ObservableCollection<PO.OrderItem?>()));

    public int Id { get; set; }//orderId
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public PO.eOrderStatus status { get; set; }//the status of this order
    public double TotalPrice { get; set; }//the total price of this order
    public ObservableCollection<PO.OrderItem?> Items//a list of the items in this order 
    {
        get { return (ObservableCollection<PO.OrderItem?>)GetValue(itemsProperty); }
        set { SetValue(itemsProperty, value); }
    }
    public override string ToString()
    {
        string ordToString =
        $@"order ID: {Id},
        customerName: {CustomerName},
        customerEmail: {CustomerEmail},
        customerAddress: {CustomerAddress},
    	orderDate: {OrderDate},
        status: {status},
    	shipDate: {ShipDate},
    	deliveryDate: {DeliveryDate},
        the order items:
       ";
        Items.Select(itm =>
        {
            ordToString += $"{itm},\n";
            return itm;
        }).ToList();
        return ordToString;
    }
}