
namespace DO;
public struct Order
{
    private readonly int Id { get; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }


}
