using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal;

sealed internal class DalXml : IDal
    //sealed public class DalXml : IDal
{

    private static Lazy<IDal> instance = new Lazy<IDal>(() => new DalXml());
    public static IDal Instance { get => instance.Value; }
    private DalXml()
    {
        lock (instance)
        {
            if (instance == null)
            {
                instance = new Lazy<IDal>();
            }
        }
    }
    public IProduct product { get; } = new Dal.Product();
    public IOrder order { get; } = new Dal.Order();
    public IOrderItem orderItem { get; } = new Dal.OrderItem();
}



