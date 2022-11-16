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
        //for (int i = 0; i < DataSource.Config.orderArrIdx; i++)
        //{
        //    if (DataSource.orderArr[i].Id == id)
        //        return DataSource.orderArr[i];
        //}
        Order order = DataSource.OrderArr.Where(order => order.Id == id).First();
        if (order.Equals(null))
        {
            throw new Exception("No order exists with this ID");
        }
        return order;
    }

    public static Order[] ReadOrder()
    {
        Order[] tmpOrderArr = new Order[DataSource.OrderArr.Count];
        DataSource.OrderArr.CopyTo(tmpOrderArr);
        //for (int i = 0; i < DataSource.Config.orderArrIdx; i++)
        //{
        //    tmpOrderArr[i] = DataSource.orderArr[i];
        //}
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


        //int i;
        //for (i = 0; i < DataSource.Config.orderArrIdx; i++)
        //{
        //    if (DataSource.orderArr[i].Id == order.Id)
        //    {
        //        DataSource.orderArr[i] = order;
        //        return;
        //    }
        //}
        
    }

    public static void DeleteOrder(int id)
    {
        Order order = DataSource.OrderArr.Where(order => order.Id == id).First();
        if (order.Equals(null))
        {
            throw new Exception("No order exists with this ID");
        }
        DataSource.OrderArr.Remove(order);
        //for (int i = 0; i < DataSource.Config.orderArrIdx; i++)
        //{
        //    if (DataSource.orderArr[i].Id == id)
        //    {
        //        DataSource.orderArr[i] = DataSource.orderArr[DataSource.Config.orderArrIdx-1];
        //        DataSource.Config.orderArrIdx -= 1;
        //        return;
        //    }
        //}
        //throw new Exception("No order exists with this ID ");
    }

}

