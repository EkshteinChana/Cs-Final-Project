using DalApi;
using BlApi;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = DalApi.Factory.Get();

    /// <summary>
    /// A private help function for checking the integrity of the data in the logical layer for adding/updating a product.
    /// </summary>
    private void checkOrdValues(BO.Product prod)
    {
        if (prod.Id < 1)
        {
            throw new InvalidValue("ID");
        }
        if (string.IsNullOrEmpty(prod.Name))
        {
            throw new InvalidValue("name");
        }
        if (prod.Price < 0)
        {
            throw new InvalidValue("price");
        }
        if (((int?)prod.Category < 0) || ((int?)prod.Category > 4))
        {
            throw new InvalidValue("category");
        }
        if (prod.InStock < 0)
        {
            throw new InvalidValue("amount in stock");
        }
    }

    /// <summary>
    /// A private help function to convert DO.Product entity to BO.ProductForList entity.
    /// </summary>
    private BO.ProductForList convertDoProdToBoProdForLst(DO.Product dP)
    {
        BO.ProductForList bP = new BO.ProductForList();
        foreach (var prop in dP.GetType().GetProperties())
        {
            if (prop.Name != "InStock" && prop.Name != "Category")
                bP.GetType().GetProperty(prop.Name)?.SetValue(bP, prop.GetValue(dP));
        }
        bP.Category = (BO.eCategory?)dP.Category;
        return bP;
    }

    /// <summary>
    /// A private help function to convert BO.Product entity to DO.Product entity.
    /// </summary>
    private DO.Product convertBoProdToDoProd(BO.Product prod)
    {
        object tmpNewProd = new DO.Product();
        foreach (var prop in prod.GetType().GetProperties())
        {
            if (prop.Name != "Category")
            {
                tmpNewProd.GetType().GetProperty(prop.Name)?.SetValue(tmpNewProd, prop.GetValue(prod));
            }
        }
        DO.Product newProd = (DO.Product)tmpNewProd;
        newProd.Category = (DO.eCategory?)prod.Category;
        return newProd;
    }

    /// <summary>
    /// A function that receives product data, checks its integrity 
    /// and sends a request to the data layer to add such a product.
    /// </summary>
    public int CreateProd(BO.Product prod)
    {
        int id = 0;
        prod.Id = 111;//Temporary value for the checkOrdValues function - will be updated later.
        checkOrdValues(prod);
        DO.Product newProd = convertBoProdToDoProd(prod);
        bool tryId = true;
        while (tryId)//Create an ID
        {
            tryId = false;
            try
            {
                Random rnd = new Random();
                newProd.Id = rnd.Next(100000, 1000000);
                id = Dal.product.Create(newProd);
            }
            catch (IdAlreadyExistsException)
            {
                tryId = true;
            }
        }
        return id;
    }

    /// <summary>
    /// A function that receives a product ID, checks that it does not exist in orders 
    /// and sends a request to the data layer to delete it from the list
    /// </summary>
    public void DeleteProd(int Id)
    {
        IEnumerable<DO.OrderItem> orderItemsList = Dal.orderItem.Read();
        DO.OrderItem ordWithProd = orderItemsList.Where(o => o.ProductId == Id).FirstOrDefault();
        if (!ordWithProd.Equals(default(DO.OrderItem)))
        {
            throw new IllegalAction("It is not possible to delete an existing product in an order");
        }
        try
        {
            Dal.product.Delete(Id);
        }
        catch (IdNotExistException err)
        {
            throw new DataError(err, "Data Error: ");
        }
    }
    /// <summary>
    /// A function that returns an entity to display product data for a customer screen 
    /// by referring to the data layer using an ID.
    /// </summary>
    public BO.ProductItem ReadProdCustomer(int Id, BO.Cart cart)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidValue("ID");
            }
            DO.Product dP = Dal.product.Read(Id);
            BO.ProductItem bP = new();
            foreach (var prop in dP.GetType().GetProperties())
            {
                if (prop.Name != "InStock" && prop.Name != "Category")
                    bP.GetType().GetProperty(prop.Name)?.SetValue(bP, prop.GetValue(dP));
            }
            bP.Category = (BO.eCategory?)dP.Category;
            bP.InStock = (dP.InStock > 0) ? true : false;
            bool exist = false;
            if (cart.Items != null)
            {
                foreach (BO.OrderItem i in cart.Items)
                {
                    if (i.ProductId == bP.Id)//The product is already in the shopping cart
                    {
                        exist = true;
                        bP.Amount = i.Amount;
                        break;
                    }
                }
            }
            if (exist == false)
            {
                bP.Amount = 0;
            }
            return bP;
        }
        catch (IdNotExistException exc)
        {
            throw new DataError(exc, "Data Error: ");
        }
    }

    /// <summary>
    /// A function that returns an entity to display product data for the manager screen 
    /// by referring to the data layer using an ID.
    /// </summary>
    public BO.Product ReadProdManager(int Id)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidValue("ID");
            }
            DO.Product dP = Dal.product.Read(Id);
            BO.Product bP = new BO.Product();
            foreach (var prop in dP.GetType().GetProperties())
            {
                if (prop.Name != "Category")
                    bP.GetType().GetProperty(prop.Name)?.SetValue(bP, prop.GetValue(dP));
            }
            bP.Category = (BO.eCategory?)dP.Category;
            return bP;
        }
        catch (IdNotExistException exc)
        {
            throw new DataError(exc, "Data Error: ");
        }
    }

    /// <summary>
    /// A function to read the list of products
    /// </summary>
    public IEnumerable<BO.ProductForList> ReadProdsList()
    {
        IEnumerable<DO.Product> dProds = Dal.product.Read();
        IEnumerable<BO.ProductForList> bProds = new List<BO.ProductForList>(dProds.Count());
        List<BO.ProductForList> bProdsList = bProds.ToList();
        foreach (DO.Product dP in dProds)
        {
            BO.ProductForList bP = convertDoProdToBoProdForLst(dP);
            bProdsList.Add(bP);
        }
        return bProdsList;
    }

    /// <summary>
    /// A function to read a list of products by specific category
    /// </summary>
    public IEnumerable<BO.ProductForList?> ReadProdsByCategory(BO.eCategory category)
    {
        DO.eCategory ctgry = (DO.eCategory)category;
        IEnumerable<DO.Product> dProds = Dal.product.Read((DO.Product p) => p.Category == ctgry);
        IEnumerable<BO.ProductForList> bProds = new List<BO.ProductForList>(dProds.Count());
        List<BO.ProductForList> bProdsList = bProds.ToList();
        foreach (DO.Product dP in dProds)
        {
            BO.ProductForList bP = convertDoProdToBoProdForLst(dP);
            bProdsList.Add(bP);
        }
        return bProdsList;
    }

    /// <summary>
    /// A function that receives product data, checks their integrity
    ///and sends a request to the data layer to update the product with such an ID.
    /// </summary>
    public void UpdateProd(BO.Product prod)
    {
        checkOrdValues(prod);
        DO.Product newProd = convertBoProdToDoProd(prod);
        try
        {
            Dal.product.Update(newProd);
        }
        catch (IdNotExistException err)
        {
            throw new DataError(err, "Data Error: ");
        }
    }

}
