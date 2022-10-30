
namespace DO;
    public struct Product
    {
    private readonly int id { get; }
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

}
