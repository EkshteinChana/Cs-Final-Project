
namespace BlImplementation;
internal static class Config
{
    private static int s_maxCartOrderItemId = 1;
    public static int MaxCartOrderItemId { get { return s_maxCartOrderItemId++; } }
}
