using DO;
using DalApi;

namespace Dal;
/// <summary>
/// This class implements the CRUD on the database functions for each item in order.
/// </summary>
public class DalOrderItem : IOrderItem
{
    /// <summary>
    /// A function to add to the database a new item to an existing order.
    /// </summary>
    public int Create(OrderItem orderItem)
    {
        OrderItem tmpOrderItem = DataSource.OrderItemList.Where(ordItem => ordItem.Id == orderItem.Id).FirstOrDefault();

        if (!tmpOrderItem.Equals(default(OrderItem)))
        {
            throw new IdAlreadyExistsException();
        }
        DataSource.OrderItemList.Add(orderItem);
        return orderItem.Id;
    }
    /// <summary>
    /// A function to delete from the database an item from an existing order.
    /// </summary>
    public void Delete(int id)
    {
        OrderItem orderItem = DataSource.OrderItemList.Where(orderItem => orderItem.Id == id).FirstOrDefault();
        if (orderItem.Equals(default(OrderItem)))
        {
            throw new IdNotExistException("order item");
        }
        DataSource.OrderItemList.Remove(orderItem);
    }
    /// <summary>
    /// A function to get from the database an item from an existing order by the item ID.
    /// </summary>
    public OrderItem Read(int id)
    {
        OrderItem orderItem = DataSource.OrderItemList.Where(orderItem => orderItem.Id == id).FirstOrDefault();
        if (orderItem.Equals(default(OrderItem)))
        {
            throw new IdNotExistException("order item");
        }
        return orderItem;
    }

    /// <summary>
    /// A function to get from the database all the items that ordered,and from all the orders.
    /// </summary>
    IEnumerable<OrderItem> ICrud<OrderItem>.Read(Func<OrderItem, bool> func)
    {
        IEnumerable<OrderItem> tmpOrderItemList = DataSource.OrderItemList;
        if(func != null)
        {
            tmpOrderItemList= tmpOrderItemList.Where(func);
            if (tmpOrderItemList.Equals(null) || tmpOrderItemList.Count() == 0)
            {
                throw new ObjectNotExistException("order or items in this order");
            }
        }
        return tmpOrderItemList;
    }

    /// <summary>
    /// A function to update a specific item in an specific order.
    /// </summary>
    public void Update(OrderItem orderItem)
    {
        OrderItem originalOrderItem = DataSource.OrderItemList.Where(originalOrderItem => originalOrderItem.Id == orderItem.Id).FirstOrDefault();
        if (originalOrderItem.Equals(default(OrderItem)))
        {
            throw new IdNotExistException("order item");
        }
        DataSource.OrderItemList.Remove(originalOrderItem);
        DataSource.OrderItemList.Add(orderItem);
    }

    /// <summary>
    /// A function to get a specific orderItem from the xml database by a function.
    /// </summary>
    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        OrderItem orderItm = DataSource.OrderItemList.Where(func).FirstOrDefault();
        if (orderItm.Equals(default(OrderItem)))
        {
            throw new ObjectNotExistException("order item");
        }
        return orderItm;
    }
}


