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
/// An entity of order in list 
/// for the order list screen
/// </summary>
public class OrderForList : DependencyObject
{
    public static readonly DependencyProperty idProperty = DependencyProperty.Register("Id", typeof(int), typeof(OrderForList), new UIPropertyMetadata(0));
    public static readonly DependencyProperty customerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(OrderForList), new UIPropertyMetadata(""));
    public static readonly DependencyProperty statusProperty = DependencyProperty.Register("status", typeof(BO.eOrderStatus), typeof(OrderForList), new UIPropertyMetadata(BO.eOrderStatus.confirmed));
    public static readonly DependencyProperty amountOfItemsProperty = DependencyProperty.Register("AmountOfItems", typeof(int), typeof(OrderForList), new UIPropertyMetadata(0));
    public static readonly DependencyProperty totalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderForList), new UIPropertyMetadata(0.0));


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
    public eOrderStatus? status//the status of the order
    {
        get { return (BO.eOrderStatus)GetValue(statusProperty); }
        set { SetValue(statusProperty, value); }
    }
    public int AmountOfItems
    {
        get { return (int)GetValue(amountOfItemsProperty); }
        set { SetValue(amountOfItemsProperty, value); }
    }
    public double TotalPrice//the total price of the order
    {
        get { return (double)GetValue(totalPriceProperty); }
        set { SetValue(totalPriceProperty, value); }
    }

    public override string ToString() => $@"
        order ID: {Id},
        customerName: {CustomerName},   
        status: {status},
        amountOfItems: {AmountOfItems}
        totalPrice: {TotalPrice}
        ";
}