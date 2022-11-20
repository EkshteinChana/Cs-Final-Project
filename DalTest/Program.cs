/// <summary>
/// Stage 1 by Ozer Ester 214255705 and Ekshtein Chana 213868631
/// </summary>
/// 

using Dal;
using DO;
using System.Xml.Linq;

char choice;
DataSource ds = new DataSource();

//=========================== Order functions
void AddOrder()
{
    string oCustomerName, oCustomerEmail, oCustomerAddress;
    int numDays, oId;
    DateTime oDate, shipDate, deliveryDate;
    Console.WriteLine("\nEnter your details:\n name-");
    oCustomerName = Console.ReadLine();
    Console.WriteLine("email-");
    oCustomerEmail = Console.ReadLine();
    Console.WriteLine("address-");
    oCustomerAddress = Console.ReadLine();

    oId = DataSource.Config.MaxOrderId;
    oDate = DateTime.Now;
    shipDate = DateTime.MinValue;
    deliveryDate = DateTime.MinValue;
    Order newOrder = new Order(oId, oCustomerName, oCustomerEmail, oCustomerAddress, oDate, shipDate, deliveryDate);
    DalOrder.Create(newOrder);
}

void WatchOrder()
{
    Console.WriteLine("\nEnter the order ID for watching: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    Order tmpOrder = DalOrder.Read(oId);
    Console.WriteLine(tmpOrder + "\n");
}

void WatchOrderList()
{
    int size = DataSource.OrderList.Count;
    Order[] ordList = new Order[size];
    ordList = DalOrder.Read();
    foreach (Order order in ordList)
    {
        Console.WriteLine(order);
    }
}

void UpdateOrder()
{
    string oCustomerName, oCustomerEmail, oCustomerAddress, shipDateS, deliveryDateS;
    DateTime oDate, shipDate, deliveryDate;
    int numDays, oId;

    Console.WriteLine("\nEnter the order id you want to update: ");
    oId = Convert.ToInt32(Console.ReadLine());
    Order tmpOrd = DalOrder.ReadOrder(oId);
    Console.WriteLine(tmpOrd + "\n");
    Console.WriteLine("\nEnter your details:\n name- ");
    oCustomerName = Console.ReadLine();
    Console.WriteLine("\nemail- ");
    oCustomerEmail = Console.ReadLine();
    Console.WriteLine("\naddress- ");
    oCustomerAddress = Console.ReadLine();
    oDate = tmpOrd.OrderDate;
    Console.WriteLine("\nEnter the order shipping date: ");

    bool correctInput = false;
    shipDateS= Console.ReadLine();
    correctInput = DateTime.TryParse(shipDateS, out shipDate);
    if (!correctInput)
    {
        throw new Exception("\nYou have entered an incorrect shipDate.");
    }

    Console.WriteLine("\nEnter the order delivering date: ");
    deliveryDateS = Console.ReadLine();
    correctInput = DateTime.TryParse(deliveryDateS, out deliveryDate);
    if (!correctInput)
    {
        throw new Exception("\nYou have entered an incorrect deliveryDate.");
    }

    Order tmpOrder = new Order(oId, oCustomerName, oCustomerEmail, oCustomerAddress, oDate, shipDate, deliveryDate);
    DalOrder.Update(tmpOrder);
}

void DeleteOrder()
{
    Console.WriteLine("\nEnter the ID of the order you want to delete: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    DalOrder.Delete(oId);
}

void WatchAllItemsInOrd()
{
    Console.WriteLine("Enter the order ID you want to watch: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    OrderItem[] itemsInOrder = DalOrderItem.ReadOrderItemByOrderId(oId);
    foreach (OrderItem oItem in itemsInOrder)
    {
        Console.WriteLine(oItem);
    }
}

//==================================== Product functions
void AddProduct()
{
    string name;
    int category;
    int inStock;
    double price;

    Console.WriteLine("Enter the product details:\n name-");
    name = Console.ReadLine();
    Console.WriteLine("\ncategory- ");
    category = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\nprice- ");
    price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("\ninStock- ");
    inStock = Convert.ToInt32(Console.ReadLine());

    bool notExists;
    int id;
    do      //Rand id and make sure is unique in the array 
    {
        notExists = true;
        Random rnd = new Random();
        id = rnd.Next(100000, 1000000);
        for (int j = 0; j < DataSource.ProductList.Count; j++)
        {
            if (DataSource.ProductList[j].Id == id)
            {
                notExists = false;
                break;
            }
        }
    } while (!notExists);
    Product newProduct = new Product(id, name, price, inStock, (eCategory)category);
    DalProduct.CreateProduct(newProduct);
}

void UpdateProduct()
{
    string name;
    int id, category, inStock;
    double price;

    Console.WriteLine("\nEnter the product's details you want to update:\n id- ");
    id = Convert.ToInt32(Console.ReadLine());
    Product tmpProd = DalProduct.ReadProduct(id);
    Console.WriteLine(tmpProd + "\n");
    Console.WriteLine("\nname- ");
    name = Console.ReadLine();
    Console.WriteLine("\ncategory- ");
    category = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\ninStock- ");
    inStock = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\nprice- ");
    price = Convert.ToDouble(Console.ReadLine());
    Product tmpProduct = new Product(id, name, price, inStock, (eCategory)category);
    DalProduct.UpdateProduct(tmpProduct);
}

void WatchProduct()
{
    Console.WriteLine("\nEnter the product ID for watching: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Product tmpProduct = DalProduct.ReadProduct(id);
    Console.WriteLine(tmpProduct + "\n");
}

void WatchProductList()
{
    int size = DataSource.ProductList.Count;
    Product[] productList = new Product[size];
    productList = DalProduct.ReadProduct();
    foreach (Product product in productList)
    {
        Console.WriteLine(product);
    }
}

void DeleteProduct()
{
    Console.WriteLine("\nEnter the ID of the product you want to delete: ");
    int id = Convert.ToInt32(Console.ReadLine());
    DalProduct.DeleteProduct(id);
}


//=============================================OrderItem functions
void AddOrderItem()
{
    int id, productId, orderId, amount, productIdxInList = -1;
    double price;
    bool correct;
    Console.WriteLine("Enter OrderItem details:");
    do
    {
        correct = false;
        Console.WriteLine("productId-");
        productId = Convert.ToInt32(Console.ReadLine());
        for (int j = 0; j < DataSource.ProductList.Count; j++)//checking that this productId exists 
        {
            if (DataSource.ProductList[j].Id == productId)
            {
                correct = true;
                productIdxInList = j;
            }
        }
        if (!correct)
            Console.WriteLine("this productId doesn't exist");
    } while (!correct);
    do
    {
        correct = false;
        Console.WriteLine("orderId-");
        orderId = Convert.ToInt32(Console.ReadLine());
        for (int j = 0; j < DataSource.OrderList.Count; j++)//checking that this orderId exists 
        {
            if (DataSource.OrderList[j].Id == orderId)
            {
                correct = true;
            }
        }
        if (!correct)
            Console.WriteLine("this orderId doesn't exist");
    } while (!correct);
    Console.WriteLine("amount-");
    amount = Convert.ToInt32(Console.ReadLine());
    price = (DataSource.ProductList[productIdxInList].Price) * amount;
    id = DataSource.Config.MaxOrderItemId;
    OrderItem newOrderItem = new OrderItem(id, productId, orderId, price, amount);
    DalOrderItem.CreateOrderItem(newOrderItem);
}

void UpdateOrderItem()
{
    int id, productId, orderId, amount, productIdxInList = -1;
    double price;
    bool correct;
    Console.WriteLine("\nEnter the orderItem's details you want to update:\n id- ");
    id = Convert.ToInt32(Console.ReadLine());
    OrderItem tmpOrdItem = DalOrderItem.ReadOrderItem(id);
    Console.WriteLine(tmpOrdItem + "\n");
    do
    {
        correct = false;
        Console.WriteLine("productId-");
        productId = Convert.ToInt32(Console.ReadLine());
        for (int j = 0; j < DataSource.ProductList.Count; j++)//checking that this productId exists 
        {
            if (DataSource.ProductList[j].Id == productId)
            {
                correct = true;
                productIdxInList = j;
            }
        }
        if (!correct)
            Console.WriteLine("this productId doesn't exist");
    } while (!correct);
    do
    {
        correct = false;
        Console.WriteLine("orderId-");
        orderId = Convert.ToInt32(Console.ReadLine());
        for (int j = 0; j < DataSource.OrderList.Count; j++)//checking that this orderId exists 
        {
            if (DataSource.OrderList[j].Id == orderId)
            {
                correct = true;
            }
        }
        if (!correct)
            Console.WriteLine("this orderId doesn't exist");
    } while (!correct);
    Console.WriteLine("amount-");
    amount = Convert.ToInt32(Console.ReadLine());
    price = (DataSource.ProductList[productIdxInList].Price) * amount;
    OrderItem newOrderItem = new OrderItem(id, productId, orderId, price, amount);
    DalOrderItem.UpdateOrderItem(newOrderItem);
}

void WatchOrderItem()
{
    Console.WriteLine("\nEnter the orderItem ID for watching: ");
    int id = Convert.ToInt32(Console.ReadLine());
    OrderItem tmpOrderItem = DalOrderItem.ReadOrderItem(id);
    Console.WriteLine(tmpOrderItem + "\n");
}

void WatchOrderItemList()
{
    int size = DataSource.OrderItemList.Count;
    OrderItem[] orderItemList = new OrderItem[size];
    orderItemList = DalOrderItem.ReadOrderItem();
    foreach (OrderItem orderItem in orderItemList)
    {
        Console.WriteLine(orderItem);
    }
}

void DeleteOrderItem()
{
    Console.WriteLine("\nEnter the ID of the orderItem you want to delete: ");
    int id = Convert.ToInt32(Console.ReadLine());
    DalOrderItem.DeleteOrderItem(id);
}

void WatchOrderItemByOrderIdProductId()
{
    Console.WriteLine("\nEnter the order ID : ");
    int oId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\nEnter the product ID : ");
    int pId = Convert.ToInt32(Console.ReadLine());
    OrderItem tmpOrderItem = DalOrderItem.ReadOrderItem(pId, oId);
    Console.WriteLine(tmpOrderItem + "\n");
}


//==================================== Menue
void Menue(string type)
{
    string specialOptions = ".";
    if (type == "orderItem")
    {
        specialOptions = $@"f for watching all items in a specific order 
            g for watching a specific item of a specific order.";
    }
    if (type == "order")
    {
        specialOptions = $@"f for watching all the items in the order
            g for watching a specific item of the order." ;
    }
    Console.WriteLine($@"
            Choose the following action:
            a for adding an {type},
            b for watching an {type},
            c for watching the {type}s list,
            d for updating an {type},
            e for deleting an {type} from the {type}s list
            {specialOptions}
            ");

    choice = Console.ReadKey().KeyChar;
    if (type == "order")
    {
        switch (choice)
        {
            case 'a':   // add
                AddOrder();
                break;
            case 'b':
                WatchOrder();
                break;
            case 'c':
                WatchOrderList();
                break;
            case 'd':
                UpdateOrder();
                break;
            case 'e':
                DeleteOrder();
                break;
            case 'f':
                WatchAllItemsInOrd();
                break;
            case 'g':
                WatchOrderItemByOrderIdProductId();
                break;
            default:
                throw new Exception("unknown action");
                break;
        }
    }
    if (type == "product")
    {
        switch (choice)
        {
            case 'a':   // add
                AddProduct();
                break;
            case 'b':
                WatchProduct();
                break;
            case 'c':
                WatchProductList();
                break;
            case 'd':
                UpdateProduct();
                break;
            case 'e':
                DeleteProduct();
                break;
            default:
                throw new Exception("unknown action");
                break;

        }
    }
    if (type == "orderItem")
    {
        switch (choice)
        {
            case 'a':   // add
                AddOrderItem();
                break;
            case 'b':
                WatchOrderItem();
                break;
            case 'c':
                WatchOrderItemList();
                break;
            case 'd':
                UpdateOrderItem();
                break;
            case 'e':
                DeleteOrderItem();
                break;
            case 'f':
                WatchAllItemsInOrd();
                break;
            case 'g':
                WatchOrderItemByOrderIdProductId();
                break;
            default:
                throw new Exception("unknown action");
                break;
        }
    }
}

void main()
{
    bool toContinue = true;
    while (toContinue)
    {
        try
        {
            Console.WriteLine(" Press 0 to exit,\n" +
            " 1 to check the orders list,\n " +
            " 2 to check the products list,\n" +
            " 3 to check the orders items list. ");
            choice = Console.ReadKey().KeyChar;
            switch (choice)
            {
                case '0':
                    toContinue = false;
                    break;
                case '1':
                    Menue("order");
                    break;
                case '2':
                    Menue("product");
                    break;
                case '3':
                    Menue("orderItem");
                    break;
            }
        }
        catch (Exception errMsg)
        {
            Console.WriteLine(errMsg.Message + "\n");
        }
    }
}

main();