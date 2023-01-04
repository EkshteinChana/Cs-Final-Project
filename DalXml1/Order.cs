using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Serialization;

/// <summary>
/// This class implements the CRUD on the xml database functions for each order.
/// </summary>
internal class Order : IOrder
{
    /// <summary>
    /// A function to add a new oder to the xml database.
    /// </summary>
    public int Create(DO.Order order)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        StreamReader ordersRead = new StreamReader("..\\..\\..\\..\\xml\\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>),xRoot);
        List<DO.Order>? ordLst = new();
        ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        DO.Order tmpOrder = ordLst.Where(ord => ord.Id == order.Id).FirstOrDefault();
        if (tmpOrder.Equals(default(DO.Order))!=true)
        {
            throw new IdAlreadyExistsException();
        }
        ordLst.Add(order);
        StreamWriter orderswrite = new StreamWriter("..\\..\\..\\..\\xml\\order.xml");
        ser.Serialize(orderswrite, ordLst);
        orderswrite.Close();        
        return order.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order> Read(Func<DO.Order, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order ReadSingle(Func<DO.Order, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order t)
    {
        throw new NotImplementedException();
    }
}


///// <summary>
///// A function to add a new oder to the database.
///// </summary>
//public int Create(Order order)
//{
//    Order tmpOrder = DataSource.OrderList.Where(ord => ord.Id == order.Id).FirstOrDefault();
//    if (!tmpOrder.Equals(default(Order)))
//    {
//        throw new IdAlreadyExistsException();
//    }
//    DataSource.OrderList.Add(order);
//    return order.Id;
//}
///// <summary>
///// A function to delete an oder from the database.
///// </summary>
//public void Delete(int id)
//{
//    Order order = DataSource.OrderList.Where(order => order.Id == id).FirstOrDefault();
//    if (order.Equals(default(Order)))
//    {
//        throw new IdNotExistException("order");
//    }
//    DataSource.OrderList.Remove(order);
//}
///// <summary>
///// A function to get the information about specific oder from the database by ID.
///// </summary>
//public Order Read(int id)
//{
//    Order order = DataSource.OrderList.Where(order => order.Id == id).FirstOrDefault();
//    if (order.Equals(default(Order)))
//    {
//        throw new IdNotExistException("order");
//    }
//    return order;
//}
///// <summary> 
/////A function to get the information about all the orders in the database.
///// </summary>
//IEnumerable<Order> ICrud<Order>.Read(Func<Order, bool>? func)
//{
//    IEnumerable<Order> tmpOrderList = DataSource.OrderList;
//    return func == null ? tmpOrderList : tmpOrderList.Where(func);
//}
///// <summary> 
/////A function to get the information about all the orders in the database.
///// </summary>
//public void Update(Order order)
//{
//    Order originalOrder = DataSource.OrderList.Where(originalOrder => originalOrder.Id == order.Id).FirstOrDefault();
//    if (originalOrder.Equals(default(Order)))
//    {
//        throw new IdNotExistException("order");
//    }
//    DataSource.OrderList.Remove(originalOrder);
//    DataSource.OrderList.Add(order);
//}

//public Order ReadSingle(Func<Order, bool> func)
//{
//    Order order = DataSource.OrderList.Where(func).FirstOrDefault();
//    if (order.Equals(default(Order)))
//    {
//        throw new ObjectNotExistException("order");
//    }
//    return order;
//}