using DalApi;
namespace Dal;
sealed internal class DalList : IDal
{
    private static Lazy<IDal> instance = new Lazy<IDal>();
    public static IDal Instance { get => instance.Value; }
    private DalList(){
        lock (instance)
        {
            if(instance == null)
            {
                instance = new Lazy<IDal>();
            }
        }
    }
    public IProduct product => new DalProduct();
    public IOrder order => new DalOrder();
    public IOrderItem orderItem => new DalOrderItem();
}

