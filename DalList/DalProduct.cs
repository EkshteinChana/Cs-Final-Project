using DO;

namespace Dal;

public static class DalProduct
{
    public static int CreateProduct(Product product)
    {
        DataSource.ProductArr.Add(product);
        return product.Id;
        //if (DataSource.Config.productArrIdx == DataSource.MaxProduct)
        //{
        //    throw new Exception("The product set is full, it is not possible to add an product");
        //}
        //DataSource.productArr[DataSource.Config.productArrIdx++] = product;
        //return product.Id;
    }

    public static Product ReadProduct(int id)
    {
        Product product = DataSource.ProductArr.Where(product => product.Id == id).FirstOrDefault();
        if (product.Equals(default(Product)))
        {
            throw new Exception("No product exists with this ID ");
        }
        return product;

        //for (int i = 0; i < DataSource.Config.productArrIdx; i++)
        //{
        //    if (DataSource.productArr[i].Id == id)
        //        return DataSource.productArr[i];
        //}
        //throw new Exception("No product exists with this ID ");
    }

    public static Product[] ReadProduct()
    {
        Product[] tmpProductArr = new Product[DataSource.ProductArr.Count];
        DataSource.ProductArr.CopyTo(tmpProductArr);
        return tmpProductArr;
        //Product[] tmpProductArr = new Product[DataSource.Config.productArrIdx];
        //for (int i = 0; i < DataSource.Config.productArrIdx; i++)
        //{
        //    tmpProductArr[i] = DataSource.productArr[i];
        //}
        //return tmpProductArr;
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
        //int i;
        //for (i = 0; i < DataSource.Config.productArrIdx; i++)
        //{
        //    if (DataSource.productArr[i].Id == product.Id)
        //    {
        //        DataSource.productArr[i] = product;
        //        return;
        //    }
        //}
        //if (i == DataSource.Config.orderArrIdx)
        //{
        //    throw new Exception("No product exists with this ID ");
        //}
    }

    public static void DeleteProduct(int id)
    {
        Product product = DataSource.ProductArr.Where(product => product.Id == id).FirstOrDefault();
        if (product.Equals(default(Product)))
        {
            throw new Exception("No product exists with this ID ");
        }
        DataSource.ProductArr.Remove(product);
        //for (int i = 0; i < DataSource.Config.productArrIdx; i++)
        //{
        //    if (DataSource.productArr[i].Id == id)
        //    {
        //        DataSource.productArr[i] = DataSource.productArr[DataSource.Config.productArrIdx-1];
        //        DataSource.Config.productArrIdx -= 1;
        //        return;
        //    }
        //}
        //throw new Exception("No product exists with this ID ");
    }

}

