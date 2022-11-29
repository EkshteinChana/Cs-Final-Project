namespace BO;
public enum eCategory
{
    Siddur,
    Tehillim,
    Shabbat,
    Chaggim,
    Others
}

public enum eOrderStatus
{
    confirmed,
    Sent,
    provided
}


public enum eUpdateOrder
{
    /// <summary>
    /// Adding a product that does not exist in the order.
    /// </summary>
    add,
    delete,
    changeAmount
}