using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlOrder : IOrder
{
    private IDal Dal = new DalList();
    public BO.Order ReadOrd(int orderId)
    {
        
    }

    public IEnumerable<BO.OrderForList> ReadOrdsManager()
    {
        
    }

    public BO.Order UpdateOrd(int orderId, BO.Order ord)
    {
        
    }

    public BO.Order UpdateOrdDelivery(int orderId)
    {
        
    }

    public BO.Order UpdateOrdShipping(int orderId)
    {
        
    }



    ///////////////////////////////////////////
    //BO.Order order = new BO.Order();
    //order.Id = DataSource.Config.MaxOrderId;
    //order.CustomerName = customerName;
    //order.CustomerEmail = customerEmail;
    //order.CustomerAddress = customerAddress;
    //order.OrderDate = DateTime.Now;
    //order.ShipDate = DateTime.MinValue;
    //order.PaymentDate = DateTime.MinValue;
    //order.DeliveryDate = DateTime.MinValue;
    //order.status = 0;
    //order.Items = cart.Items; 
    //order.TotalPrice = cart.TotalPrice;
    //////////////////////////////////////////
}
