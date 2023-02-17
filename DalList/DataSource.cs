using DO;

namespace Dal;
/// <summary>
/// This class initializes the entity data lists.
/// </summary>
public class DataSource
{
    public static List<Order> OrderList = new List<Order>();
    public static List<Product> ProductList = new List<Product>();
    public static List<OrderItem> OrderItemList = new List<OrderItem>();
    /// <summary>
    /// This class contains an ID for all the data entities.
    /// </summary>
    public static class Config
    {
        private static int s_maxOrderItemId = 1;
        public static int MaxOrderItemId { get { return s_maxOrderItemId++; } }

        private static int s_maxOrderId = 1;
        public static int MaxOrderId { get { return s_maxOrderId++; } }
    }

    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// This is the main function in DataSource, here we call the functions to initialize the data entities.
    /// </summary>
    private static void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        CreateOrderItemList();
    }

    /// <summary>
    /// A function to initialize the products list in the database
    /// </summary>
    private static void CreateProductList()
    {
        // Temp help array 
        (string?, eCategory?, double)[] prodNameCategoryPrice =
           {("Magnet siddur", eCategory.Siddur,52.5 ),
            ("Artistic Shabbat box", eCategory.Shabbat,200),
            ("pendant model siddur", eCategory.Siddur,60),
            ("Artistic status of Passover Haggadot", eCategory.Chaggim,300),
            ("Zmiroth Shabbos lace model", eCategory.Shabbat,16.8),
            ("Sukkah decoration", eCategory.Chaggim,99),
            ("tehillim model Pearls", eCategory.Tehillim,70),
            ("prestigious crystal tehillim", eCategory.Tehillim,85),
            ("Mizmor L'todah", eCategory.Others,89),
            ("An artistic matchbox for Shabbos", eCategory.Shabbat,144)};

        bool notExists;
        for (int i = 0; i < 10; i++)
        {
            Product tmpProd = new Product();
            do      //Rand id and make sure is unique in the array 
            {
                notExists = true;
                Random rnd = new Random();
                tmpProd.Id = rnd.Next(100000, 1000000);
                for (int j = 0; j < ProductList.Count; j++)
                {
                    if (ProductList[j].Id == tmpProd.Id)
                    {
                        notExists = false;
                        break;
                    }
                }
            } while (!notExists);

            (tmpProd.Name, tmpProd.Category, tmpProd.Price) = prodNameCategoryPrice[i];
            if (i % 20 == 0)
                tmpProd.InStock = 0;
            else
            {
                Random rnd = new Random();
                tmpProd.InStock = rnd.Next(1, 500);
            }
            ProductList.Add(tmpProd);
        }
    }

    /// <summary>
    /// A function to initialize the orders list in the database
    /// </summary>
    private static void CreateOrderList()
    {
        (string?, string?, string?)[] CustomerDetails = new (string?, string?, string?)[5]
           {("Shimon Cohen","shimon66@gmail.com", "Shaulzon 66" ),
            ("Daniel Levi","levi05276@gmail.com", "Sorotskin 16"),
            ("Reuven Katz","reuvenkatz123@gmail.com","Idelson 24" ),
            ("Mordechai Rabinowitz","Rabinowitz678@gmail.com", "Franc 12"),
            ("Sarah Klein","klein424@gmail.com", "Gulak 424")};

        for (int i = 0; i < 20; i++)
        {
            Order tmpOrd = new Order();
            tmpOrd.Id = Config.MaxOrderId;
            Random rnd = new Random();
            int idx = rnd.Next(0, 5);  // Rand who will be the customer of the current order
            (tmpOrd.CustomerName, tmpOrd.CustomerEmail, tmpOrd.CustomerAddress) = CustomerDetails[idx];
            rnd = new Random();
            int numDays = rnd.Next(20, 30);
            TimeSpan daysBefore = TimeSpan.FromDays(numDays);
            tmpOrd.OrderDate = DateTime.Now - daysBefore;
            if (i % 10 < 8)  // 80% have ship date
            {
                numDays = rnd.Next(1, 5);
                TimeSpan daysUntillShip = TimeSpan.FromDays(numDays);
                tmpOrd.ShipDate = tmpOrd.OrderDate + daysUntillShip;
                if (i % 10 < 6)// 60% from them have delivery date
                {
                    numDays = rnd.Next(3, 8);
                    TimeSpan daysUntilDelivery = TimeSpan.FromDays(numDays);
                    tmpOrd.DeliveryDate = tmpOrd.ShipDate + daysUntilDelivery;
                }
                else
                {
                    tmpOrd.DeliveryDate = null;
                }
            }
            else
            {
                tmpOrd.DeliveryDate = null;
                tmpOrd.ShipDate = null;
            }
            OrderList.Add(tmpOrd);
        }
    }
    /// <summary>
    /// A function to initialize the items in order list in the database
    /// </summary>
    private static void CreateOrderItemList()
    {
        int amount;
        for (int i = 0; i < OrderList.Count; i++) // Create 1-4 orderItems for each order
        {
            OrderItem tmpOrdItem = new OrderItem();
            Random rnd = new Random();
            int sumDifProducts = rnd.Next(1, 5); // the sum of the different typs products in the order
            for (int j = 0; j < sumDifProducts; j++)
            {
                tmpOrdItem.Id = Config.MaxOrderItemId;
                tmpOrdItem.OrderId = OrderList[i].Id;
                bool exist;
                int pIdx;
                do
                {
                    exist = false;                    
                    pIdx = rnd.Next(0, ProductList.Count);  // pIdx is the location in the Products array
                    if (ProductList[pIdx].InStock == 0)
                        exist = true;
                    int pBarcode = ProductList[pIdx].Id;
                    for (int k = 0; k < j; k++)
                    {
                        if (OrderItemList[OrderItemList.Count - k - 1].ProductId == pBarcode)
                        {
                            exist = true;
                        }
                    }
                } while (exist);
                tmpOrdItem.ProductId = ProductList[pIdx].Id;
                int amountOfProduct = rnd.Next(1, 10);
                if (amountOfProduct <= ProductList[pIdx].InStock)
                {
                    amount = amountOfProduct; // The amount of each product
                }
                else
                {
                    amount = ProductList[pIdx].InStock;
                }
                Product product = ProductList[pIdx];
                product.InStock -= amount;
                ProductList[pIdx] = product;
                tmpOrdItem.Price = ProductList[pIdx].Price;
                tmpOrdItem.Amount = amount;
                OrderItemList.Add(tmpOrdItem);
            }
        }
    }
}

