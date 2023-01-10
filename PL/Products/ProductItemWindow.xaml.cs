using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using BlApi;
using BO;
using DalApi;

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


    ///// <summary>
    ///// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
    ///// </summary>
    private PO.ProductItem convertBoProdItmToPoProdItm(BO.ProductItem bP)
    {
        PO.ProductItem p = new();
        p.Name = bP.Name;
        p.Price = bP.Price;
        p.Id = bP.Id;
        p.Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others;
        p.Amount = bP.Amount;
        p.InStock = bP.InStock;
        return p;
    }
    ///// <summary>
    ///// A private help function to convert PO.ProductForList entity to BO.ProductForList entity.
    ///// </summary>
    private BO.Cart convertPoCartToBoCart(PO.Cart pCrt)
    {
        BO.Cart bCrt = new();
        bCrt.CustomerName = pCrt.CustomerName;
        bCrt.CustomerEmail = pCrt.CustomerEmail;
        bCrt.CustomerAddress = pCrt.CustomerAddress;
        bCrt.Items = (List<BO.OrderItem?>)from itm in pCrt.Items
                                          select new BO.OrderItem
                                          {
                                              Id = itm.Id,
                                              Name = itm.Name,
                                              Price = itm.Price,
                                              Amount = itm.Amount,
                                              TotalPrice = itm.TotalPrice,

                                          };
        bCrt.TotalPrice = pCrt.TotalPrice;
        return bCrt;
    }


    /// <summary>
    /// Constractor of ProductItemWindow for watching a productItem.
    /// </summary>
    /// IBl Ibl, int id, PO.Cart crt, Window w, BO.eCategory? ctgry
    public ProductItemWindow(IBl? Ibl, Window? w, BO.eCategory? ctgry, int? id, PO.Cart? crt)

    {
        try
        {
            InitializeComponent();
            //bl = Ibl;
            //sourcWindow = w;
            //catagory = ctgry;
            //cart = crt;
            //BO.Cart bCrt = convertPoCartToBoCart(cart);
            //BO.ProductItem bP = bl.Product.ReadProdCustomer((int)id, bCrt);
            //PO.ProductItem p = convertBoProdItmToPoProdItm(bP);
            //DataContext = p;
        }
        catch (DataError dataError)
        {
            MessageBox.Show(dataError.Message + " " + dataError?.InnerException?.Message);
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message);
        }
        ibl = Ibl;
        this.w = w;
        this.ctgry = ctgry;
        this.id = id;
        this.crt = crt;
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