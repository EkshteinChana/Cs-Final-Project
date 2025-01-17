﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
namespace PL.Products;

/// <summary>
/// Interaction logic for ProductCatalogWindow.xaml
/// </summary>
public partial class ProductCatalogWindow : Window
{
    private IBl bl;
    private ObservableCollection<PO.ProductForList?> currentProductList { get; set; }//the list of the products
    private PO.Cart cart = new();
    private Window? srcW;
    private int? OrderId;
    private Action? action;

    /// <summary>
    /// constractor of ProductCatalogWindow which imports the list of products.
    /// </summary>
    public ProductCatalogWindow(IBl Ibl, PO.Cart c = null, Window sourcW = null, int? orderId = null, Action? actn=null)
    {
        InitializeComponent();
        cart = c ?? new PO.Cart();
        srcW = sourcW;
        OrderId = orderId;
        action= actn;
        bl = Ibl;
        if (srcW != null) //Enter from OrderTracking
        {
            ExitBtn.Content = "Back";
        }
        else
        {
            ExitBtn.Content = "Exit";
        }
        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
        currentProductList = new();
        bProds.Select(bP =>
        {
            PO.ProductForList p = convertBoPrdLstToPoPrdLst(bP);
            currentProductList.Add(p);
            return bP;
        }).ToList();
        ProductsListview.DataContext = currentProductList;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        CartBtn.DataContext = cart;
    }

    /// <summary>
    /// A function that filters the products by category.
    /// </summary>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList((BO.eCategory?)CategorySelector.SelectedItem);
        currentProductList.Clear();
        bProds.Select(bP =>
        {
            PO.ProductForList p = convertBoPrdLstToPoPrdLst(bP);
            currentProductList.Add(p);
            return bP;
        }).ToList();
        ProductsListview.DataContext = currentProductList;
    }

    /// <summary>
    /// A function that opens the ProductItemWindow for watching a product.
    /// </summary>
    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        PO.ProductForList p = (PO.ProductForList)((ListView)sender).SelectedItem;
        new ProductItemWindow(bl, this, (BO.eCategory?)CategorySelector.SelectedItem, p.Id, cart, OrderId , action).Show();
        if (OrderId != null)
        {
            Hide();
        }
        else
            Close();
    }

    /// <summary>
    /// A function that show all the product
    /// </summary>
    public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
    {
        CategorySelector.SelectedItem = null;
    }

    /// <summary>
    /// A function that opens the CartWindow for watching he cart, 
    /// the function is not possible if the user enterd in order to add an item for his exist order.
    /// </summary>
    private void CartBtn_Click(object sender, RoutedEventArgs e)
    {
        new Cart.CartWindow(bl,cart).Show();
        Close();
    }

    /// <summary>
    /// A function for returning to the mainWindow or to the orderWindow.
    /// </summary>
    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        cart.Items.Clear();
        cart = new();
        if (srcW != null) // return to the Order details
        {
            srcW.Show();
        }
        else
        {
            new MainWindow().Show();
        }
        Close();
    }

    /// <summary>
    /// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
    /// </summary>
    private PO.ProductForList convertBoPrdLstToPoPrdLst(BO.ProductForList bP)
    {
        PO.ProductForList p = new()
        {
            Name = bP.Name,
            Price = bP.Price,
            Id = bP.Id,
            Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others
        };
        return p;
    }
}
