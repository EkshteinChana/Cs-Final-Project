using DO;

namespace Dal;

public static class DalOrder
{
    public static int CreateOrder(Order order)
    {      
        DataSource.OrderArr.Add(order);
        return order.Id;
    }

    public static Order ReadOrder(int id)
    {
        Order order = DataSource.OrderArr.Where(order => order.Id == id).FirstOrDefault();
        if (order.Equals(default(Order)))
        {
            throw new Exception("No order exists with this ID");
        }
        return order;
    }

    public static Order[] ReadOrder()
    {
        Order[] tmpOrderArr = new Order[DataSource.OrderArr.Count];
        DataSource.OrderArr.CopyTo(tmpOrderArr);
        return tmpOrderArr;
    }

    public static void UpdateOrder(Order order)
    {
        Order originalOrder = DataSource.OrderArr.Where(originalOrder => originalOrder.Id == order.Id).FirstOrDefault();
        if (originalOrder.Equals(default(Order)))
        {
            throw new Exception("No order exists with this ID");
        }
        DataSource.OrderArr.Remove(originalOrder);
        DataSource.OrderArr.Add(order);
    }

    public static void DeleteOrder(int id)
    {
        Order order = DataSource.OrderArr.Where(order => order.Id == id).FirstOrDefault();
        if (order.Equals(default(Order)))
        {
            throw new Exception("No order exists with this ID");
        }
        DataSource.OrderArr.Remove(order);
    }

}

