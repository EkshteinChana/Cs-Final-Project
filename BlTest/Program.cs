
///// <summary>
///// C# project Stage 2 
///// by Ozer Ester 214255705 and Ekshtein Chana 213868631
///// </summary>

using BO;
using BlImplementation;
using BlTest;

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
    Console.WriteLine(ord);
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

/// <summary>
/// A function for adding a product to an order.
/// </summary>
void AddProdToOrder()
{
    Console.WriteLine("\nEnter the order ID: ");
    int ordId = Console.Read();
    Console.WriteLine("\nEnter the product ID: ");
    int pId = Console.Read();
    Console.WriteLine("\nEnter the amount: ");
    int amount = Console.Read();
    Order ord = bl.Order.UpdateOrd(ordId, pId, amount, eUpdateOrder.add);
    Console.WriteLine("The update order:\n" + ord + "\n");
}

/// <summary>
/// A function for deleting a product from an order.
/// </summary>
void DeleteProdFromOrder()
{
    Console.WriteLine("\nEnter the order ID: ");
    int ordId = Console.Read();
    Console.WriteLine("\nEnter the product ID: ");
    int pId = Console.Read();
    Order ord = bl.Order.UpdateOrd(ordId, pId, 0, eUpdateOrder.delete);
    Console.WriteLine("The update order:\n" + ord + "\n");
}

/// <summary>
/// A function for updating the amount of a product in an order.
/// </summary>
void UpdateAmountProdInOrder()
{
    Console.WriteLine("\nEnter the order ID: ");
    int ordId = Console.Read();
    Console.WriteLine("\nEnter the product ID: ");
    int pId = Console.Read();
    Console.WriteLine("\nEnter the new amount: ");
    int amount = Console.Read();
    Order ord = bl.Order.UpdateOrd(ordId, pId, amount, eUpdateOrder.changeAmount);
    Console.WriteLine("The update order:\n" + ord + "\n");
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
/// A function that receives details from the user(manager) about the new product and sends them to the function for adding a product (in the logical layer) .
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
/// A function that receives details from the user(manager) for updating a product,
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
/// A function that receives product ID from the user for adding a product to the cart,
/// and sends it to the function that will do it (in the logical layer).
/// </summary>
void AddProductToCart()
{
    Console.WriteLine("Enter the product ID you want to add to the cart:");
    int Id = Console.Read();
    cart = bl.Cart.CreateProdInCart(cart, Id);
}


/// <summary> 
/// A function that receives product ID and amount from the user for updating the amount of a product in the cart,
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
    Dictionary<char, Func> Actions = new Dictionary<char, Func>();
    string Options = ".";
    if (type == "product")
    {
        Options = $@"a for watching a product for the manager screen\n
                    b for watching a product for the customer screen\n
                    c for watching the products list\n
                    d for adding a product\n
                    e for updating a product\n
                    f for deleting a product\n";
        Actions = new Dictionary<char, Func>(){
            {'a', WatchProductManager },
            {'b', WatchProductCustomer},
            {'c', WatchProductList},
            {'d', AddProduct},
            {'e', UpdateProduct},
            {'f', DeleteProduct} };

    }
    if (type == "cart")
    {
        Options = $@"a for adding a product to the cart\n
                    b for updating the amount of a product in the cart\n
                    c for approval of a shopping cart for an order\n";
        Actions = new Dictionary<char, Func>(){
            {'a', AddProductToCart },
            {'b', UpdateProdAmontInCart},
            {'c', MakeOrder} };
    }
    if (type == "order")
    {
        Options = $@"a for watching a specific order\n
                    b the orders list\n
                    c for updating the shipping date\n
                    d for updating the delivery date\n
                    e for adding a product to the order\n
                    f for deleting a product from the order\n
                    g for updating the amount of a product in the order\n";
        Actions = new Dictionary<char, Func>(){
            {'a', WatchOrder },
            {'b', WatchOrderList},
            {'c', UpdateShippingDate},
            {'d', UpdateDeliveryDate},
            {'e', AddProdToOrder},
            {'f', DeleteProdFromOrder},
            {'g',UpdateAmountProdInOrder} };
    }
    Console.WriteLine($@"{Options}");
    choice = Console.ReadKey().KeyChar;
    var res = Actions[choice];
    res();
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
        catch (BlApi.DataError err)
        {
            Console.WriteLine(err.Message + " " + err.InnerException.Message +"\n");
        }
        catch (BlApi.InvalidValue err)
        {
            Console.WriteLine(err.Message + "\n");
        }
        catch (BlApi.OutOfStock err)
        {
            Console.WriteLine(err.Message + "\n");
        }
        catch (BlApi.ItemNotExist err)
        {
            Console.WriteLine(err.Message + "\n");
        }
        catch (BlApi.IllegalAction err)
        {
            Console.WriteLine(err.Message + "\n");
        }
    }
}

main();