using DalApi;
using Dal;
using BlApi;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = new DalList();
    public void CreateProd(BO.Product prod)
    {
        if(prod.Id < 1)
        {
            throw new InvalidID();
        }
        if (string.IsNullOrEmpty(prod.Name))
        {
            throw new InvalidName();
        }
        if (prod.Price < 0)
        {
            throw new InvalidPrice();
        }
        if(prod.InStock < 0)
        {
            throw new InvalidAmountInStock();
        }
///////////////////////////////////////////////
    }

public void DeleteProd(int Id)
    {

    }

    public BO.Product ReadProdCustomer(int Id)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidID();
            }
            DO.Product dP = Dal.product.Read(Id);
            BO.Product bP = new BO.Product();
            bP.Id = dP.Id;
            bP.Name = dP.Name;
            bP.Price = dP.Price;
            bP.Category = (BO.eCategory)dP.category;
            bP.InStock = dP.InStock;
            return bP;
        }
        catch (IdNotExist exc)
        {
            throw new DataError(exc);
        }
    }

    public BO.Product ReadProdManager(int Id)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidID();
            }
            DO.Product dP = Dal.product.Read(Id);
            BO.Product bP = new BO.Product();
            bP.Id = dP.Id;
            bP.Name = dP.Name;
            bP.Price = dP.Price;
            bP.Category = (BO.eCategory)dP.category;
            bP.InStock = dP.InStock;
            return bP;
        }
        catch (IdNotExist exc)
        {
            throw new DataError(exc);
        }
    }

    public IEnumerable<BO.ProductItem> ReadProdsCustomer()
    {
        IEnumerable<DO.Product> dProds = Dal.product.Read();
        IEnumerable<BO.ProductItem> bProds = new List<BO.ProductItem>(dProds.Count());
        foreach (DO.Product dP in dProds)
        {
            BO.ProductItem bP = new BO.ProductItem();
            bP.Id = dP.Id;
            bP.Name = dP.Name;
            bP.Price = dP.Price;
            bP.Category = (BO.eCategory)dP.category;
            bP.InStock = dP.InStock > 0 ? true : false;
            //??????bP.Amount=
            bProds.Append(bP);
        }
        return bProds;
    }

    public IEnumerable<BO.ProductForList> ReadProdsManager()
    {
        IEnumerable<DO.Product> dProds = Dal.product.Read();
        IEnumerable<BO.ProductForList> bProds = new List<BO.ProductForList>(dProds.Count());
        foreach (DO.Product dP in dProds)
        {
            BO.ProductForList bP = new BO.ProductForList();
            bP.Id = dP.Id;
            bP.Name = dP.Name;
            bP.Price = dP.Price;
            bP.Category = (BO.eCategory)dP.category;
            bProds.Append(bP);
        }
        return bProds;
    }

    public void UpdateProd(BO.Product prod)
    {

    }

}
