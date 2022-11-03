
namespace DO;
public struct Order
{
    public readonly int id { get; }
    public string customerName { get; set; }
    public string customerEmail { get; set; }
    public string customerAddress { get; set; }
    public DateTime orderDate { get; set; }
    public DateTime shipDate { get; set; }
    public DateTime deliveryDate { get; set; }
    
    public override string ToString() => $@"
        order ID: {id},
        customerName: {customerName},
        customerEmail: {customerEmail},
        customerAddress: {customerAddress},
    	orderDate: {orderDate},
    	shipDate: {shipDate},
    	deliveryDate: {deliveryDate}
        ";

    public Order(int _id, string _customerName, string _customerEmail, string _customerAddress, DateTime _orderDate,DateTime _shipDate, DateTime _deliveryDate)
    {
        id = _id;
        customerName = _customerName;
        customerEmail = _customerEmail;
        customerAddress = _customerAddress;
        orderDate = _orderDate;
        shipDate = _shipDate;
        deliveryDate = _deliveryDate;
    }
}
