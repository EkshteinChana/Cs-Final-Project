using BlApi;
namespace BlImplementation
{
    sealed public class Bl : IBl
    {
        public IProduct Product => new BlProduct();
        public IOrder Order => new BlOrder();
        public ICart Cart => new BlCart();       
    }
}



