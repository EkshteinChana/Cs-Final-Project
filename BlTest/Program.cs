
///// <summary>
///// C# project Stage 2 
///// by Ozer Ester 214255705 and Ekshtein Chana 213868631
///// </summary>

using BO;
using BlImplementation;


//=========================== Generic variables
char choice;
Bl bl = new Bl();
Cart cart = new();
//=========================== Order functions

/// <summary>
/// A function that receives details from the user for displaying order data, and sends them to the function to do so(in the logical layer).
/// </summary>
void WatchOrder()
{
    Console.WriteLine("\nEnter the order ID for watching: ");
    int oId = Convert.ToInt32(Console.ReadLine());
    BO.Order ord = bl.Order.ReadOrd(oId);
    Console.WriteLine(ord + "\n");
}
/// <summary>
/// A function that receives details from the user(manager) for displaying the data of all orders, and sends them to the function that will do it (in the logical layer).
/// </summary>
void WatchOrderList()
{
    IEnumerable<OrderForList> ordList = bl.Order.ReadOrdsManager();
    foreach (OrderForList order in ordList)
    {
        Console.WriteLine(order);
    }
}
/// <summary>
/// A function for updating the shipping date.
/// </summary>
void UpdateShippingDate()
{
    Console.WriteLine("\nEnter the order ID: ");
    int ordId = Console.Read();
    Order ord = bl.Order.UpdateOrdShipping(ordId);
    Console.WriteLine(ord + "\n");
}
/// <summary>
/// A function for updating the delivery date.
/// </summary>
void UpdateDeliveryDate()
{
    Console.WriteLine("\nEnter the order ID: ");
    int ordId = Console.Read();
    Order ord = bl.Order.UpdateOrdDelivery(ordId);
    Console.WriteLine(ord + "\n");
}


//==================================== Product functions

/// <summary>
/// A function that receives details from the user(manager) for displaying product data,
/// and sends them to the function to do so (in the logical layer).
/// </summary>
void WatchProductManager()
{
    Console.WriteLine("\nEnter the product ID for watching: ");
    int id = Console.Read();
    Product tmpProduct = bl.Product.ReadProdManager(id);
    Console.WriteLine(tmpProduct + "\n");
}

/// <summary>
/// A function that receives details from the user(customer) for displaying product data,
/// and sends them to the function to do so (in the logical layer).
/// </summary>
void WatchProductCustomer()
{
    Console.WriteLine("\nEnter the product ID for watching: ");
    int id = Console.Read();
    /////////////////??????
    Cart cart = new();
    /////////////////??????
    ProductItem productItem = bl.Product.ReadProdCustomer(id, cart);
    Console.WriteLine(productItem + "\n");
}

/// <summary>
/// A function that receives details from the user for displaying the data of all products,
/// and sends them to the function that will do it (in the logical layer).
/// </summary>
void WatchProductList()
{
    IEnumerable<ProductForList> productList = bl.Product.ReadProdsList();
    foreach (ProductForList p in productList)
    {
        Console.WriteLine(p);
    }
}

/// <summary>
/// A function that receives details from the user(manager) about the new product and sends them to the function for adding an product (in the logical layer) .
/// </summary>
void AddProduct()
{
    Product newProduct = new Product();
    Console.WriteLine("Enter the product details:\n name-");
    newProduct.Name = Console.ReadLine();
    Console.WriteLine("\ncategory- ");
    newProduct.Category = (eCategory)Console.Read();
    Console.WriteLine("\nprice- ");
    newProduct.Price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("\ninStock- ");
    newProduct.InStock = Console.Read();
    int ID = bl.Product.CreateProd(newProduct);
    Console.WriteLine($"The ID of the added product is: {ID}");
}

/// <summary>
/// A function that receives details from the user(manager) for updating an product,
/// and sends them to the function that will do it (in the logical layer).
/// </summary>.
void UpdateProduct()
{
    Product tmpProduct = new Product();
    Console.WriteLine("\nEnter the product's details you want to update:\n id- ");
    tmpProduct.Id = Console.Read();
    Product srcProd = bl.Product.ReadProdManager(tmpProduct.Id);
    Console.WriteLine(srcProd + "\n");
    Console.WriteLine("\nname- ");
    tmpProduct.Name = Console.ReadLine();
    Console.WriteLine("\ncategory- ");
    tmpProduct.Category = (eCategory)Console.Read();
    Console.WriteLine("\ninStock- ");
    tmpProduct.InStock = Console.Read();
    Console.WriteLine("\nprice- ");
    tmpProduct.Price = Convert.ToDouble(Console.ReadLine());
    bl.Product.UpdateProd(tmpProduct);
}

/// <summary>
/// A function that receives details from the user for deleting a product,
/// and sends them to the function that will do it (in the logical layer).
/// </summary>
void DeleteProduct()
{
    Console.WriteLine("\nEnter the ID of the product you want to delete: ");
    int id = Console.Read();
    bl.Product.DeleteProd(id);
}


//==================================== Cart functions

/// <summary> 
/// A function that receives product ID from the user for adding an product to the cart,
/// and sends it to the function that will do it (in the logical layer).
/// </summary>
void AddProductToCart()
{
    Console.WriteLine("Enter the product ID you want to add to the cart:");
    int Id = Console.Read();
    cart = bl.Cart.CreateProdInCart(cart, Id);
}


/// <summary> 
/// A function that receives product ID and amount from the user for updating the amount of an product in the cart,
/// and sends them to the function that will do it (in the logical layer).
/// </summary>
void UpdateProdAmontInCart()
{
    Console.WriteLine("Enter the product ID you want to update its amount in the cart:");
    int id = Console.Read();
    Console.WriteLine("Enter the new amount");
    int amount = Console.Read();
    cart = bl.Cart.UpdateAmountOfProd(cart, id, amount);
}

/// <summary> 
/// A function that receives customer's details for making an order,
/// and sends them to the function that will do it (in the logical layer).
/// </summary>
void MakeOrder()
{
    Console.WriteLine("\nEnter your details:\n name-");
    string customerName = Console.ReadLine();
    Console.WriteLine("\nemail-");
    string customerEmail = Console.ReadLine();
    Console.WriteLine("\naddress-");
    string customerAddress = Console.ReadLine();
    bl.Cart.MakeOrder(cart, customerName, customerEmail, customerAddress);
}


//==================================== Main

/// <summary>
/// A function that presents the user with a menu of actions for the selected entity, receives a action by  
/// input and routes it to the appropriate function.
/// </summary>
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
            g for watching a specific item of the order.";
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
            " 1 to check the product functions,\n " +
            " 2 to check the cart functions,\n" +
            " 3 to check the order functions. ");
            choice = Console.ReadKey().KeyChar;
            switch (choice)
            {
                case '0':
                    toContinue = false;
                    break;
                case '1':
                    Menue("product");
                    break;
                case '2':
                    Menue("cart");
                    break;
                case '3':
                    Menue("order");
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