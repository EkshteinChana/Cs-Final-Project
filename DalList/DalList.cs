using DalApi;
namespace Dal;
//public IProduct product { get; }
//public IOrder order { get; }
//public IOrderItem orderItem { get; }
sealed public class DalList : IDal
{
    public IProduct product => new DalProduct();
    public IOrder order => new DalOrder();
    public IOrderItem orderItem => new DalOrderItem();
}
