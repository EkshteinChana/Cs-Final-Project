using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlProduct : IProduct
{
    private IDal Dal = new DalList();
    public void CreateProd(BO.Product prod)
    {
        
    }

    public void DeleteProd(int Id)
    {
        
    }

    public BO.Product ReadProdCustomer(int Id)
    {
        try
        {
            DO.Product dP= Dal.product.Read(Id);
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
            bP.InStock = dP.InStock>0 ? true : false;
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
