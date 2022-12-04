
using BO;
namespace BlApi;
/// <summary>
/// Interfaces for actions regarding to main logical entity-Order
/// </summary>
public interface IOrder
{
    /// <summary>
    /// A function to read the list of orders for manager screen
    /// </summary>
    /// <returns>An IEnumerable of OrderForList</returns>
    public IEnumerable<OrderForList?> ReadOrdsManager();
    /// <summary>
    /// A function to read the details of an order
    /// for manager screen and customer screen
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order ReadOrd(int orderId);
    /// <summary>
    /// A function to update the shipping date
    /// (for Order Management Screen of a Manager )
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order UpdateOrdShipping(int orderId);
    /// <summary>
    /// A function to update the delivery date 
    /// (for Order Management Screen of a Manager )
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order UpdateOrdDelivery(int orderId);
    /// <summary>
    /// A function to update an order 
    /// (for Manager Screen)
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order UpdateOrd(int orderId, int pId, int amount, BO.eUpdateOrder action);
}