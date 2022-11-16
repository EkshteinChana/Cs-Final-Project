
namespace DalApi
{
    internal interface IDal
    {
        public IProduct product { get; }
        public IOrder order { get; }
        public IOrderItem orderItem { get; }
    }
}

