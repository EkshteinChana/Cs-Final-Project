public class IdNotExist  : Exception
{
    public override string Message => "ID is not exist";
}

public class IdAlreadyExists : Exception
{
    public override string Message => "Such an ID is already exists";
}

public class IncorrectDate : Exception
{
    public override string Message => "Invalid date entered";
}


public class UnKnownAction : Exception
{
    public override string Message => "Not exist date entered";
}