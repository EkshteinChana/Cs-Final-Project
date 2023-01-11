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
    private ObservableCollection<PO.ProductForList?> currentProductList;
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
    /// A private help function to convert PO.ProductForList entity to BO.ProductForList entity.
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
        if (pCrt.Items.Count == 0)
            bCrt.Items = new();
        else
            bCrt.Items = (List<BO.OrderItem?>)from itm in pCrt.Items
                                              select new BO.OrderItem
                                              {
                                                  Id = itm.Id,
                                                  ProductId = itm.ProductId,
                                                  Name = itm.Name,
                                                  Price = itm.Price,
                                                  Amount = itm.Amount,
                                                  TotalPrice = itm.TotalPrice,
                                              };
        return bCrt;
    }

    /// <summary>
    /// Constractor of ProductItemWindow for watching a productItem.
    /// </summary>
    public ProductItemWindow(IBl? Ibl, Window? w, BO.eCategory? ctgry, int? id, PO.Cart? crt, ObservableCollection<PO.ProductForList?> cl)
    {
        try
        {
            InitializeComponent();
            bl = Ibl;
            sourcWindow = w;
            catagory = ctgry;
            cart = crt;
            currentProductList = cl;
            BO.Cart bCrt = convertPoCartToBoCart(cart);
            BO.ProductItem bP = bl.Product.ReadProdCustomer((int)id, bCrt);
            PO.ProductItem p = convertBoProdItmToPoProdItm(bP);
            DataContext = p;
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
}
