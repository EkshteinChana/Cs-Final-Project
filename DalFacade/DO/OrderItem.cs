
namespace DO;
    public struct OrderItem
    {
        public int ProductId { get; }
        public int OrderId { get; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public OrderItem(int productId , int orderId ,float price , int amount)
        {
            ProductId = productId;
            OrderId = orderId;
            Price = price;
            Amount = amount;
        }
    public override string ToString() => $@"
            product ID: {ProductId}
            order ID: {OrderId}
            price: {Price}
            amount: {Amount} ";
     }



//public int ID
//{
//    get { return id; }
//}

//public string ProductName { get; set; }
//public eCategory Category { get; set; }

//public Product(int Id)
//{
//    this.id = Id; // רק כאן אפשר לאתחל את המשתנה
//}