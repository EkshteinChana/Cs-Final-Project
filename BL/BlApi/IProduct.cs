using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;
/// <summary>
/// Interfaces for actions regarding to main logical entity-Product
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Product list request for manager screen
    /// </summary>
    /// <returns>An IEnumerable of ProductForList</returns>
    public IEnumerable<ProductForList> ReadProdsManager();
    /// <summary>
    /// Catalog request for Customer screen
    /// </summary>
    /// <returns>An IEnumerable of ProductItem</returns>
    public IEnumerable<ProductItem> ReadProdsCustomer();
    // <summary>
    /// Product details request by productId
    /// for manager screen
    /// </summary>
    /// <returns>Product</returns>
    public Product ReadProdManager(int Id);
    // <summary>
    /// Product details request by productId
    /// for customer screen
    /// </summary>
    /// <returns>Product</returns>
    public Product ReadProdCustomer(int Id);
    // <summary>
    /// Adding Product (for customer screen)
    /// </summary>
    /// <returns>void</returns>
    public void CreateProd(Product prod);
    // <summary>
    /// Deleting Product (for customer screen)
    /// </summary>
    /// <returns>void</returns>
    public void DeleteProd(int Id);
    // <summary>
    /// Updating Product (for customer screen)
    /// </summary>
    /// <returns>void</returns>
    public void UpdateProd(Product prod);
}
