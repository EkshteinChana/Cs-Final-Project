
using DO;

namespace Dal;
public class DataSource
{
    public const int MaxOrder = 100;
    public const int MaxProduct = 50;
    public const int MaxOrderItem = 200;
    public static Order[] OrderArr = new Order[MaxOrder];
    public static Product[] ProductArr = new Product[MaxProduct];
    public static OrderItem[] OrderItemArr = new OrderItem[MaxOrderItem];

    public static class Config
    {
        public static int ProductArrIdx = 0;
        public static int OrderItemArrIdx = 0;
        public static int OrderArrIdx = 0;

        private static int s_maxOrderItemId = 1;
        public static int MaxOrderItemId { get { return s_maxOrderItemId++; } }
 
        private static int s_maxOrderId = 1;
        public static int MaxOrderId { get { return s_maxOrderId++; } }
    }

    static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize()
    {
        CreateProductArr();
        CreateOrderArr();
        CreateOrderItemArr();
    }


    private static void CreateProductArr()
    {
        // Temp help array 
        (string, eCategory, double)[] prodNameCategoryPrice = 
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
        int pId;
        string pName;
        double pPrice;
        int pInStock;
        eCategory pCategory;
        
        for (int i = 0; i < 10; i++)
        {
            do      //Rand id and make sure is unique in the array 
            {
                notExists = true;
                Random rnd = new Random();
                pId = rnd.Next(100000, 1000000);
                for (int j = 0; j < Config.ProductArrIdx; j++)
                {
                    if (ProductArr[j].Id == pId)
                    {
                        notExists = false;
                        break;
                    }
                }
            } while (!notExists);

            (pName, pCategory, pPrice) = prodNameCategoryPrice[i];
            if (i % 20 == 0)
                pInStock = 0;
            else
            {
                Random rnd = new Random();
                pInStock = rnd.Next(1, 500);
            }
            Product product = new Product(pId, pName, pPrice, pInStock, pCategory);
            ProductArr[Config.ProductArrIdx++] = product;
        }
    }


    private static void CreateOrderArr()
    {

        (string, string, string)[] CustomerDetails = new (string, string, string)[5]
           {("Shimon Cohen","shimon66@gmail.com", "Shaulzon 66" ),
            ("Daniel Levi","levi05276@gmail.com", "Sorotskin 16"),
            ("Reuven Katz","reuvenkatz123@gmail.com","Idelson 24" ),
            ("Mordechai Rabinowitz","Rabinowitz678@gmail.com", "Franc 12"),
            ("Sarah Klein","klein424@gmail.com", "Gulak 424")};
 
        int oId;
        string oCustomerName;
        string oCustomerEmail;
        string oCustomerAddress;
        DateTime oOrderDate;
        DateTime oShipDate;
        DateTime oDeliveryDate;

        for (int i = 0; i < 20; i++)
        {
            oId = Config.MaxOrderId;
            Random rnd = new Random();
            int idx = rnd.Next(0,5);  // Rand who will be the customer of the current order
            (oCustomerName, oCustomerEmail, oCustomerAddress) = CustomerDetails[idx];
            rnd = new Random();
            int numDays = rnd.Next(20, 30);
            TimeSpan daysBefore = TimeSpan.FromDays(numDays);
            oOrderDate = DateTime.Now - daysBefore;
            if (i%10<8)  // 80% have ship date
            {
                numDays = rnd.Next(1, 5);
                TimeSpan daysUntillShip = TimeSpan.FromDays(numDays);
                oShipDate = oOrderDate + daysUntillShip;
                if (i % 10 < 6)// 60% from them have delivery date
                {
                    numDays = rnd.Next(3, 8);
                    TimeSpan daysUntilDelivery = TimeSpan.FromDays(numDays);
                    oDeliveryDate = oShipDate + daysUntilDelivery;
                }
                else
                {
                    oDeliveryDate = DateTime.MinValue;
                }
            }
            else
            {
                oDeliveryDate = DateTime.MinValue;
                oShipDate = DateTime.MinValue;
            }
            
            Order order = new Order(oId, oCustomerName, oCustomerEmail, oCustomerAddress, oOrderDate, oShipDate, oDeliveryDate);
            OrderArr[Config.OrderArrIdx++] = order;
        }
    }

    private static void CreateOrderItemArr()
    {

        int id;
        int productId;
        int orderId;
        double price;
        int amount;

        for (int i = 0; i < Config.OrderArrIdx; i++) // Create 1-4 orderItems for each order
        {
            Random rnd = new Random();
            int sumDifProducts = rnd.Next(1, 5); // the sum of the different typs products in the order
            for(int j=0;j< sumDifProducts; j++)
            {
                id = Config.MaxOrderItemId;
                orderId = OrderArr[i].Id;
                bool exist;
                int pIdx;
                do
                {
                    exist = false;
                    pIdx = rnd.Next(0, Config.ProductArrIdx);  // pIdx is the location in the Products array
                    int pBarcode = ProductArr[pIdx].Id; 
                        for (int k = 0; k < j ; k++)
                        {
                            if(OrderItemArr[Config.OrderItemArrIdx - k - 1 ].ProductId == pBarcode)
                            {
                                exist = true;
                            }
                        }
                    } while (exist);
                productId = ProductArr[pIdx].Id;
                int amountOfProduct= rnd.Next(1, 10);
                if(amountOfProduct<= ProductArr[pIdx].InStock)
                {
                    amount = amountOfProduct; // The amount of each product
                }
                else
                {
                    amount = ProductArr[pIdx].InStock;
                }
                ProductArr[pIdx].InStock -= amount;
                price = amount * ProductArr[pIdx].Price; 
                OrderItem orderItem = new OrderItem(id,productId, orderId, price, amount);
                OrderItemArr[Config.OrderItemArrIdx++] = orderItem;
            }                 
        }
    }
}

