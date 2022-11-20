using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// An entity of an item in the order 
/// (represents a row in the order) 
/// for a list of items in the shopping cart screen and in the order details screen
/// </summary>
public class OrderItem
{
    public int Id { get; set; }//OrderItem ID
    public int ProductId { get; set; }
    public string Name { get; set; }//product's name
    public double Price { get; set; }//product's price
    public int Amount { get; set; }//Amount of items of a product in the cart/order
    public double TotalPrice { get; set; }//Total price of an item (according to product price and his quantity at the order/cart)

    public override string ToString() => $@"
            ID: {Id}
            name: {Name}
            productID: {ProductId}
            price: {Price}
            amount: {Amount}
            total price:{TotalPrice}";
}
