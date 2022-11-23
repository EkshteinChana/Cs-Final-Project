
namespace BlApi;

public class DataError : Exception
{
    public DataError(Exception inner) : base("", inner) { }
    public override string Message => InnerException.Message;
}
//public class InvalidValue : Exception
//{
//    public InvalidValue(string property) { }
//    public override string Message => "Invalid Value";
//}

public class InvalidID : Exception
{
    public override string Message => "Invalid ID entered";
}

public class InvalidName : Exception
{
    public override string Message => "Invalid name entered";
}

public class InvalidPrice : Exception
{
    public override string Message => "Invalid price entered";
}

public class InvalidAmountInStock : Exception
{
    public override string Message => "Invalid amount in stock entered";
}

public class OutOfStock : Exception
{
    public override string Message => "Out of stock";
}

//public class InvalidDate : Exception
//{
//    public override string Message => "Invalid date entered";
//}
