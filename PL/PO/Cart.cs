using System.Collections.ObjectModel;

namespace PL.PO;
/// <summary>
/// A PO entity of shopping cart
/// for the shopping cart management screen and order confirmation
/// </summary>
public class Cart 
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public ObservableCollection<PO.OrderItem?> Items { get; set; }//a list of the items in the shopping cart 
    public double TotalPrice { get; set; }//the total price of the shopping cart
    public override string ToString() => $@"
        customerName: {CustomerName},
        customerEmail: {CustomerEmail},
        customerAddress: {CustomerAddress},
        items: {Items},
        totalPrice: {TotalPrice}
        ";

    public Cart()
    {
        Items = new();
    }
}
