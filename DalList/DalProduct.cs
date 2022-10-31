using DO;

namespace Dal;

  internal class DalProduct
    {

    public static int CreateProduct(Product product)
    {
        if (DataSource.Config.productArrIdx == DataSource.MaxProduct)
        {
            throw new Exception("The product set is full, it is not possible to add an product");
        }
        DataSource.productArr[DataSource.Config.productArrIdx++] = product;
        return product.id;
    }

    public static Product ReadProduct(int id)
    {
        for (int i = 0; i <= DataSource.Config.productArrIdx; i++)
        {
            if (DataSource.productArr[i].id == id)
                return DataSource.productArr[i];
        }
        throw new Exception("No product exists with this ID ");
    }

    public static Product[] ReadProduct()
    {
        Product[] tmpProductArr = new Product[DataSource.productArr.Length];
        for (int i = 0; i < DataSource.productArr.Length; i++)
        {
            tmpProductArr[i] = DataSource.productArr[i];
        }
        return tmpProductArr;
    }

    public static Product UpdateProduct(Product product)
    {
        for (int i = 0; i <= DataSource.Config.productArrIdx; i++)
        {
            if (DataSource.productArr[i].id == product.id)
            {
                DataSource.productArr[i] = product;
            }
        }
        throw new Exception("No product exists with this ID ");
    }

    public static void DeleteProduct(int id)
    {
        for (int i = 0; i <= DataSource.Config.productArrIdx; i++)
        {
            if (DataSource.productArr[i].id == id)
            {
                DataSource.productArr[i] = DataSource.productArr[DataSource.Config.productArrIdx];
                DataSource.Config.productArrIdx -= 1;
                break;
            }
        }
        throw new Exception("No product exists with this ID ");
    }

}

