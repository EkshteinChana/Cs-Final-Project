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
        
    }

    public BO.Product ReadProdManager(int Id)
    {
        
    }

    public IEnumerable<BO.ProductItem> ReadProdsCustomer()
    {
        
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

    public void UpdateProd(Product prod)
    {
        
    }
}
