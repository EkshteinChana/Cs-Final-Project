using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;


namespace PL.PO;
/// <summary>
/// PO entity of order 
/// for order detail screens and actions on a order
/// </summary>
public class Order: DependencyObject
{
    public static readonly DependencyProperty idProperty = DependencyProperty.Register("Id", typeof(int), typeof(Order), new UIPropertyMetadata(0));
    public static readonly DependencyProperty customerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(Order), new UIPropertyMetadata(""));
    public static readonly DependencyProperty customerEmailProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(Order), new UIPropertyMetadata(""));
    public static readonly DependencyProperty customerAddressProperty = DependencyProperty.Register("CustomerAddress", typeof(string), typeof(Order), new UIPropertyMetadata(""));
    public static readonly DependencyProperty orderDateProperty = DependencyProperty.Register("OrderDate", typeof(DateTime), typeof(Order), new UIPropertyMetadata(null));
    public static readonly DependencyProperty shipDateProperty = DependencyProperty.Register("ShipDate", typeof(DateTime), typeof(Order), new UIPropertyMetadata(null));
    public static readonly DependencyProperty deliveryDateProperty = DependencyProperty.Register("DeliveryDate", typeof(DateTime), typeof(Order), new UIPropertyMetadata(null));
    public static readonly DependencyProperty totalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(Order), new UIPropertyMetadata(0.0));
    public static readonly DependencyProperty statusProperty = DependencyProperty.Register("status", typeof(PO.eOrderStatus), typeof(Order), new UIPropertyMetadata(PO.eOrderStatus.confirmed));
    public static readonly DependencyProperty itemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<PO.OrderItem?>), typeof(Order), new UIPropertyMetadata(new ObservableCollection<PO.OrderItem?>()));


    public int Id//orderId
    {
        get { return (int)GetValue(idProperty); }
        set { SetValue(idProperty, value); }
    }
    public string? CustomerName
    {
        get { return (string)GetValue(customerNameProperty); }
        set { SetValue(customerNameProperty, value); }
    }
    public string? CustomerEmail
    {
        get { return (string)GetValue(customerEmailProperty); }
        set { SetValue(customerEmailProperty, value); }
    }
    public string? CustomerAddress
    {
        get { return (string)GetValue(customerAddressProperty); }
        set { SetValue(customerAddressProperty, value); }
    }
    public DateTime? OrderDate
    {
        get { return (DateTime)GetValue(orderDateProperty); }
        set { SetValue(orderDateProperty, value); }
    }
    public DateTime? ShipDate
    {
        get { return (DateTime)GetValue(shipDateProperty); }
        set { SetValue(shipDateProperty, value); }
    }
    public DateTime? DeliveryDate
    {
        get { return (DateTime)GetValue(deliveryDateProperty); }
        set { SetValue(deliveryDateProperty, value); }
    }
    public PO.eOrderStatus? status//the status of this order
    {
        get { return (PO.eOrderStatus)GetValue(statusProperty); }
        set { SetValue(statusProperty, value); }
    }
    public double TotalPrice//the total price of this order
    {
        get { return (double)GetValue(totalPriceProperty); }
        set { SetValue(totalPriceProperty, value); }
    }
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