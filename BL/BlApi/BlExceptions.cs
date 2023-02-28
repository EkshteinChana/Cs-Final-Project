
namespace BlApi;
/// <summary>
/// Exception in Bl that catch exception from the data layer
/// </summary>
public class DataErrorException : Exception
{
    public DataErrorException(Exception inner,string m) : base(m, inner) { }
}

/// <summary>
/// Exception for invalid values
/// </summary>
public class InvalidValueException : Exception
{
    public readonly string msg;
    public InvalidValueException(string m) { msg = m;}
    public override string Message => $"Invalid {msg}";
}

/// <summary>
/// Exception in case the desired amount is not in stock
/// </summary>
public class OutOfStockException : Exception
{
    public int amount { get; set; }
    public int productId { get; set; }
    public OutOfStockException(int pId,int amnt) { amount = amnt; productId = pId; }
    public override string Message => $"There are only {amount} items from product: {productId} in stock";
}
/// <summary>
/// Exception in case the product was not taken
/// </summary>
public class ItemNotExistException : Exception
{
    public override string Message => $"You have not yet taken this product";
}
/// <summary>
/// Exception in case the action cannot be performed
/// </summary>
public class IllegalActionException : Exception
{
    public readonly string msg;
    public IllegalActionException(string m) { msg = m; }
    public override string Message => msg;
}




