namespace PL.PO;
/// <summary>
/// A PO product item entity for the catalog screen of the customer
/// </summary>
public class ProductItem { 
    public int Id { get; set; }//product id
    public string? Name { get; set; }
    public double Price { get; set; }//productPrice
    public BO.eCategory? Category { get; set; }
    public int Amount { get; set; }//The quantity of this product in the customer's cart
    public bool InStock { get; set; }//Availability (whether in stock)
    public override string ToString() => $@"
        ID: {Id}
        name: {Name} 
        price: {Price}
        category: {Category}
        amount: {Amount}
        InStock: {InStock}
        ";
}









