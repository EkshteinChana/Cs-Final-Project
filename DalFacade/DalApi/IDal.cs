namespace DalApi;

/// <summary>
/// This interface calls the interfaces of all data layer entities.
/// </summary>
public interface IDal
{
    public IProduct product { get; }
    public IOrder order { get; }
    public IOrderItem orderItem { get; }
}

