using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;
        /// <summary>
        /// constractor of ProductListWindow which imports the list of products.
        /// </summary>
        public ProductListWindow(IBl Ibl)
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
            ProductsListview.ItemsSource = bl.Product.ReadProdsByCategory(ctgry);
        }
        /// <summary>
        /// A function that opens the ProductWindow for adding a product.
        /// </summary>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl, null).Show();
            this.Hide();
        }
        /// <summary>
        /// A function that opens the ProductWindow for updating or deleting a product.
        /// </summary>
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductForList p = (ProductForList)((ListView)sender).SelectedItem;
            new ProductWindow(bl, p.Id).Show();
            this.Hide();
        }
        /// <summary>
        /// A function that show all the product
        /// </summary>
        private void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
           ProductsListview.ItemsSource = bl.Product.ReadProdsList();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
