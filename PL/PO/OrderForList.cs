namespace PL.PO;

/// <summary>
/// An entity of order in list 
/// for the order list screen
/// </summary>
public class OrderForList {

    public int Id { get; set; }//orderId
    public string? CustomerName { get; set;  }      
    public eOrderStatus status { get; set; }//the CustomerNamestatus of the order
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; } //the total price of the order
    public override string ToString() => $@"
        order ID: {Id},
        customerName: {CustomerName},   
        status: {status},
        amountOfItems: {AmountOfItems}
        totalPrice: {TotalPrice}
        ";

}
