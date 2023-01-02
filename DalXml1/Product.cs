using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

/// <summary>
/// This class implements the CRUD on the database functions for each product in the store.
/// </summary>
internal class Product : IProduct
{
    //public Product()
    //{
    //    //read
    //    //StreamReader product
    //}
    public int Create(DO.Product t)
    {
        XElement? root = XDocument.Load("..\\..\\product.xml").Root;
        List<XElement> products = (List<XElement>)root.Elements("Product");
        XElement tmpProduct = products.Where(prod => prod.Element("Id")?.Value == t.Id).FirstOrDefault();
        if (!tmpProduct.Equals(default(Product)))
        {
            throw new IdAlreadyExistsException();
        }
        DataSource.ProductList.Add(product);
        return product.Id;
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



//namespace Dal;
///// <summary>
///// This class implements the CRUD on the database functions for each product in the store.
///// </summary>
//public class DalProduct : IProduct
//{
//    /// <summary>
//    /// A function to add a new product to the database.
//    /// </summary>
//    public int Create(Product product)
//    {
//        Product tmpProduct = DataSource.ProductList.Where(prod => prod.Id == product.Id).FirstOrDefault();
//        if (!tmpProduct.Equals(default(Product)))
//        {
//            throw new IdAlreadyExistsException();
//        }
//        DataSource.ProductList.Add(product);
//        return product.Id;
//    }
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

