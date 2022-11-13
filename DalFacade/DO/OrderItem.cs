
namespace DO;
public struct OrderItem
{
    public readonly int Id { get; }
    public int ProductId { get; }
    public int OrderId { get; }
    public double Price { get; set; }
    public int Amount { get; set; }
    
    public override string ToString() => $@"
            ID: {Id}
            product ID: {ProductId}
            order ID: {OrderId}
            price*amount: {Price}
            amount: {Amount} ";

    public OrderItem(int _id, int _productId, int _orderId, double _price, int _amount)
    {
        Id = _id;
        ProductId = _productId;
        OrderId = _orderId;
        Price = _price;
        Amount = _amount;
    }
}

