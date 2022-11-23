using DO;
using DalApi;

namespace Dal;
/// <summary>
/// This class implements the CRUD on the database functions for each product in the store.
/// </summary>
public class DalProduct : IProduct
{
    /// <summary>
    /// A function to add a new product to the database.
    /// </summary>
    public int Create(Product product)
    {
        Product tmpProduct = DataSource.ProductList.Where(prod => prod.Id == product.Id).FirstOrDefault();
        if (!tmpProduct.Equals(default(Product)))
        {
            throw new IdAlreadyExists();
        }
        DataSource.ProductList.Add(product);
        return product.Id;
    }
    /// <summary>
    /// A function to delete a product from the database.
    /// </summary>
    public void Delete(int id)
    {
        Product product = DataSource.ProductList.Where(product => product.Id == id).FirstOrDefault();
        if (product.Equals(default(Product)))
        {
            throw new IdNotExist();
        }
        DataSource.ProductList.Remove(product);
    }
    /// <summary>
    ///  A function to get the information about specific product in the database by ID.
    /// </summary>
    public Product Read(int id)
    {
        Product product = DataSource.ProductList.Where(product => product.Id == id).FirstOrDefault();
        if (product.Equals(default(Product)))
        {
            throw new IdNotExist();
        }
        return product;
    }
    /// <summary>
    ///  A function to get the information about all the products in the database.
    /// </summary>
    public IEnumerable<Product> Read()
    {
        List<Product> tmpProductList = new List<Product>(DataSource.ProductList.Count);
        tmpProductList = DataSource.ProductList;
        return tmpProductList;
    }
    /// <summary>
    ///  A function to update a specific product in the database. 
    /// </summary>
    public void Update(Product product)
    {
        Product originalProduct = DataSource.ProductList.Where(originalProduct => originalProduct.Id == product.Id).FirstOrDefault();
        if (originalProduct.Equals(default(Product)))
        {
            throw new IdNotExist();
        }
        DataSource.ProductList.Remove(originalProduct);
        DataSource.ProductList.Add(product);
    }
}

