using BlApi;
using BlImplementation;
using PL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PL.Cart;
/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private IBl bl;
    PO.Cart cart;
    /// <summary>
    /// A private help function to convert PO.Cart entity to BO.Cart entity.
    /// </summary>
    private BO.Cart convertPoCartToBoCart(PO.Cart pCrt)
    {
        BO.Cart bCrt = new()
        {
            CustomerName = pCrt.CustomerName,
            CustomerEmail = pCrt.CustomerEmail,
            CustomerAddress = pCrt.CustomerAddress,
            TotalPrice = pCrt.TotalPrice
        };
        bCrt.Items = new();
        if (pCrt.Items.Count != 0)
            pCrt.Items.Select(itm =>
            {
                BO.OrderItem oI = new()
                {
                    Id = itm.Id,
                    ProductId = itm.ProductId,
                    Name = itm.Name,
                    Price = itm.Price,
                    Amount = itm.Amount,
                    TotalPrice = itm.TotalPrice,
                };
                bCrt.Items.Add(oI);
                return itm;
            }).ToList();
        return bCrt;
    }
    /// <summary>
    /// A private help function to convert BO.Cart entity to PO.Cart entity.
    /// </summary>
    private PO.Cart convertBoCartToPoCart(BO.Cart bCrt)
    {
        PO.Cart pCrt = new()
        {
            CustomerName = bCrt.CustomerName,
            CustomerEmail = bCrt.CustomerEmail,
            CustomerAddress = bCrt.CustomerAddress,
            TotalPrice = bCrt.TotalPrice
        };
        if (bCrt.Items.Count == 0)
            pCrt.Items = new();
        else
            bCrt.Items.Select(itm =>
            {
                PO.OrderItem oI = new()
                {
                    Id = itm.Id,
                    ProductId = itm.ProductId,
                    Name = itm.Name,
                    Price = itm.Price,
                    Amount = itm.Amount,
                    TotalPrice = itm.TotalPrice,
                };
                pCrt.Items.Add(oI);
                return itm;
            }).ToList();
        return pCrt;
    }
    /// <summary>
    /// constractor of CartWindow which imports the list of the orderItems in the cart.
    /// </summary>
    public CartWindow(IBl Ibl, Window w, PO.Cart c)
    {
        InitializeComponent();
        bl = Ibl;
        cart = c;
        DataContext = cart;
    }

    /// <summary>
    /// A function for order confirmation (in the PL layer).
    /// </summary>
    private void MakeOrderBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Cart bCart = convertPoCartToBoCart(cart);
            bl.Cart.MakeOrder(bCart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAddress);
            MessageBox.Show("The order has been sent successfully");
            cart.Items.Clear();
            cart = new();
            DataContext = cart;///???
        }
        catch (OutOfStock exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (InvalidValue exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (DataError dataError)
        {
            MessageBox.Show(dataError.Message + " " + dataError?.InnerException?.Message);
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message);
        }
    }

    /// <summary>
    /// A function for returning to the ProductCatalogWindow.
    /// </summary>
    private void ReturnToCatalogBtn_Click(object sender, RoutedEventArgs e)
    {
        new PL.Products.ProductCatalogWindow(bl,cart).Show();
        this.Close();
    }
    /// <summary>
    /// A private help function for updating amount of orderItem in the cart.
    /// </summary>
    private void UpdateAmount(int ID, int amount)
    {
        try
        {
            BO.Cart bCrt = convertPoCartToBoCart(cart);
            bCrt = bl.Cart.UpdateAmountOfProd(bCrt, ID, amount);
            cart.Items.Clear();
            cart = convertBoCartToPoCart(bCrt);
            DataContext = cart;
        }
        catch (InvalidValue exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (OutOfStock exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (ItemNotExist exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (DataError dataError)
        {
            MessageBox.Show(dataError.Message + " " + dataError?.InnerException?.Message);
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message);
        }
    }
    /// <summary>
    /// A function to increase the amount of a product in the cart by 1.
    /// </summary>
    private void IncreaseBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem currentOI = (PO.OrderItem)((Button)sender).DataContext;
        UpdateAmount(currentOI.ProductId, currentOI.Amount + 1);
    }
    /// <summary>
    /// A function to decrease the amount of a product in the cart by 1.
    /// </summary>
    private void DecreaseBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem currentOI = (PO.OrderItem)((Button)sender).DataContext;
        UpdateAmount(currentOI.ProductId, currentOI.Amount - 1);
    }
    /// <summary>
    /// A function to delete a product from the cart.
    /// </summary>
    private void DeleteBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem currentOI = (PO.OrderItem)((Button)sender).DataContext;
        UpdateAmount(currentOI.ProductId, 0);
    }
    /// <summary>
    /// A function emptying a cart.
    /// </summary>
    private void EmptyCart_Click(object sender, RoutedEventArgs e)
    {
        cart.Items.Clear();
        cart = new();
        DataContext = cart;
    }
    private void OrderItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e){}
}
