using DalApi;
namespace Dal;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList(){ }
    public IProduct product => new DalProduct();
    public IOrder order => new DalOrder();
    public IOrderItem orderItem => new DalOrderItem();
}

