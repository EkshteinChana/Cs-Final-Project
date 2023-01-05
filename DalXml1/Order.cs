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
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order> ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        DO.Order tmpOrder = ordLst.Where(ord => ord.Id == order.Id).FirstOrDefault();
        if (tmpOrder.Equals(default(DO.Order)) != true)
        {
            throw new IdAlreadyExistsException();
        }
        ordLst.Add(order);
        StreamWriter orderswrite = new StreamWriter("..\\..\\..\\..\\xml\\order.xml");
        ser.Serialize(orderswrite, ordLst);
        orderswrite.Close();
        return order.Id;
    }

    /// <summary>
    /// A function to delete an oder from the xml database.
    /// </summary>
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        StreamReader ordersRead = new StreamReader("..\\..\\..\\..\\xml\\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        DO.Order tmpOrder = ordLst.Where(ord => ord.Id == id).FirstOrDefault();
        if (tmpOrder.Equals(default(DO.Order)))
        {
            throw new IdNotExistException("order");
        }
        ordLst?.Remove(tmpOrder);
        StreamWriter orderswrite = new StreamWriter("..\\..\\..\\..\\xml\\order.xml");
        ser.Serialize(orderswrite, ordLst);
        orderswrite.Close();
    }
    /// <summary>
    /// A function to get the information about specific oder from the xml database by ID.
    /// </summary>
    public DO.Order Read(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        StreamReader ordersRead = new StreamReader("..\\..\\..\\..\\xml\\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        DO.Order tmpOrder = ordLst.Where(ord => ord.Id == id).FirstOrDefault();
        if (tmpOrder.Equals(default(DO.Order)))
        {
            throw new IdNotExistException("order");
        }
        return tmpOrder;
    }

    /// <summary> 
    ///A function to get the information about all the orders in the database.
    /// </summary>
    public IEnumerable<DO.Order> Read(Func<DO.Order, bool>? func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        StreamReader ordersRead = new StreamReader("..\\..\\..\\..\\xml\\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        return func == null ? ordLst : ordLst?.Where(func);
    }

    /// <summary>
    /// A function to get a specific order from the xml database by a function.
    /// </summary>
    public DO.Order ReadSingle(Func<DO.Order, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        StreamReader ordersRead = new StreamReader("..\\..\\..\\..\\xml\\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        DO.Order tmpOrder = ordLst.Where(func).FirstOrDefault();
        if (tmpOrder.Equals(default(DO.Order)))
        {
            throw new ObjectNotExistException("order");
        }
        return tmpOrder;
    }

    /// <summary> 
    ///A function to get the information about all the orders in the xml database.
    /// </summary>
    public void Update(DO.Order order)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        StreamReader ordersRead = new StreamReader("..\\..\\..\\..\\xml\\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? ordLst = (List<DO.Order>?)ser.Deserialize(ordersRead);
        ordersRead.Close();
        DO.Order originalOrder = ordLst.Where(ord => ord.Id == order.Id).FirstOrDefault();
        if (originalOrder.Equals(default(DO.Order)))
        {
            throw new IdNotExistException("order");
        }
        ordLst.Remove(originalOrder);
        ordLst.Add(order);
        StreamWriter orderswrite = new StreamWriter("..\\..\\..\\..\\xml\\order.xml");
        ser.Serialize(orderswrite, ordLst);
        orderswrite.Close();
    }
}



