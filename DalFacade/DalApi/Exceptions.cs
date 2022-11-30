public class IdNotExist  : Exception
{
    public readonly string msg;
    public IdNotExist(string m) { msg = m; }
    public override string Message => $"The {msg} ID is not exist";
}

public class IdAlreadyExists : Exception
{
    public override string Message => "Such an ID is already exists";
}


//public readonly string msg;
//public InvalidValue(string m) { msg = m; }
//public override string Message => $"Invalid {msg}";
//}