using System;
using System.Collections.Generic;
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
using BlApi;
using BO;
namespace PL.Products;
/// <summary>
/// Interaction logic for ProductCatalogWindow.xaml
/// </summary>
public partial class ProductCatalogWindow : Window
{
    private IBl bl;
    Cart cart;
    /// <summary>
    /// constractor of ProductCatalogWindow which imports the list of products.
    /// </summary>
    public ProductCatalogWindow(IBl Ibl)
    {
        InitializeComponent();
        bl = Ibl;
        ProductsListview.ItemsSource = bl.Product.ReadProdsList();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
    }

    /// <summary>
    /// A function that filters the products by category.
    /// </summary>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.eCategory ctgry = (BO.eCategory)CategorySelector.SelectedItem;
        //ProductsListview.ItemsSource = bl.Product.ReadProdsList(ctgry);
    }
    /// <summary>
    /// A function that opens the ProductItemWindow for watching a product.
    /// </summary>
    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        //ProductForList p = (ProductForList)((ListView)sender).SelectedItem;
        //new ProductItemWindow(bl, p.Id, cart).Show();
        //this.Close();
    }
    /// <summary>
    /// A function that show all the product
    /// </summary>
    public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
    {
        ProductsListview.ItemsSource = bl.Product.ReadProdsList();
    }


    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void CartBtn_Click(object sender, RoutedEventArgs e)
    {

    }
}

