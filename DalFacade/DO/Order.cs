
namespace DO;
public struct Order
{
    private readonly int id { get; }
    public string customerName { get; set; }
    public string customerEmail { get; set; }
    public string customerAddress { get; set; }
    public DateTime orderDate { get; set; }
    public DateTime deliveryDate { get; set; }
    //public Order(string _customerName, string _customerEmail, string _customerAddress)
    //{

    //}
    public override string ToString() => $@"
        order ID: {id},
        customerName: {customerName},
        customerEmail: {customerEmail},
        customerAddress: {customerAddress},
    	orderDate: {orderDate},
    	deliveryDate: {deliveryDate}
        ";

}
