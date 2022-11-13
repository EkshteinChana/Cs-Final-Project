
namespace DO;
    public struct Product
    {
    public readonly int Id { get; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    public eCategory category { get; set; }
    public override string ToString() => $@"
        product: {Name} 
        ID: {Id}
        price: {Price}
        category: {category}
    	amount in stock: {InStock}
        ";

    public Product(int _id, string _name, double _price , int _inStock, eCategory _category)
    {
        Id = _id;
        Name = _name;
        Price = _price;
        InStock = _inStock;
        category = _category;
    }

}
