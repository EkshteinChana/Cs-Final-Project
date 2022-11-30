
namespace BlApi;

public class DataError : Exception
{
    public DataError(Exception inner,string m) : base(m, inner) { }
}
public class InvalidValue : Exception
{
    public readonly string msg;
    public InvalidValue(string m) { msg = m;}
    public override string Message => $"Invalid {msg}";
}

public class OutOfStock : Exception
{
    public int amount { get; set; }
    public int productId { get; set; }
    public OutOfStock(int pId,int amnt) { amount = amnt; productId = pId; }
    public override string Message => $"There are only {amount} items from product: {productId} in stock";
}

public class ItemNotExist : Exception
{
    public override string Message => $"You have not yet taken this product";
}

public class IllegalAction : Exception
{
    public readonly string msg;
    public IllegalAction(string m) { msg = m; }
    public override string Message => msg;
}




