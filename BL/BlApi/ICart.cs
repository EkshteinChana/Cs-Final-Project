using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;
/// <summary>
/// Interfaces for actions regarding to shopping cart-all the actions are for customer screen 
/// </summary>

public interface ICart
{
    // <summary>
    /// Adding Product to a cart (for catalog screen and details product screen)
    /// </summary>
    /// <returns>Cart</returns>
    public Cart CreateProdInCart(Cart cart,int Id);
    // <summary>
    /// Updating amount of product in a cart (for cart screen)
    /// </summary>
    /// <returns>Cart</returns>
    public Cart UpdateAmountOfProd(Cart cart, int id,int amount);
    // <summary>
    /// cart confirmation for order/making an order (for shopping cart screen and order completion screen)
    /// </summary>
    /// <returns>void</returns>
    public void MakeOrder(Cart cart, string customerName, string customerEmail, string customerAddress);
}
