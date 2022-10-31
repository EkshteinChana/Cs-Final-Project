
namespace DO;
    public struct Product
    {
    public readonly int id { get; }
    public string name { get; set; }
    public double price { get; set; }
    public int inStock { get; set; }
    public eCategory category { get; set; }
    public override string ToString() => $@"
        product: {name} 
        ID: {id}
        price: {price}
        category: {category}
    	amount in stock: {inStock}
        ";

    public Product(int _id, string _name, double _price , int _inStock, eCategory _category)
    {
        id = _id;
        name = _name;
        price = _price;
        inStock = _inStock;
        category = _category;
    }

}
