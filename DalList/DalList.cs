using DalApi;
namespace Dal;
//public IProduct product { get; }
//public IOrder order { get; }
//public IOrderItem orderItem { get; }
sealed public class DalList : IDal
{
    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
}
