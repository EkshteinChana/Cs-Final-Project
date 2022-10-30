
using DO;

namespace Dal;
internal static class DataSource
{
    internal const int MaxOrder = 100;
    internal const int MaxProduct = 50;
    internal const int MaxOrderItem = 200;
    internal static Order[] orderArr = new Order[MaxOrder];
    internal static Product[] productArr = new Product[MaxProduct];
    internal static OrderItem[] orderItemArr = new OrderItem[MaxOrderItem];


    //private void CreateProductArr()
    //{
        
    //}
    internal static class Config
    {

    }

}

