using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class OrderItem : IOrderItem
{
    public int Create(DO.OrderItem t)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem> Read(Func<DO.OrderItem, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem ReadSingle(Func<DO.OrderItem, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem t)
    {
        throw new NotImplementedException();
    }
}
