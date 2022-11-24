public class IdNotExist  : Exception
{
    public override string Message => "ID is not exist";
}

public class IdAlreadyExists : Exception
{
    public override string Message => "Such an ID is already exists";
}
