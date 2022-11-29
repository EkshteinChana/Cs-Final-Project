using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = new DalList();
    private BO.eOrderStatus checkStatus(DO.Order dO)
    {
        if (dO.DeliveryDate != DateTime.MinValue)
        {
            return BO.eOrderStatus.provided;
        }
        else if (dO.ShipDate != DateTime.MinValue)
        {
            return BO.eOrderStatus.Sent;
        }
        else
        {
            return BO.eOrderStatus.confirmed;
        }
    }

    private BO.Order convertDToB(DO.Order dO)
    {
        BO.Order bO = new();
        bO.Id = dO.Id;
        bO.CustomerName = dO.CustomerName;
        bO.CustomerEmail = dO.CustomerEmail;
        bO.DeliveryDate = dO.DeliveryDate;
        bO.status = checkStatus(dO);
        //items and totalPrice:
        IEnumerable<DO.OrderItem> orderItems = Dal.orderItem.Read();
        IEnumerable<DO.OrderItem> items = new List<DO.OrderItem>(orderItems.Count());
        items = orderItems.Where(ordItm => ordItm.OrderId == bO.Id);
        bO.Items = new List<BO.OrderItem>(items.Count());
        List<BO.OrderItem> bItemsList = bO.Items.ToList();
        bO.TotalPrice = 0;
        foreach (DO.OrderItem dItm in items)
        {
            BO.OrderItem bItm = new();
            bItm.Id = dItm.Id;
            bItm.ProductId = dItm.ProductId;
            bItm.Name = (Dal.product.Read(dItm.ProductId)).Name;
            bItm.Price = dItm.Price;
            bItm.Amount = dItm.Amount;
            bItm.TotalPrice = bItm.Price * bItm.Amount;
            bO.TotalPrice += bItm.TotalPrice;
            bItemsList.Add(bItm);
        }
        bO.Items = bItemsList;
        return bO;
    }

    BO.Order BlApi.IOrder.ReadOrd(int orderId)
    {
        if(orderId < 0)
        {
            throw new InvalidValue("ID");
        }
        try
        {
            DO.Order dO =  Dal.order.Read(orderId);
            return convertDToB(dO);
        }
        catch (IdNotExist err)
        {
            throw new DataError(err);
        }
    }

    IEnumerable<BO.OrderForList> BlApi.IOrder.ReadOrdsManager()
    {
        IEnumerable<DO.Order> dOrders = Dal.order.Read();
        List<BO.OrderForList> orderList = new List<BO.OrderForList>(dOrders.Count());
        foreach (DO.Order dO in dOrders)
        {
            BO.Order bO = convertDToB(dO);
            BO.OrderForList ordForList = new();
            ordForList.Id = bO.Id;
            ordForList.CustomerName = bO.CustomerName;
            ordForList.status = bO.status;
            ordForList.AmountOfItems = bO.Items.Count();
            ordForList.TotalPrice = bO.TotalPrice;
            orderList.Add(ordForList);
        }
        return orderList;
    }

 
    BO.Order BlApi.IOrder.UpdateOrd(int oId , int pId , int amount , BO.eUpdateOrder action) // (Bonus)
    {
        try
        {
            DO.Order dOrd = Dal.order.Read(oId);
            if (dOrd.OrderDate == DateTime.MinValue)
            {
                throw new IllegalAction("The customer has not completed the order.");
            }
            if(dOrd.ShipDate != DateTime.MinValue)
            {
                throw new IllegalAction("The order has already been sent.");
            }

            IEnumerable<DO.OrderItem> orderItems =  Dal.orderItem.Read();
            DO.OrderItem itmInOrd = orderItems.Where(oItm => oItm.ProductId == pId && oItm.OrderId == oId).FirstOrDefault();

            switch (action)
            {
                case BO.eUpdateOrder.add:
                    if (!itmInOrd.Equals(default(DO.OrderItem)))
                    {
                        throw new IllegalAction("Adding a product that already exists in the order.");
                    }
                    DO.OrderItem newItm = new();
                    newItm.Id = DataSource.Config.MaxOrderItemId;
                    newItm.OrderId = oId;
                    newItm.ProductId = pId;
                    newItm.Price = Dal.product.Read(pId).Price;
                    newItm.Amount = amount;
                    
                    Dal.orderItem.Create(newItm);
                    break;
                case BO.eUpdateOrder.delete:
                    if (itmInOrd.Equals(default(DO.OrderItem)))
                    {
                        throw new IllegalAction("Deletion of a product that does not exist in the order.");
                    }
                    
                    Dal.orderItem.Delete(itmInOrd.Id);
                    break;
                case BO.eUpdateOrder.changeAmount:
                    if (itmInOrd.Equals(default(DO.OrderItem)))
                    {
                        throw new IllegalAction("Updating the quantity of an item that does not exist in the order");
                    }
                    itmInOrd.Amount = amount;
                    
                    Dal.orderItem.Update(itmInOrd);
                    break;
            }         
            return convertDToB(dOrd);
        }
        catch (IdNotExist err)
        {
            throw new DataError(err);
        }
        catch (IdAlreadyExists err)
        {
            throw new DataError(err);
        }
    }

    BO.Order BlApi.IOrder.UpdateOrdDelivery(int oId)
    {
        if (oId < 0)
        {
            throw new InvalidValue("ID");
        }
        try
        {
            DO.Order dOrder = Dal.order.Read(oId);
            if(dOrder.DeliveryDate !=  DateTime.MinValue)
            {
                throw new IllegalAction("The order has already been delivered.");
            }
            return convertDToB(dOrder);
        }
        catch (IdNotExist err)
        {
            throw new DataError(err);
        } 
    }

    BO.Order BlApi.IOrder.UpdateOrdShipping(int oId)
    {
        if (oId < 0)
        {
            throw new InvalidValue("ID");
        }
        try
        {
            DO.Order dOrder = Dal.order.Read(oId);
            if (dOrder.ShipDate != DateTime.MinValue)
            {
                throw new IllegalAction("The order has already been sent.");
            }
            return convertDToB(dOrder);
        }
        catch (IdNotExist err)
        {
            throw new DataError(err);
        }
    }

}
