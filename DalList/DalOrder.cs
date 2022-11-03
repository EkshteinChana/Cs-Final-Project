using DO;

namespace Dal;

public static class DalOrder
{
    public static int CreateOrder(Order order)
    {
        if(DataSource.Config.orderArrIdx== DataSource.MaxOrder)
        {
            throw new Exception("The order set is full, it is not possible to add an order");
        }
        DataSource.orderArr[DataSource.Config.orderArrIdx++] = order;
        return order.id;
    }

    public static Order ReadOrder(int id)
    {
        for(int i=0; i < DataSource.Config.orderArrIdx; i++)
        {
            if (DataSource.orderArr[i].id == id)
                return DataSource.orderArr[i];
        }
            throw new Exception("No order exists with this ID ");    
    }

    public static Order[] ReadOrder()
    {
        Order[] tmpOrderArr = new Order[DataSource.Config.orderArrIdx];
        for (int i = 0; i < DataSource.Config.orderArrIdx; i++)
        {
            tmpOrderArr[i] = DataSource.orderArr[i];               
        }
        return tmpOrderArr;
    }

    public static Order UpdateOrder(Order order)
    {
        for (int i = 0; i < DataSource.Config.orderArrIdx; i++)
        {
            if (DataSource.orderArr[i].id == order.id)
            {
                DataSource.orderArr[i] = order;
            }               
        }
        throw new Exception("No order exists with this ID ");
    }

    public static void DeleteOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderArrIdx; i++)
        {
            if (DataSource.orderArr[i].id == id)
            {
                DataSource.orderArr[i] = DataSource.orderArr[DataSource.Config.orderArrIdx];
                DataSource.Config.orderArrIdx -= 1;
                break;
            }
        }
        throw new Exception("No order exists with this ID ");
    }

}   

