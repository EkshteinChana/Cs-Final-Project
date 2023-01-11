﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
using PL.PO;

namespace PL;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl;
    //public static readonly DependencyProperty listProperty = DependencyProperty.Register("List", typeof(ObservableCollection<PO.ProductForList?>), typeof(ProductListWindow), new UIPropertyMetadata(new ObservableCollection<PO.ProductForList?>()));
    private ObservableCollection<PO.ProductForList?> currentProductList//the list of the products 
    {
        get;set;
        //get { return (ObservableCollection<PO.ProductForList?>)GetValue(listProperty); }
        //set { SetValue(listProperty, value); }
    }
    ///// <summary>
    ///// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
    ///// </summary>
    private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
    {
        PO.ProductForList p = new();
        p.Name = bP.Name;   
        p.Price = bP.Price; 
        p.Id = bP.Id;
        p.Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others; 
        return p;
    }

    /// <summary>
    /// constractor of ProductListWindow which imports the list of products.
    /// </summary>
    public ProductListWindow(IBl Ibl)
    {
        InitializeComponent();
        bl = Ibl;
        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
        //ListOfProductForList ProdForLstList = new();
        currentProductList = new();
        bProds.Select(bP =>
        {
            PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
            //ProdForLstList.List?.Add(p);
            currentProductList.Add(p);
            return bP;
        }).ToList();
        //ProductsListview.DataContext = ProdForLstList.List;
        ProductsListview.DataContext = currentProductList;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
    }

    /// <summary>
    /// A function that filters the products by category.
    /// </summary>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //ListOfProductForList ProdForLstList = new();
        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList((BO.eCategory?)CategorySelector.SelectedItem);
        //ProdForLstList.List.Clear();
        currentProductList.Clear();
        bProds.Select(bP =>
        {
            PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
            //ProdForLstList.List.Add(p);
            currentProductList.Add(p);
            return bP;
        }).ToList();
        //ProductsListview.DataContext = ProdForLstList.List;
        ProductsListview.DataContext = currentProductList;
    }
    /// <summary>
    /// A function that opens the ProductWindow for adding a product.
    /// </summary>
    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl,this, (BO.eCategory?)CategorySelector.SelectedItem,null , currentProductList).Show();
        this.Hide();
    }
    /// <summary>
    /// A function that opens the ProductWindow for updating or deleting a product.
    /// </summary>
    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        PO. ProductForList p = (PO. ProductForList)((ListView)sender).SelectedItem;
        new ProductWindow(bl,this, (BO.eCategory?)CategorySelector.SelectedItem, p.Id , currentProductList).Show();
        this.Hide();
    }
    /// <summary>
    /// A function that show all the product
    /// </summary>
    public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
    {
        CategorySelector.SelectedItem = null;
    }
    
    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

}
