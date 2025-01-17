﻿using System;
using System.Linq;
using System.Windows;
using BlApi;
namespace PL.Products;
/// <summary>
/// Interaction logic for ProductItemWindow.xaml
/// </summary>
public partial class ProductItemWindow : Window
{
    private IBl bl;
    private BO.eCategory? catagory;
    private PO.Cart cart;
    private PO.ProductItem currentProd;
    private int? orderId;
    private Window sourceWindow;
    bool isConfirmed = false;
    Action? action;

    /// <summary>
    /// Constractor of ProductItemWindow for watching a productItem.
    /// </summary>
    public ProductItemWindow(IBl? Ibl, Window? w, BO.eCategory? ctgry, int? id, PO.Cart crt = null, int? ordId = null, Action? actn = null)
    {
        try
        {
            InitializeComponent();
            bl = Ibl;
            orderId = ordId;
            isConfirmed = orderId != null;
            sourceWindow = w;
            catagory = ctgry;
            cart = crt;
            action = actn;
            BO.Cart bCrt = convertPoCartToBoCart(cart);
            BO.ProductItem bP = bl?.Product.ReadProdCustomer((int)id, bCrt);
            currentProd = convertBoProdItmToPoProdItm(bP);
            var ob = new { currentProd, isConfirmed };
            DataContext = ob;
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// A function that opens the ProductCatalogWindow.
    /// </summary>
    private void ShowProductListBtn_Click(object sender, RoutedEventArgs e)
    {
        if (orderId != null) // When the order is confirmed
        {
            sourceWindow.Show();
        }
        else
            new ProductCatalogWindow(bl, cart).Show();
        Close();
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
            MessageBox.Show("The product quantity has been successfully reduced", "is decreased", MessageBoxButton.OK, MessageBoxImage.Information);
            new ProductCatalogWindow(bl, cart).Show();
            Close();
        }
        
        catch (OutOfStockException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (ItemNotExistException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
            MessageBox.Show("The product has been successfully added", "is added", MessageBoxButton.OK, MessageBoxImage.Information);
            new ProductCatalogWindow(bl, cart).Show();
            Close();
        }
        catch (OutOfStockException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void AddMeBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Order.UpdateOrd(orderId ?? -1, currentProd.Id, 1 , BO.eUpdateOrder.add);
            action?.Invoke();
            MessageBox.Show("The product has been successfully added", "is added", MessageBoxButton.OK, MessageBoxImage.Information);
            sourceWindow.Show();
            Close();
        }
        catch(IllegalActionException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
        
    }
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

}
