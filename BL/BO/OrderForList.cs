namespace BO;

/// <summary>
/// An entity of order in list 
/// for the order list screen
/// </summary>
public class OrderForList
{
    public int Id { get; set; }//OrderId
    public string? CustomerName { get; set; }
    public eOrderStatus? status { get; set; }//the status of the order
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }//the total price of the order

    public override string ToString() => $@"
        order ID: {Id},
        customerName: {CustomerName},   
        status: {status},
        amountOfItems: {AmountOfItems}
        totalPrice: {TotalPrice}
        ";
}
