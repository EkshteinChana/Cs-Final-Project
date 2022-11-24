
namespace DO;
public struct OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// //////////////////////////////////////////////
    /// </summary>
    public int Amount { get; set; }
    
    public override string ToString() => $@"
            ID: {Id}
            product ID: {ProductId}
            order ID: {OrderId}
            price*amount: {Price}
            amount: {Amount} ";
}

