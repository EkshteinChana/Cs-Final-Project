using DO;

namespace Dal;

public static class DalProduct
{
    public static int CreateProduct(Product product)
    {
        DataSource.ProductArr.Add(product);
        return product.Id;
    }

    public static Product ReadProduct(int id)
    {
        Product product = DataSource.ProductArr.Where(product => product.Id == id).FirstOrDefault();
        if (product.Equals(default(Product)))
        {
            throw new Exception("No product exists with this ID ");
        }
        return product;
    }

    public static Product[] ReadProduct()
    {
        Product[] tmpProductArr = new Product[DataSource.ProductArr.Count];
        DataSource.ProductArr.CopyTo(tmpProductArr);
        return tmpProductArr;
    }

    public static void UpdateProduct(Product product)
    {
        Product originalProduct = DataSource.ProductArr.Where(originalProduct => originalProduct.Id == product.Id).FirstOrDefault();
        if (originalProduct.Equals(default(Product)))
        {
            throw new Exception("No product exists with this ID ");
        }
        DataSource.ProductArr.Remove(originalProduct);
        DataSource.ProductArr.Add(product);
    }

    public static void DeleteProduct(int id)
    {
        Product product = DataSource.ProductArr.Where(product => product.Id == id).FirstOrDefault();
        if (product.Equals(default(Product)))
        {
            throw new Exception("No product exists with this ID ");
        }
        DataSource.ProductArr.Remove(product);
    }

}

