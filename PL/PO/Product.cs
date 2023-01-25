using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PL.PO;
/// <summary>
/// PO entity of product 
/// for product detail screens (for manager) 
/// and actions on a product
/// </summary>
public class Product : DependencyObject
{

    public static readonly DependencyProperty idProperty = DependencyProperty.Register("Id", typeof(int), typeof(Product), new UIPropertyMetadata(0));
    public static readonly DependencyProperty nameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Product), new UIPropertyMetadata(""));
    public static readonly DependencyProperty priceProperty = DependencyProperty.Register("Price", typeof(double), typeof(Product), new UIPropertyMetadata(0.0));
    public static readonly DependencyProperty categoryProperty = DependencyProperty.Register("Category", typeof(BO.eCategory), typeof(Product), new UIPropertyMetadata(BO.eCategory.Others));
    public static readonly DependencyProperty inStockProperty = DependencyProperty.Register("InStock", typeof(int), typeof(Product), new UIPropertyMetadata(0));
    public int Id//product id
    {
        get { return (int)GetValue(idProperty); }
        set { SetValue(idProperty, value); }
    }
    public string? Name
    {
        get { return (string)GetValue(nameProperty); }
        set { SetValue(nameProperty, value); }
    }
    public double Price
    {
        get { return (double)GetValue(priceProperty); }
        set { SetValue(priceProperty, value); }
    }
    public BO.eCategory? Category
    {
        get { return (BO.eCategory)GetValue(categoryProperty); }
        set { SetValue(categoryProperty, value); }
    }

    public int InStock//amount in stock
    {
        get { return (int)GetValue(inStockProperty); }
        set { SetValue(inStockProperty, value); }
    }

    public override string ToString() => $@"
        ID: {Id}
        name: {Name} 
        price: {Price}
        category: {Category}
    	amount in stock: {InStock}
        ";
}
