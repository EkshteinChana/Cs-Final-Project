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
using BlImplementation;
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

        public ProductListWindow(IBl Ibl)
        {
            InitializeComponent();
            bl = Ibl;
            ProductsListview.ItemsSource = bl.Product.ReadProdsList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.eCategory ctgry=(BO.eCategory)CategorySelector.SelectedItem;
            ProductsListview.ItemsSource = bl.Product.ReadProdsByCategory(ctgry);
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl,null).Show();
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductForList p = (ProductForList)((ListView)sender).SelectedItem;
            new ProductWindow(bl,p.Id).Show();
        }

        private void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsListview.ItemsSource = bl.Product.ReadProdsList();
        }
    }
}
