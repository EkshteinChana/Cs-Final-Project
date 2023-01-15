using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using BlApi;
using BlImplementation;
using BO;
using DalApi;
using DO;

//using PL.PO;

namespace PL.Products;
/// <summary>
/// Interaction logic for ProductItemWindow.xaml
/// </summary>
public partial class ProductItemWindow : Window
{
    private IBl bl;
    Window sourcWindow;
    BO.eCategory? catagory;
    PO.Cart? cart;
    PO.ProductItem currentProd;
    //public int Amountx//The quantity of this product in the customer's cart
    //{
    //    get { return (int)GetValue(amountProperty); }
    //    set { SetValue(amountProperty, value); }
    //}
    //public static readonly DependencyProperty amountProperty = DependencyProperty.Register("Amountx", typeof(int), typeof(ProductItem), new UIPropertyMetadata(0));
    /// <summary>
    /// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
    /// </summary>
    private PO.ProductItem convertBoProdItmToPoProdItm(BO.ProductItem bP)
    {
        PO.ProductItem p = new()
        {
            Name = bP.Name,
            Price = bP.Price,
            Id = bP.Id,
            Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others,
            Amount = bP.Amount,
            InStock = bP.InStock,
        };
        return p;
    }
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
    /// Constractor of ProductItemWindow for watching a productItem.
    /// </summary>
    public ProductItemWindow(IBl? Ibl, Window? w, BO.eCategory? ctgry, int? id,ref PO.Cart? crt)
    {
        try
        {
            InitializeComponent();
            bl = Ibl;
            sourcWindow = w;
            catagory = ctgry;
            cart = crt;
            BO.Cart bCrt = convertPoCartToBoCart(cart);
            BO.ProductItem bP = bl.Product.ReadProdCustomer((int)id, bCrt);
            currentProd = convertBoProdItmToPoProdItm(bP);
            DataContext = currentProd;
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
    /// A function that opens the ProductCatalogWindow.
    /// </summary>
    private void ShowProductListBtn_Click(object sender, RoutedEventArgs e)
    {
        sourcWindow.Show();
        this.Close();
    }
    /// <summary>
    /// A function to decrease the amount of a product in the cart by 1.
    /// </summary>
    private void decreaseBy1Btn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Cart bCrt = convertPoCartToBoCart(cart);
            int amount = Convert.ToInt32(AmountContentLbl.Content);
            bCrt = bl.Cart.UpdateAmountOfProd(bCrt, currentProd.Id, amount - 1);
            cart.Items.Clear();
            cart = convertBoCartToPoCart(bCrt);
            BO.ProductItem bP = bl.Product.ReadProdCustomer((int)currentProd.Id, bCrt);
            currentProd = convertBoProdItmToPoProdItm(bP);
           // Amountx = currentProd.Amount;
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
    private void increaseBy1Btn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Cart bCrt = convertPoCartToBoCart(cart);
            bCrt = bl.Cart.CreateProdInCart(bCrt, currentProd.Id);
            cart.Items.Clear();
            cart = convertBoCartToPoCart(bCrt);
            BO.ProductItem bP = bl.Product.ReadProdCustomer((int)currentProd.Id, bCrt);
            currentProd = convertBoProdItmToPoProdItm(bP);
            //Amountx = currentProd.Amount;
        }
        catch (InvalidValue exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (OutOfStock exc)
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
}
