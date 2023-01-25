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
using BlApi;
using BlImplementation;

namespace PL.Products;
/// <summary>
/// Interaction logic for ProductCatalogWindow.xaml
/// </summary>
public partial class ProductCatalogWindow : Window
{
    private IBl bl;
    private ObservableCollection<PO.ProductForList?> currentProductList { get; set; }//the list of the products
    PO.Cart cart;

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
    /// <summary>
    /// constractor of ProductCatalogWindow which imports the list of products.
    /// </summary>
    public ProductCatalogWindow(IBl Ibl, PO.Cart c = null)
    {
        InitializeComponent();
        cart = c ?? new PO.Cart();
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
        new ProductItemWindow(bl, this, (BO.eCategory?)CategorySelector.SelectedItem, p.Id, cart).Show();
        this.Close();
    }
    /// <summary>
    /// A function that show all the product
    /// </summary>
    public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
    {
        CategorySelector.SelectedItem = null;
    }
    private void CartBtn_Click(object sender, RoutedEventArgs e)
    {
        new Cart.CartWindow(bl, this, cart).Show();
        this.Close();
    }
    /// <summary>
    /// A function for returning to the ProductCatalogWindow.
    /// </summary>
    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        cart.Items.Clear();
        cart = new();
        new MainWindow().Show();
        this.Close();
    }
    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
}
