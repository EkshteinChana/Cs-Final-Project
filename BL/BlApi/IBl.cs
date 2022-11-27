
namespace BlApi;
/// <summary>
/// This interface calls the interfaces of all logical layer entities.
/// </summary>
public interface IBl
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public ICart Cart { get; }
}
