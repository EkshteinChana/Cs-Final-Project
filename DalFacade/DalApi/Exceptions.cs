public class IdNotExistException  : Exception
{
    public readonly string msg;
    public IdNotExistException(string _msg) { msg = _msg; }
    public override string Message => $"The {msg} ID is not exist";
}

public class IdAlreadyExistsException : Exception
{
    public override string Message => "Such an ID is already exists";
}


public class ObjectNotExistException : Exception
{
    public readonly string obj ;
    public ObjectNotExistException( string _obj )
    {
        obj = _obj;
    }
    public override string Message => $"No such { obj } exists ";
}
