using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

/// <summary>
/// This class implements the CRUD on the xml database functions for each item in order.
/// </summary>
internal class OrderItem : IOrderItem
{
    /// <summary>
    /// A function to add to the xml database a new item to an existing order.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(DO.OrderItem orderItem)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader orderItemsRead = new StreamReader(@"..\xml\orderItem.xml");
        XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem>? ordItmLst = (List<DO.OrderItem>?)ser.Deserialize(orderItemsRead);
        orderItemsRead.Close();
        DO.OrderItem tmpOrderItem = ordItmLst.Where(ordItem => ordItem.Id == orderItem.Id).FirstOrDefault();
        if (!tmpOrderItem.Equals(default(DO.OrderItem)))
        {
            throw new IdAlreadyExistsException();
        }
        ordItmLst.Add(orderItem);
        StreamWriter orderItemsWrite = new StreamWriter(@"..\xml\orderItem.xml");
        ser.Serialize(orderItemsWrite, ordItmLst);
        orderItemsWrite.Close();
        return orderItem.Id;
    }

    /// <summary>
    /// A function to delete from the xml database an item from an existing order.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader orderItemsRead = new StreamReader(@"..\xml\orderItem.xml");
        XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem>? ordItmLst = (List<DO.OrderItem>?)ser.Deserialize(orderItemsRead);
        orderItemsRead.Close();
        DO.OrderItem tmpOrderItem = ordItmLst.Where(ordItem => ordItem.Id == id).FirstOrDefault();
        if (tmpOrderItem.Equals(default(DO.OrderItem)))
        {
            throw new IdNotExistException("order item");
        }
        ordItmLst.Remove(tmpOrderItem);
        StreamWriter orderItemsWrite = new StreamWriter(@"..\xml\orderItem.xml");
        ser.Serialize(orderItemsWrite, ordItmLst);
        orderItemsWrite.Close();
    }

    /// <summary>
    /// A function to get from the xml database an item from an existing order by the item ID.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Read(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader orderItemsRead = new StreamReader(@"..\xml\orderItem.xml");
        XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem>? ordItmLst = (List<DO.OrderItem>?)ser.Deserialize(orderItemsRead);
        orderItemsRead.Close();
        DO.OrderItem tmpOrderItem = ordItmLst.Where(ordItem => ordItem.Id == id).FirstOrDefault();
        if (tmpOrderItem.Equals(default(DO.OrderItem)))
        {
            throw new IdNotExistException("order item");
        }
        return tmpOrderItem;
    }

    /// <summary>
    /// A function to get from the xml database all the items that ordered,and from all the orders.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem> Read(Func<DO.OrderItem, bool>? func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader orderItemsRead = new StreamReader(@"..\xml\orderItem.xml");
        XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem> tmpOrderItemList = (List<DO.OrderItem>)ser.Deserialize(orderItemsRead);
        orderItemsRead.Close();
        if (func != null)
        {
            tmpOrderItemList = tmpOrderItemList.Where(func).ToList();
            if (tmpOrderItemList.Equals(null) || tmpOrderItemList.Count() == 0)
            {
                throw new ObjectNotExistException("order or items in this order");
            }
        }
        return tmpOrderItemList;
    }

    /// <summary>
    /// A function to get a specific orderItem from the xml database by a function.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem ReadSingle(Func<DO.OrderItem, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader orderItemsRead = new StreamReader(@"..\xml\orderItem.xml");
        XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem>? ordItmLst = (List<DO.OrderItem>?)ser.Deserialize(orderItemsRead);
        orderItemsRead.Close();
        DO.OrderItem tmpOrderItem = ordItmLst.Where(func).FirstOrDefault();
        if (tmpOrderItem.Equals(default(DO.OrderItem)))
        {
            throw new ObjectNotExistException("order item");
        }
        return tmpOrderItem;
    }

    /// <summary>
    /// A function to update a specific item in an specific order.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem orderItem)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader orderItemsRead = new StreamReader(@"..\xml\orderItem.xml");
        XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem>? ordItmLst = (List<DO.OrderItem>?)ser.Deserialize(orderItemsRead);
        orderItemsRead.Close();
        DO.OrderItem originalOrderItem = ordItmLst.Where(ordItem => ordItem.Id == orderItem.Id).FirstOrDefault();
        if (originalOrderItem.Equals(default(DO.OrderItem)))
        {
            throw new IdNotExistException("order item");
        }
        ordItmLst.Remove(originalOrderItem);
        ordItmLst.Add(orderItem);
        StreamWriter orderItemsWrite = new StreamWriter(@"..\xml\orderItem.xml");
        ser.Serialize(orderItemsWrite, ordItmLst);
        orderItemsWrite.Close();
    }
}






