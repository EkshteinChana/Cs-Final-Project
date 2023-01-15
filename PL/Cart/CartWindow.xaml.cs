using BlApi;
using BlImplementation;
using PL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    private ObservableCollection<PO.ProductForList?> currentProdItmList { get; set; }//the list of the product items 
    /// <summary>
    /// constractor of CartWindow which imports the list of the productItems in the cart.
    /// </summary>
    public CartWindow()
    {
        InitializeComponent();
        bl = Ibl;
        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
        currentProductList = new();
        bProds.Select(bP =>
        {
            PO.ProductForList p = convertBoPrdLstToPoPrdLst(bP);
            currentProductList.Add(p);
            return bP;
        }).ToList();
        ProductsListview.DataContext = currentProdItmList;
    }

    private void MakeOrderBtn_Click(object sender, RoutedEventArgs e)
    {

    }
}










public ProductListWindow(IBl Ibl)
{
    InitializeComponent();
    bl = Ibl;
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
/// A function that opens the ProductWindow for adding a product.
/// </summary>
private void AddProductButton_Click(object sender, RoutedEventArgs e)
{
    new ProductWindow(bl, this, (BO.eCategory?)CategorySelector.SelectedItem, null, currentProductList).Show();
    this.Hide();
}
/// <summary>
/// A function that opens the ProductWindow for updating or deleting a product.
/// </summary>
private void OrderItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
{
    PO.ProductForList p = (PO.ProductForList)((ListView)sender).SelectedItem;
    new ProductWindow(bl, this, (BO.eCategory?)CategorySelector.SelectedItem, p.Id, currentProductList).Show();
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