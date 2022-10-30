
namespace DO;
    public struct OrderItem
    {
        public int productId { get; }
        public int orderId { get; }
        public float price { get; set; }
        public int amount { get; set; }
        public OrderItem(int _productId , int _orderId ,float _price , int _amount)
        {
            productId = _productId;
            orderId = _orderId;
            price = _price;
            amount = _amount;
        }
    public override string ToString() => $@"
            product ID: {productId}
            order ID: {orderId}
            price: {price}
            amount: {amount} ";
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