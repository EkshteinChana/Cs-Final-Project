using DalApi;
using Dal;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = new DalList();
    public void CreateProd(BO.Product prod)
    {
        if(prod.Id < 1)
        {
            throw new Exception("Id is not valid");
        }
        if (string.IsNullOrEmpty(prod.Name))
        {
            throw new Exception("Name is not valid");
        }
        if (prod.Price < 0)
        {
            throw new Exception("Price is not valid");
        }

    }

    public void DeleteProd(int Id)
    {

    }

    public BO.Product ReadProdCustomer(int Id)
    {
        try
        {
            DO.Product dP = Dal.product.Read(Id);
            BO.Product bP = new BO.Product();
            bP.Id = dP.Id;
            bP.Name = dP.Name;
            bP.Price = dP.Price;
            bP.Category = (BO.eCategory)dP.category;
            bP.InStock = dP.InStock;
            return bP;
        }
        catch (Exception e)
        {
            throw new Exception("?????????");
        }
    }

    public BO.Product ReadProdManager(int Id)
    {
        try
        {
            DO.Product dP = Dal.product.Read(Id);
            BO.Product bP = new BO.Product();
            bP.Id = dP.Id;
            bP.Name = dP.Name;
            bP.Price = dP.Price;
            bP.Category = (BO.eCategory)dP.category;
            bP.InStock = dP.InStock;
            return bP;
        }
        catch (Exception e)
        {
            throw new Exception("?????????");
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
