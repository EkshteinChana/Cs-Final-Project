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
using BlImplementation;

namespace PL.Products;
/// <summary>
/// Interaction logic for ProductCatalogWindow.xaml
/// </summary>
public partial class ProductCatalogWindow : Window
{ }
    //private IBl bl;
    //PO.Cart cart =new();

    /////// <summary>
    /////// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
    /////// </summary>
    //private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
    //{
    //    PO.ProductForList p = new();
    //    p.Name = bP.Name;
    //    p.Price = bP.Price;
    //    p.Id = bP.Id;
    //    p.Category = (BO.eCategory)bP.Category;
    //    return p;
    //}
    ///// <summary>
    ///// constractor of ProductCatalogWindow which imports the list of products.
    ///// </summary>
    //public ProductCatalogWindow(IBl Ibl)
    //{
    //    InitializeComponent();
    //    bl = Ibl;
    //    IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
    //    PO.ListOfProductForList ProdForLstList = new();
    //    bProds.Select(bP =>
    //    {
    //        PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
    //        ProdForLstList.List?.Add(p);
    //        return bP;
    //    }).ToList();
    //    ProductsListview.DataContext = ProdForLstList.List;
    //    CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
    //}

    ///// <summary>
    ///// A function that filters the products by category.
    ///// </summary>
    //private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    PO.ListOfProductForList ProdForLstList = new();
    //    IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList((BO.eCategory?)CategorySelector.SelectedItem);
    //    ProdForLstList.List.Clear();
    //    bProds.Select(bP =>
    //    {
    //        PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
    //        ProdForLstList.List.Add(p);
    //        return bP;
    //    }).ToList();
    //    ProductsListview.DataContext = ProdForLstList.List;
    //}
    ///// <summary>
    ///// A function that opens the ProductItemWindow for watching a product.
    ///// </summary>
    //private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    //{
    //    PO.ProductForList p = (PO.ProductForList)((ListView)sender).SelectedItem;
    //    new ProductItemWindow(bl, p.Id, cart,this,(BO.eCategory?)CategorySelector.SelectedItem).Show();
    //    this.Hide();
    //}
    ///// <summary>
    ///// A function that show all the product
    ///// </summary>
    //public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
    //{
    //    CategorySelector.SelectedItem = null;
    //}


    //private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{

    //}

    //private void CartBtn_Click(object sender, RoutedEventArgs e)
    //{

    //}
//}






