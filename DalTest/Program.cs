/// <summary>
/// Stage 1 by Ozer Ester 214255705 and Ekshtein Chana 213868631
/// </summary>
/// 

using Dal;
using DO;
char choice;

//Order functions
void addOrder()
{
    string oCustomerName, oCustomerEmail, oCustomerAddress;
    int numDays, oId;
    DateTime oDate, shipDate, deliveryDate;
    TimeSpan daysUntillShip, daysUntilDelivery;

    Random rnd = new Random();
    Console.WriteLine("Enter your details:\n name-");
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
    //id, string _customerName, string _customerEmail, string _customerAddress, DateTime _orderDate,DateTime _shipDate, DateTime _deliveryDate)
    Order tmpOrder = new Order(oId, oCustomerName, oCustomerEmail, oCustomerAddress, oDate, );

}
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
            b for display an {type},
            c for display the {type}s list,
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

        }
    }
}

void main()
{
    bool toContinue = true;
    while (toContinue)
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
}

main();