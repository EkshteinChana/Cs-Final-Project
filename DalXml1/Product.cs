using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;


internal class Product : IProduct
{
    public Product()
    {
        //read
        //StreamReader product
    }
    public int Create(DO.Product t)
    {
        throw new NotImplementedException();
        //write
        //save
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Product Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product> Read(Func<DO.Product, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Product ReadSingle(Func<DO.Product, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product t)
    {
        throw new NotImplementedException();
    }
}
