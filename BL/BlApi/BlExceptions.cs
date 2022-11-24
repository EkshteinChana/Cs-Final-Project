
namespace BlApi;

public class DataError : Exception
{
    public DataError(Exception inner) : base("", inner) { }
    public override string Message => InnerException.Message;
}
public class InvalidValue : Exception
{
    public string msg { get; set; }
    public InvalidValue(string m) { msg = m; }
    public override string Message => $"Invalid {msg} entered";

}

public class OutOfStock : Exception
{
    public override string Message => "Out of stock";
}

