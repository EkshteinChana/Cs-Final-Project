using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Order list request for manager screen
    /// </summary>
    /// <returns>An IEnumerable of OrderForList</returns>
    public IEnumerable<OrderForList> ReadOrdsManager();
    /// <summary>
    /// Order details request by orderId
    /// for manager screen and customer screen
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order ReadOrdItems(int orderId);
    /// <summary>
    /// Order Shipping Update 
    /// (for Order Management Screen of a Manager )
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order UpdateOrdShipping(int orderId);
    /// <summary>
    /// Order Delivery Update 
    /// (for Order Management Screen of a Manager )
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order UpdateOrdDelivery(int orderId);
    /// <summary>
    /// Updating Order 
    /// (for Manager Screen)
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    public Order UpdateOrd(int orderId,Order ord);
}