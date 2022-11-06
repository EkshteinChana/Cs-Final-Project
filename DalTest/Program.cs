/// <summary>
/// Stage 1 by Ozer Ester 214255705 and Ekshtein Chana 213868631
/// </summary>
/// 

using Dal;
using DO;
using System.Xml.Linq;

char choice;
DataSource ds=new DataSource();

//=========================== Order functions
void addOrder()
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
    DalOrder.CreateOrder(newOrder);
}

void watchOrder()
{
    Console.WriteLine("\nEnter the order ID for watching: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    Order tmpOrder = DalOrder.ReadOrder(oId);
    Console.WriteLine(tmpOrder + "\n");
}

void watchOrderList()
{
    int size = DataSource.Config.orderArrIdx;
    Order[] ordList = new Order[size];
    ordList = DalOrder.ReadOrder();
    foreach (Order order in ordList)
    {
        Console.WriteLine(order);
    }
}

void updateOrder()
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
    oDate = tmpOrd.orderDate;
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
    DalOrder.UpdateOrder(tmpOrder);
}

void deleteOrder()
{
    Console.WriteLine("\nEnter the ID of the order you want to delete: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    DalOrder.DeleteOrder(oId);
}

void watchAllItemsInOrd()
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
void addProduct()
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
        for (int j = 0; j < DataSource.Config.productArrIdx; j++)
        {
            if (DataSource.productArr[j].id == id)
            {
                notExists = false;
                break;
            }
        }
    } while (!notExists);
    Product newProduct = new Product(id, name, price, inStock, (eCategory)category);
    DalProduct.CreateProduct(newProduct);
}

void updateProduct()
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

void watchProduct()
{
    Console.WriteLine("\nEnter the product ID for watching: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Product tmpProduct = DalProduct.ReadProduct(id);
    Console.WriteLine(tmpProduct + "\n");
}

void watchProductList()
{
    int size = DataSource.Config.productArrIdx;
    Product[] productList = new Product[size];
    productList = DalProduct.ReadProduct();
    foreach (Product product in productList)
    {
        Console.WriteLine(product);
    }
}

void deleteProduct()
{
    Console.WriteLine("\nEnter the ID of the product you want to delete: ");
    int id = Convert.ToInt32(Console.ReadLine());
    DalProduct.DeleteProduct(id);
}


//=============================================OrderItem functions
void addOrderItem()
{
    int id, productId, orderId, amount, productIdxInArr = -1;
    double price;
    bool correct;
    Console.WriteLine("Enter OrderItem details:");
    do
    {
        correct = false;
        Console.WriteLine("productId-");
        productId = Convert.ToInt32(Console.ReadLine());
        for (int j = 0; j < DataSource.Config.productArrIdx; j++)//checking that this productId exists 
        {
            if (DataSource.productArr[j].id == productId)
            {
                correct = true;
                productIdxInArr = j;
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
        for (int j = 0; j < DataSource.Config.orderArrIdx; j++)//checking that this orderId exists 
        {
            if (DataSource.orderArr[j].id == orderId)
            {
                correct = true;
            }
        }
        if (!correct)
            Console.WriteLine("this orderId doesn't exist");
    } while (!correct);
    Console.WriteLine("amount-");
    amount = Convert.ToInt32(Console.ReadLine());
    price = (DataSource.productArr[productIdxInArr].price) * amount;
    id = DataSource.Config.MaxOrderItemId;
    OrderItem newOrderItem = new OrderItem(id, productId, orderId, price, amount);
    DalOrderItem.CreateOrderItem(newOrderItem);
}

void updateOrderItem()
{
    int id, productId, orderId, amount, productIdxInArr = -1;
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
        for (int j = 0; j < DataSource.Config.productArrIdx; j++)//checking that this productId exists 
        {
            if (DataSource.productArr[j].id == productId)
            {
                correct = true;
                productIdxInArr = j;
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
        for (int j = 0; j < DataSource.Config.orderArrIdx; j++)//checking that this orderId exists 
        {
            if (DataSource.orderArr[j].id == orderId)
            {
                correct = true;
            }
        }
        if (!correct)
            Console.WriteLine("this orderId doesn't exist");
    } while (!correct);
    Console.WriteLine("amount-");
    amount = Convert.ToInt32(Console.ReadLine());
    price = (DataSource.productArr[productIdxInArr].price) * amount;
    OrderItem newOrderItem = new OrderItem(id, productId, orderId, price, amount);
    DalOrderItem.UpdateOrderItem(newOrderItem);
}

void watchOrderItem()
{
    Console.WriteLine("\nEnter the orderItem ID for watching: ");
    int id = Convert.ToInt32(Console.ReadLine());
    OrderItem tmpOrderItem = DalOrderItem.ReadOrderItem(id);
    Console.WriteLine(tmpOrderItem + "\n");
}

void watchOrderItemList()
{
    int size = DataSource.Config.orderItemArrIdx;
    OrderItem[] orderItemList = new OrderItem[size];
    orderItemList = DalOrderItem.ReadOrderItem();
    foreach (OrderItem orderItem in orderItemList)
    {
        Console.WriteLine(orderItem);
    }
}

void deleteOrderItem()
{
    Console.WriteLine("\nEnter the ID of the orderItem you want to delete: ");
    int id = Convert.ToInt32(Console.ReadLine());
    DalOrderItem.DeleteOrderItem(id);
}

void watchOrderItemByOrderIdProductId()
{
    Console.WriteLine("\nEnter the order ID : ");
    int oId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\nEnter the product ID : ");
    int pId = Convert.ToInt32(Console.ReadLine());
    OrderItem tmpOrderItem = DalOrderItem.ReadOrderItem(pId, oId);
    Console.WriteLine(tmpOrderItem + "\n");
}


//==================================== Menue
void menue(string type)
{
    string specialOptions = ".";
    if (type == "orderItem")
    {
        specialOptions = "f for watching a specific item of a specific order.";
    }
    if (type == "order")
    {
        specialOptions = "f for watching all the items in the order \n g for watching a specific item of the order." ;
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
                addOrder();
                break;
            case 'b':
                watchOrder();
                break;
            case 'c':
                watchOrderList();
                break;
            case 'd':
                updateOrder();
                break;
            case 'e':
                deleteOrder();
                break;
            case 'f':
                watchAllItemsInOrd();
                break;
            case 'g':
                watchOrderItemByOrderIdProductId();
                break;
        }
    }
    if (type == "product")
    {
        switch (choice)
        {
            case 'a':   // add
                addProduct();
                break;
            case 'b':
                watchProduct();
                break;
            case 'c':
                watchProductList();
                break;
            case 'd':
                updateProduct();
                break;
            case 'e':
                deleteProduct();
                break;
            case 'f':
                break;
            case 'g':
                break;
        }
    }
    if (type == "orderItem")
    {
        switch (choice)
        {
            case 'a':   // add
                addOrderItem();
                break;
            case 'b':
                watchOrderItem();
                break;
            case 'c':
                watchOrderItemList();
                break;
            case 'd':
                updateOrderItem();
                break;
            case 'e':
                deleteOrderItem();
                break;
            case 'f':
                watchOrderItemByOrderIdProductId();
                break;
            case 'g':
                break;
        }
    }
}

void main()
{

    //DataSource.s_intilize();
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
                    menue("order");
                    break;
                case '2':
                    menue("product");
                    break;
                case '3':
                    menue("orderItem");
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