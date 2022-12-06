using DO;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        OrderItem ReadOrderItem(int pId, int oId);
        IEnumerable<OrderItem> ReadOrderItemByOrderId(int oId);
    }
}
