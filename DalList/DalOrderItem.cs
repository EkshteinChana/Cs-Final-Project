using DO;

namespace Dal;

 public static class DalOrderItem
{

    public static int CreateOrderItem(OrderItem orderItem)
    {
        if (DataSource.Config.orderItemArrIdx == DataSource.MaxOrderItem)
        {
            throw new Exception("The orderItem set is full, it is not possible to add an orderItem");
        }
        DataSource.orderItemArr[DataSource.Config.orderItemArrIdx++] = orderItem;
        return orderItem.id;
    }

    public static OrderItem ReadOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            if (DataSource.orderItemArr[i].id == id)
                return DataSource.orderItemArr[i];
        }
        throw new Exception("No orderItem exists with this ID ");
    }

    public static OrderItem ReadOrderItem(int pId,int oId)
    {
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            if ((DataSource.orderItemArr[i].productId == pId) &&(DataSource.orderItemArr[i].orderId == oId))
                return DataSource.orderItemArr[i];
        }
        throw new Exception("No orderItem exists with this product ID and order ID");
    }

    public static OrderItem[] ReadOrderItem()
    {
        OrderItem[] tmpOrderItemArr = new OrderItem[DataSource.Config.orderItemArrIdx];
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            tmpOrderItemArr[i] = DataSource.orderItemArr[i];
        }
        return tmpOrderItemArr;
    }

    public static OrderItem[] ReadOrderItemByOrderId(int oId)
    {       
        int j = 0;
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            if (DataSource.orderItemArr[i].orderId == oId)
                j++;
        }    
        if(j==0)
        {
            throw new Exception("No orderItems exist with this order ID");
        }
        OrderItem[] tmpOrderItemArr = new OrderItem[j];
        j = 0;
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            if (DataSource.orderItemArr[i].orderId == oId)
                tmpOrderItemArr[j++] = DataSource.orderItemArr[i];
        }
        return tmpOrderItemArr;
    }

    public static OrderItem UpdateOrderItem(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            if (DataSource.orderItemArr[i].id == orderItem.id)
            {
                DataSource.orderItemArr[i] = orderItem;
            }
        }
        throw new Exception("No orderItem exists with this ID ");
    }

    public static void DeleteOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.orderItemArrIdx; i++)
        {
            if (DataSource.orderItemArr[i].id == id)
            {
                DataSource.orderItemArr[i] = DataSource.orderItemArr[DataSource.Config.orderItemArrIdx];
                DataSource.Config.orderItemArrIdx -= 1;
                break;
            }
        }
        throw new Exception("No orderItem exists with this ID ");
    }

}


