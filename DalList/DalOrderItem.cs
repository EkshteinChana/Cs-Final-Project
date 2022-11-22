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
            throw new IdNotExist();
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
            throw new IdNotExist();
        }
        return orderItem;
    }

    /// <summary>
    /// A function to get from the database all the items that ordered,and from all the orders.
    /// </summary>
    public IEnumerable<OrderItem> Read()
    {
        OrderItem[] tmpOrderItemList = new OrderItem[DataSource.OrderItemList.Count];
        DataSource.OrderItemList.CopyTo(tmpOrderItemList);
        return tmpOrderItemList;
    }
    /// <summary>
    /// A function to get an specific item from a specific all the items that ordered,and from all the orders.
    /// </summary>
    public OrderItem ReadOrderItem(int pId, int oId)
    {
        OrderItem orderItem = DataSource.OrderItemList.Where(orderItem => orderItem.ProductId == pId && orderItem.OrderId == oId).FirstOrDefault();
        if (orderItem.Equals(default(OrderItem)))
        {
            throw new IdNotExist();
        }
        return orderItem;
    }
    /// <summary>
    /// A function to get from the database all the items in an specific order.
    /// </summary>
    public IEnumerable<OrderItem> ReadOrderItemByOrderId(int oId)
    {
        List<OrderItem> orderItems = DataSource.OrderItemList.Where(orderItem => orderItem.OrderId == oId).ToList();
        if (orderItems.Equals(null) || orderItems.Count == 0)
        {
            throw new IdNotExist();
        }
        List<OrderItem> tmpOrderItemList = new List<OrderItem>(orderItems.Count);
        tmpOrderItemList = orderItems;
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
            throw new IdNotExist();
        }
        DataSource.OrderItemList.Remove(originalOrderItem);
        DataSource.OrderItemList.Add(orderItem);
    }
}


