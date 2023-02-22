
using BO;
using System.Runtime.CompilerServices;

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
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public IEnumerable<ProductForList?> ReadProdsList(eCategory? ctgry=null);

    /// <summary>
    /// A function to read details of a product by productId
    /// for manager screen
    /// </summary>
    /// <returns>Product</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public Product ReadProdManager(int Id);

    /// <summary>
    /// A function to read details of a product by productId
    /// for customer screen
    /// </summary>
    /// <returns>ProductItem</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public ProductItem ReadProdCustomer(int Id,Cart cart);

    /// <summary>
    /// A function to add a product (for manager screen)
    /// </summary>
    /// <returns>void</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public int CreateProd(Product prod);

    /// <summary>
    /// A function to delete a product (for manager screen)
    /// </summary>
    /// <returns>int(ID of the added product) </returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public void DeleteProd(int Id);

    /// <summary>
    /// A function to update a Product (for manager screen)
    /// </summary>
    /// <returns>void</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public void UpdateProd(Product prod);
}
