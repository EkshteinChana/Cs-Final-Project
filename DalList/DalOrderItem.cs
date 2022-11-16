using DO;

namespace Dal;

public static class DalOrderItem
{
    public static int CreateOrderItem(OrderItem orderItem)
    {
        DataSource.OrderItemArr.Add(orderItem);
        return orderItem.Id;
    }

    public static OrderItem ReadOrderItem(int id)
    {
        OrderItem orderItem = DataSource.OrderItemArr.Where(orderItem => orderItem.Id == id).FirstOrDefault();
        if (orderItem.Equals(default(OrderItem)))
        {
            throw new Exception("No orderItem exists with this ID ");
        }
        return orderItem;
    }

    public static OrderItem ReadOrderItem(int pId, int oId)
    {
        OrderItem orderItem = DataSource.OrderItemArr.Where(orderItem => orderItem.ProductId == pId && orderItem.OrderId == oId).FirstOrDefault();
        if (orderItem.Equals(default(OrderItem)))
        {
            throw new Exception("No orderItem exists with this product ID and order ID");
        }
        return orderItem;
    }


    public static OrderItem[] ReadOrderItem()
    {
        OrderItem[] tmpOrderItemArr = new OrderItem[DataSource.OrderItemArr.Count];
        DataSource.OrderItemArr.CopyTo(tmpOrderItemArr);
        return tmpOrderItemArr;
    }

    public static OrderItem[] ReadOrderItemByOrderId(int oId)
    {
        List<OrderItem> orderItems = DataSource.OrderItemArr.Where(orderItem => orderItem.OrderId == oId).ToList() ;
        if(orderItems.Equals(null) || orderItems.Count == 0)
        {
            throw new Exception("No orderItems exist with this order ID");
        }
        OrderItem[] tmpOrderItemArr = new OrderItem[orderItems.Count];
        orderItems.CopyTo(tmpOrderItemArr);
        return tmpOrderItemArr;
    }

    public static void UpdateOrderItem(OrderItem orderItem)
    {
        OrderItem originalOrderItem = DataSource.OrderItemArr.Where(originalOrderItem => originalOrderItem.Id == orderItem.Id).FirstOrDefault();
        if (originalOrderItem.Equals(default(OrderItem)))
        {
            throw new Exception("No orderItem exists with this ID");
        }
        DataSource.OrderItemArr.Remove(originalOrderItem);
        DataSource.OrderItemArr.Add(orderItem);
    }

    public static void DeleteOrderItem(int id)
    {
        OrderItem orderItem = DataSource.OrderItemArr.Where(orderItem => orderItem.Id == id).FirstOrDefault();
        if (orderItem.Equals(default(OrderItem)))
        {
            throw new Exception("No orderItem exists with this ID");
        }
        DataSource.OrderItemArr.Remove(orderItem);
    }
}


