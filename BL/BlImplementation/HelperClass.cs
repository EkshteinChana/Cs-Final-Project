
namespace BlImplementation;
public static class Config
{
    private static int s_maxOrderItemId = 1;
    public static int MaxOrderItemId { get { return s_maxOrderItemId++; } }

    private static int s_maxOrderId = 1;
    public static int MaxOrderId { get { return s_maxOrderId++; } }
}
