
using BO;
namespace BlApi;
/// <summary>
/// Interfaces for actions regarding to main logical entity-Product
/// </summary>
public interface IProduct
{
    /// <summary>
    /// A function to read the list of products for manager screen
    /// </summary>
    /// <returns>An IEnumerable of ProductForList</returns>
    public IEnumerable<ProductForList> ReadProdsManager();
    /// <summary>
    /// A function to read the list of products (catalog)
    /// for customer screen
    /// </summary>
    /// <returns>An IEnumerable of ProductItem</returns>
    public IEnumerable<ProductItem> ReadProdsCustomer();
    // <summary>
    /// A function to read details of a product by productId
    /// for manager screen
    /// </summary>
    /// <returns>Product</returns>
    public Product ReadProdManager(int Id);
    // <summary>
    /// A function to read details of a product by productId
    /// for customer screen
    /// </summary>
    /// <returns>Product</returns>
    public Product ReadProdCustomer(int Id);
    // <summary>
    /// A function to add a product (for manager screen)
    /// </summary>
    /// <returns>void</returns>
    public int CreateProd(Product prod);
    // <summary>
    /// A function to delete a product (for manager screen)
    /// </summary>
    /// <returns>int(ID of the added product) </returns>
    public void DeleteProd(int Id);
    // <summary>
    /// A function to update a Product (for manager screen)
    /// </summary>
    /// <returns>void</returns>
    public void UpdateProd(Product prod);
}
