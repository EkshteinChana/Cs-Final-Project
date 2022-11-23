using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlOrder : IOrder
{
    private IDal Dal = new DalList();
    public Order ReadOrd(int orderId)
    {
        
    }

    public IEnumerable<OrderForList> ReadOrdsManager()
    {
        
    }

    public Order UpdateOrd(int orderId, Order ord)
    {
        
    }

    public Order UpdateOrdDelivery(int orderId)
    {
        
    }

    public Order UpdateOrdShipping(int orderId)
    {
        
    }
}
