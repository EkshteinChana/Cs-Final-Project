using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PL.PO;
/// <summary>
/// A PO entity of an item in the order 
/// (represents a row in the order) 
/// for a list of items in the shopping cart screen and in the order details screen
/// </summary>
public class OrderItem: DependencyObject
{

    public static readonly DependencyProperty idProperty = DependencyProperty.Register("Id", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));
    public static readonly DependencyProperty productIdProperty = DependencyProperty.Register("ProductId", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));
    public static readonly DependencyProperty nameProperty = DependencyProperty.Register("Name", typeof(string), typeof(OrderItem), new UIPropertyMetadata(""));    
    public static readonly DependencyProperty priceProperty = DependencyProperty.Register("Price", typeof(double), typeof(OrderItem), new UIPropertyMetadata(0.0));
    public static readonly DependencyProperty amountProperty = DependencyProperty.Register("Amount", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));
    public static readonly DependencyProperty totalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderItem), new UIPropertyMetadata(0.0));

    public int Id//OrderItem ID
    {
        get { return (int)GetValue(idProperty); }
        set { SetValue(idProperty, value); }
    }
    public int ProductId
    {
        get { return (int)GetValue(productIdProperty); }
        set { SetValue(productIdProperty, value); }
    }
    public string? Name//product's name
    {
        get { return (string)GetValue(nameProperty); }
        set { SetValue(nameProperty, value); }
    }
    public double Price//product's price
    {
        get { return (double)GetValue(priceProperty); }
        set { SetValue(priceProperty, value); }
    }
    public int Amount//Amount of items of a product in the cart/order
    {
        get { return (int)GetValue(amountProperty); }
        set { SetValue(amountProperty, value); }
    }
    public double TotalPrice//Total price of an item (according to product price and his quantity at the order/cart)
    {
        get { return (double)GetValue(totalPriceProperty); }
        set { SetValue(totalPriceProperty, value); }
    }

    public override string ToString() => $@"
            ID: {Id}
            name: {Name}
            productID: {ProductId}
            price: {Price}
            amount: {Amount}
            total price:{TotalPrice}";
}
