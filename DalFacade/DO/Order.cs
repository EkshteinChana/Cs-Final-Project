
namespace DO;
public struct Order
{
    public readonly int Id { get; }
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

    public Order(int _id, string _customerName, string _customerEmail, string _customerAddress, DateTime _orderDate,DateTime _shipDate, DateTime _deliveryDate)
    {
        Id = _id;
        CustomerName = _customerName;
        CustomerEmail = _customerEmail;
        CustomerAddress = _customerAddress;
        OrderDate = _orderDate;
        ShipDate = _shipDate;
        DeliveryDate = _deliveryDate;
    }
}
