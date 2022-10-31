
using DO;

namespace Dal;
internal static class DataSource
{
    internal const int MaxOrder = 100;
    internal const int MaxProduct = 50;
    internal const int MaxOrderItem = 200;
    internal static Order[] orderArr = new Order[MaxOrder];
    internal static Product[] productArr = new Product[MaxProduct];
    internal static OrderItem[] orderItemArr = new OrderItem[MaxOrderItem];

    internal static class Config
    {
        internal static int productArrIdx = 0;
        internal static int orderItemArrIdx = 0;
        internal static int orderArrIdx = 0;

        private static int maxOrderItemId = 1;
        public static int MaxOrderItemId { get { return maxOrderItemId++; } }
 
        private static int maxOrderId = 1;
        public static int MaxOrderId { get { return maxOrderId++; } }
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

        (string, eCategory, double)[] prodNameCategoryPrice = new (string, eCategory, double)[10]
           {("Magnet siddur", eCategory.Siddur,52.5 ),
            ("Artistic Shabbat box", eCategory.Shabbat,200),
            ("pendant model siddur", eCategory.Siddur,60),
            ("Artistic status of Passover Haggadot", eCategory.Chaggim,300),
            ("Zimirot Shabbat lace model", eCategory.Shabbat,16.8),
            ("Sukkah decoration", eCategory.Chaggim,99),
            ("tehillim model Pearls", eCategory.Tehillim,70),
            ("prestigious crystal tehillim", eCategory.Tehillim,85),
            ("Mizmor L'todah", eCategory.Others,89),
            ("An artistic matchbox for Shabbat", eCategory.Shabbat,144)};

        bool notExists;
        int pId;
        string pName;
        double pPrice;
        int pInStock;
        eCategory pCategory;

        for (int i = 0; i < 10; i++)
        {
            do
            {
                notExists = true;
                Random rnd = new Random();
                pId = rnd.Next(100000, 1000000);
                for (int j = 0; j < Config.productArrIdx; j++)
                {
                    if (productArr[j].id == pId)
                    {
                        notExists = false;
                        break;
                    }
                }
            } while (!notExists);
            (pName, pCategory, pPrice) = prodNameCategoryPrice[i];
            if (i % 20 == 3)
                pInStock = 0;
            else
            {
                Random rnd = new Random();
                pInStock = rnd.Next(1, 15);
            }
            Product product = new Product(pId, pName, pPrice, pInStock, pCategory);
            productArr[Config.productArrIdx++] = product;
        }
    }


    private static void CreateOrderArr()
    {

        (string, string, string)[] OrderingDetails = new (string, string, string)[5]
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
            int idx = rnd.Next(0,5);
            (oCustomerName, oCustomerEmail, oCustomerAddress) = OrderingDetails[idx];
            rnd = new Random();
            int numDays = rnd.Next(20, 30);
            TimeSpan daysBefore = TimeSpan.FromDays(numDays);
            oOrderDate = DateTime.Now - daysBefore;
            if (i%10<8)
            {
                numDays = rnd.Next(1, 5);
                TimeSpan daysUntulShip = TimeSpan.FromDays(numDays);
                oShipDate = oOrderDate + daysUntulShip;        
            }
            else
            {
                oShipDate = DateTime.MinValue;
            }
            if (i % 10 < 6)
            {
                numDays = rnd.Next(3, 8);
                TimeSpan daysUntulDelivery = TimeSpan.FromDays(numDays);
                oDeliveryDate = oShipDate + daysUntulDelivery;
            }
            else
            {
                oDeliveryDate= DateTime.MinValue; 
            }

            Order order = new Order(oId, oCustomerName, oCustomerEmail, oCustomerAddress, oOrderDate, oShipDate, oDeliveryDate);
            orderArr[Config.orderArrIdx++] = order;
        }
    }

    private static void CreateOrderItemArr()
    {

        int id;
        int productId;
        int orderId;
        double price;
        int amount;

        for (int i = 0; i < orderArr.Length; i++)
        {
            Random rnd = new Random();
            int numProducts = rnd.Next(1, 5);
            for(int j=0;j< numProducts; j++)
            {
                id = Config.MaxOrderItemId;
                orderId = orderArr[i].id;
                int pIdx = rnd.Next(0, productArr.Length);
                productId = productArr[pIdx].id;
                price = productArr[pIdx].price;
                int amountOfProduct= rnd.Next(1, 10);
                if(amountOfProduct<= productArr[pIdx].inStock)
                {
                    amount = amountOfProduct;
                }
                else
                {
                    amount = productArr[pIdx].inStock;
                }
                productArr[pIdx].inStock -= amount;
                OrderItem orderItem = new OrderItem(id,productId, orderId, price, amount);
                orderItemArr[Config.orderItemArrIdx++] = orderItem;
            }                 
        }
    }
}

