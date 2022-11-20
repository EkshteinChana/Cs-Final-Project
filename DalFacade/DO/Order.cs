
namespace DO;
public struct Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    
    public override string ToString() => $@"
        order ID: {Id},
        customerName: {CustomerName},
        customerEmail: {CustomerEmail},
        customerAddress: {CustomerAddress},
    	orderDate: {OrderDate},
    	shipDate: {ShipDate},
    	deliveryDate: {DeliveryDate}
        ";
}
