
namespace DO;
    public struct Product
    {
    public int Id { get; set; }
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
}
