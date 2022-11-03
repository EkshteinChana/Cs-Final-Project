
namespace DO;
public struct OrderItem
{
    public readonly int id { get; }
    public int productId { get; }
    public int orderId { get; }
    public double price { get; set; }
    public int amount { get; set; }
    
    public override string ToString() => $@"
            ID: {id}
            product ID: {productId}
            order ID: {orderId}
            price: {price}
            amount: {amount} ";

    public OrderItem(int _id, int _productId, int _orderId, double _price, int _amount)
    {
        id = _id;
        productId = _productId;
        orderId = _orderId;
        price = _price;
        amount = _amount;
    }
}
