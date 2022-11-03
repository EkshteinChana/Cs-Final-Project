/// <summary>
/// Stage 1 by Ozer Ester 214255705 and Ekshtein Chana 213868631
/// </summary>
/// 

using Dal;
using DO;
char choice;

//=========================== Order functions
void addOrder()
{
    string oCustomerName, oCustomerEmail, oCustomerAddress;
    int numDays, oId;
    DateTime oDate, shipDate, deliveryDate;
    TimeSpan daysUntillShip, daysUntilDelivery;
    Random rnd = new Random();

    Console.WriteLine("\nEnter your details:\n name-");
    oCustomerName = Console.ReadLine();
    Console.WriteLine("email-");
    oCustomerEmail = Console.ReadLine();
    Console.WriteLine("address-");
    oCustomerAddress = Console.ReadLine();

    oId = DataSource.Config.MaxOrderId;
    oDate = DateTime.Now;
    numDays = rnd.Next(1, 5);
    daysUntillShip = TimeSpan.FromDays(numDays);
    shipDate = oDate + daysUntillShip;
    numDays = rnd.Next(3, 8);
    daysUntilDelivery = TimeSpan.FromDays(numDays);
    deliveryDate = shipDate + daysUntilDelivery;
    Order newOrder = new Order(oId, oCustomerName, oCustomerEmail, oCustomerAddress, oDate, shipDate, deliveryDate);
    DalOrder.CreateOrder(newOrder);
}

void watchOrder()
{
    Console.WriteLine("\nEnter the order ID for watching: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    Order tmpOrder = DalOrder.ReadOrder(oId);
    Console.WriteLine(tmpOrder+"\n");
}

void watchOrderList()
{
   
    DalOrder.ReadOrder();
}

void updateOrder()
{
    Console.WriteLine("\nEnter the order id you want to update: ");

}

//==================================== Product functions
void addProduct()
{
    string name;
    int category ;
    int inStock;
    double price;

    Console.WriteLine("Enter the product details:\n name-");
    name = Console.ReadLine();
    Console.WriteLine("category-");
    category = Convert.ToInt32(Console.ReadLine();
    Console.WriteLine("price-");
    price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("inStock-");
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
    Product newProduct = new Product(id, name, price, inStock, category);
    DalProduct.CreateProduct(newProduct);
}

//==================================== Menue
void menue(string type)
{
    string specialOptions = ".";
    if (type == "order item") { specialOptions = "f for display all the items of existing orders. "; };
    if (type == "order")
    {
        specialOptions = "f for display all the items in the order " +
                         "g for display a specific item of the order.";
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

            }
        }
        catch( Exception errMsg)
        {
            Console.WriteLine(errMsg.Message+"\n");
        }
    
    }
}

main();