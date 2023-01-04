namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

/// <summary>
/// This class implements the CRUD on the database functions for each product in the store.
/// </summary>
internal class Product : IProduct
{
    /// <summary>
    /// A function to add a new product to the Xml database.
    /// </summary>
    public int Create(DO.Product prod)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement> products = root.Elements("Product").ToList();
        XElement tmpProduct = products.Where(prd => Convert.ToInt32(prd.Element("Id")?.Value.ToString()) == prod.Id).FirstOrDefault();

        if (tmpProduct != null)
        {
            throw new IdAlreadyExistsException();
        }
        XElement el = new("Product",
                new XElement("Id", Convert.ToString(prod.Id)),
                new XElement("Name", prod.Name),
                new XElement("Price", Convert.ToDouble(prod.Price)),
                new XElement("InStock", Convert.ToString(prod.InStock)),
                new XElement("Category", Convert.ToString(prod.Category)));
        root.Add(el);
        root?.Save("..\\..\\..\\..\\xml\\product.xml");
        return prod.Id;
    }

    /// <summary>
    /// A function to delete a product from the database.
    /// </summary>
    public void Delete(int id)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement> products = root.Elements("Product").ToList();
        XElement tmpProduct = products.Where(prd => Convert.ToInt32(prd.Element("Id")?.Value.ToString()) == id).FirstOrDefault();
        if (tmpProduct == null)
        {
            throw new IdNotExistException("product");
        }
        tmpProduct.Remove();
        root?.Save("..\\..\\..\\..\\xml\\product.xml");

        //////////////////////////////////////////////////////
        //root.Element(tmpProduct.Name).Remove;
        ////////////////////////////////////////////////////////
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



//namespace Dal;
///// <summary>
///// This class implements the CRUD on the database functions for each product in the store.
///// </summary>
//public class DalProduct : IProduct
//{

//    /// <summary>
//    /// A function to delete a product from the database.
//    /// </summary>
//    public void Delete(int id)
//    {
//        Product product = DataSource.ProductList.Where(product => product.Id == id).FirstOrDefault();
//        if (product.Equals(default(Product)))
//        {
//            throw new IdNotExistException("product");
//        }
//        DataSource.ProductList.Remove(product);
//    }
//    /// <summary>
//    ///  A function to get the information about specific product in the database by ID.
//    /// </summary>
//    public Product Read(int id)
//    {
//        Product product = DataSource.ProductList.Where(product => product.Id == id).FirstOrDefault();
//        if (product.Equals(default(Product)))
//        {
//            throw new IdNotExistException("product");
//        }
//        return product;
//    }
//    /// <summary>
//    ///  A function to get the information about all the products in the database.
//    /// </summary>
//    IEnumerable<Product> ICrud<Product>.Read(Func<Product, bool> func)
//    {
//        List<Product> tmpProductList = DataSource.ProductList;
//        return func == null ? tmpProductList : tmpProductList.Where(func);
//    }
//    /// <summary>
//    ///  A function to update a specific product in the database. 
//    /// </summary>
//    public void Update(Product product)
//    {
//        Product originalProduct = DataSource.ProductList.Where(originalProduct => originalProduct.Id == product.Id).FirstOrDefault();
//        if (originalProduct.Equals(default(Product)))
//        {
//            throw new IdNotExistException("product");
//        }
//        DataSource.ProductList.Remove(originalProduct);
//        DataSource.ProductList.Add(product);
//    }

//    Product ICrud<Product>.ReadSingle(Func<Product, bool> func)
//    {
//        Product prod = DataSource.ProductList.Where(func).FirstOrDefault();
//        if (prod.Equals(default(Order)))
//        {
//            throw new ObjectNotExistException("product");
//        }
//        return prod;
//    }
//}

