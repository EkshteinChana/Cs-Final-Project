
using BlApi;
using System.Diagnostics;

namespace BO;

/// <summary>
/// Main logical entity of order 
/// for order detail screens and actions on a order
/// </summary>
public class Order
{
    public int Id { get; set; }//orderId
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public eOrderStatus? status { get; set; }//the status of this order
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public IEnumerable<BO.OrderItem?> Items { get; set; } //a list of the items in this order 
    public double TotalPrice { get; set; }//the total price of this order

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
