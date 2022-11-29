﻿
namespace BlApi;

public class DataError : Exception
{
    public DataError(Exception inner) : base("", inner) { }
    public override string Message => InnerException.Message;
}
public class InvalidValue : Exception
{
    public readonly string msg;
    public InvalidValue(string m) { msg = m; }
    public override string Message => $"Invalid {msg} entered";
}

public class OutOfStock : Exception
{
    public int amount { get; set; }
    public OutOfStock(int amnt) { amount = amnt; }
    public override string Message => $"There are only {amount} items in stock";
}

public class ItemNotExist : Exception
{
    public override string Message => $"You have not yet taken this product";
}
public class InvalidOrderItem : Exception
{
    public string msg { get; set; }
    public InvalidOrderItem(string m) { msg = m; }
    public override string Message => $"Invalid order item - {msg}";
}
public class IllegalDeletion : Exception
{
    public readonly string msg;
    public IllegalDeletion(string m) { msg = m; }
    public override string Message => msg;
}


