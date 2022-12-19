using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = DalApi.Factory.Get();
    private BO.eOrderStatus checkStatus(DO.Order dO)
    {
        if (dO.DeliveryDate != null)
        {
            return BO.eOrderStatus.provided;
        }
        else if (dO.ShipDate != null)
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
        foreach (var prop in dO.GetType().GetProperties())
        {
            bO.GetType().GetProperty(prop.Name).SetValue(bO, prop.GetValue(dO));
        }
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
            foreach (var prop in dItm.GetType().GetProperties())
            {
                if (prop.Name != "OrderId")
                    bItm.GetType().GetProperty(prop.Name).SetValue(bItm, prop.GetValue(dItm));
            }
            //bItm.Id = dItm.Id;
            //bItm.ProductId = dItm.ProductId;
            bItm.Name = (Dal.product.Read(dItm.ProductId)).Name;
            //bItm.Price = dItm.Price;
           // bItm.Amount = dItm.Amount;
            bItm.TotalPrice = bItm.Price * bItm.Amount;
            bO.TotalPrice += bItm.TotalPrice;
            bItemsList.Add(bItm);
        }
        bO.Items = bItemsList;
        return bO;
    }

    BO.Order BlApi.IOrder.ReadOrd(int orderId)
    {
        if (orderId < 0)
        {
            throw new InvalidValue("ID");
        }
        try
        {
            DO.Order dO = Dal.order.Read(orderId);
            return convertDToB(dO);
        }
        catch (IdNotExistException err)
        {
            throw new DataError(err, "Data Error: ");
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

            foreach (var prop in ordForList.GetType().GetProperties())
            {
                if (prop.Name != "AmountOfItems")
                    ordForList.GetType().GetProperty(prop.Name).SetValue(ordForList, bO.GetType().GetProperty(prop.Name).GetValue(bO));
            }

            //ordForList.Id = bO.Id;
            //ordForList.CustomerName = bO.CustomerName;
            //ordForList.status = bO.status;
            ordForList.AmountOfItems = bO.Items.Count();
            //ordForList.TotalPrice = bO.TotalPrice;
            orderList.Add(ordForList);
        }
        return orderList;
    }


    BO.Order BlApi.IOrder.UpdateOrd(int oId, int pId, int amount, BO.eUpdateOrder action) // (Bonus)
    {
        try
        {
            DO.Order dOrd = Dal.order.Read(oId);
            if (dOrd.OrderDate == null)
            {
                throw new IllegalAction("The customer has not completed the order.");
            }
            if (dOrd.ShipDate != null)
            {
                throw new IllegalAction("The order has already been sent.");
            }

            IEnumerable<DO.OrderItem> orderItems = Dal.orderItem.Read();

            DO.OrderItem? itmInOrd = orderItems.Where(oItm => (oItm.ProductId == pId && oItm.OrderId == oId)).FirstOrDefault();

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
                    if (itmInOrd.Equals(default(DO.OrderItem)) || itmInOrd == null)
                    {
                        throw new IllegalAction("Deletion of a product that does not exist in the order.");
                    }
                    DO.OrderItem tmpItmInOrd = (DO.OrderItem)itmInOrd;
                    Dal.orderItem.Delete(tmpItmInOrd.Id);

                    break;
                case BO.eUpdateOrder.changeAmount:
                    if (itmInOrd.Equals(default(DO.OrderItem)) || itmInOrd == null)
                    {
                        throw new IllegalAction("Updating the quantity of an item that does not exist in the order");
                    }
                    DO.OrderItem tmpItmInOrd1 = (DO.OrderItem)itmInOrd;
                    tmpItmInOrd1.Amount = amount;
                    Dal.orderItem.Update(tmpItmInOrd1);
                    break;
            }
            return convertDToB(dOrd);
        }
        catch (IdNotExistException err)
        {
            throw new DataError(err, "Data Error: ");
        }
        catch (IdAlreadyExistsException err)
        {
            throw new DataError(err, "Data Error: ");
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
            if (dOrder.DeliveryDate != null)
            {
                throw new IllegalAction("The order has already been delivered.");
            }
            if (dOrder.ShipDate == null)
            {
                throw new IllegalAction("The order has not been sent yet.");
            }
            dOrder.DeliveryDate = DateTime.Now;
            Dal.order.Update(dOrder);
            return convertDToB(dOrder);
        }
        catch (IdNotExistException err)
        {
            throw new DataError(err, "Data Error: ");
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
            if (dOrder.ShipDate != null)
            {
                throw new IllegalAction("The order has already been sent.");
            }
            dOrder.ShipDate = DateTime.Now;
            Dal.order.Update(dOrder);
            return convertDToB(dOrder);
        }
        catch (IdNotExistException err)
        {
            throw new DataError(err, "Data Error: ");
        }
    }

}
