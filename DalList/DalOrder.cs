using DO;
using DalApi;

namespace Dal;
/// <summary>
/// This class implements the CRUD on the database functions for each order.
/// </summary>
public class DalOrder : IOrder
{
    /// <summary>
    /// A function to add a new oder to the database.
    /// </summary>
    public int Create(Order order)
    {
        Order tmpOrder = DataSource.OrderList.Where(ord => ord.Id == order.Id).FirstOrDefault();
        if (!tmpOrder.Equals(default(Order)))
        {
            throw new IdAlreadyExistsException();
        }
        DataSource.OrderList.Add(order);
        return order.Id;
    }
    /// <summary>
    /// A function to delete an oder from the database.
    /// </summary>
    public void Delete(int id)
    {
        Order order = DataSource.OrderList.Where(order => order.Id == id).FirstOrDefault();
        if (order.Equals(default(Order)))
        {
            throw new IdNotExistException("order");
        }
        DataSource.OrderList.Remove(order);
    }
    /// <summary>
    /// A function to get the information about specific oder from the database by ID.
    /// </summary>
    public Order Read(int id)
    {
        Order order = DataSource.OrderList.Where(order => order.Id == id).FirstOrDefault();
        if (order.Equals(default(Order)))
        {
            throw new IdNotExistException("order");
        }
        return order;
    }
    /// <summary> 
    ///A function to get the information about all the orders in the database.
    /// </summary>
    IEnumerable<Order> ICrud<Order>.Read(Func<Order, bool>? func) 
    {
        IEnumerable<Order> tmpOrderList = DataSource.OrderList;
        return func==null ? tmpOrderList : tmpOrderList.Where(func);
    }
    /// <summary> 
    ///A function to get the information about all the orders in the database.
    /// </summary>
    public void Update(Order order)
    {
        Order originalOrder = DataSource.OrderList.Where(originalOrder => originalOrder.Id == order.Id).FirstOrDefault();
        if (originalOrder.Equals(default(Order)))
        {
            throw new IdNotExistException("order");
        }
        DataSource.OrderList.Remove(originalOrder);
        DataSource.OrderList.Add(order);
    }

    public Order ReadSingle(Func<Order, bool> func)
    {
        Order order =  DataSource.OrderList.Where(func).FirstOrDefault();
        if (order.Equals(default(Order)))
        {
            throw new ObjectNotExistException("order");        
        }
        return order;
    }
}
