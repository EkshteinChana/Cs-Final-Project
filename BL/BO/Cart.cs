using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Main Logical entity of shopping cart
/// for the shopping cart management screen and order confirmation
/// </summary>
public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public IEnumerable<OrderItem> Items { get; set; }//a list of the items in the shopping cart
    public double TotalPrice { get; set; }//the total price of the shopping cart

    public override string ToString() => $@"
        customerName: {CustomerName},
        customerEmail: {CustomerEmail},
        customerAddress: {CustomerAddress},
        items: {items},
        totalPrice: {TotalPrice}
        ";
}
