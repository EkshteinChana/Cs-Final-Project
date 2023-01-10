
using BO;
namespace BlApi;
/// <summary>
/// Interfaces for actions regarding to main logical entity-Product
/// </summary>
public interface IProduct
{

    /// <summary>
    /// A function to read the list of products
    /// </summary>
    /// <returns>An IEnumerable of ProductForList</returns>
    public IEnumerable<ProductForList?> ReadProdsList(eCategory? ctgry=null);

    ///// <summary>
    ///// A function to read a list of products by specific category
    ///// </summary>
    ///// <returns>An IEnumerable of ProductForList</returns>
    //public IEnumerable<ProductForList?> ReadProdsByCategory(eCategory? ctgry);

    /// <summary>
    /// A function to read details of a product by productId
    /// for manager screen
    /// </summary>
    /// <returns>Product</returns>
    public Product ReadProdManager(int Id);
    
    /// <summary>
    /// A function to read details of a product by productId
    /// for customer screen
    /// </summary>
    /// <returns>ProductItem</returns>
    public ProductItem ReadProdCustomer(int Id,Cart cart);
    
    /// <summary>
    /// A function to add a product (for manager screen)
    /// </summary>
    /// <returns>void</returns>
    public int CreateProd(Product prod);
    
    /// <summary>
    /// A function to delete a product (for manager screen)
    /// </summary>
    /// <returns>int(ID of the added product) </returns>
    public void DeleteProd(int Id);
    
    /// <summary>
    /// A function to update a Product (for manager screen)
    /// </summary>
    /// <returns>void</returns>
    public void UpdateProd(Product prod);
}
