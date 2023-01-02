using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class Order : IOrder
{
    public int Create(DO.Order t)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order> Read(Func<DO.Order, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order ReadSingle(Func<DO.Order, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order t)
    {
        throw new NotImplementedException();
    }
}
