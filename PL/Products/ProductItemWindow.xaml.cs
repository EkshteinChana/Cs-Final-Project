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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private IBl bl;
        /// <summary>
        /// Constractor of ProductItemWindow for watching a productItem.
        /// </summary>
        public ProductItemWindow(BlApi.IBl Ibl, int id, Cart cart)
        {
            try
            {
                InitializeComponent();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                bl = Ibl;
                ProductItem p = bl.Product.ReadProdCustomer((int)id, cart);
                DataContext = p;
                //if (id != null)
                //{
                //    ProductItem p = bl.Product.ReadProdCustomer((int)id,cart);
                //    DataContext = p;
                //    CategorySelector.SelectedItem = p.Category;
                //    AddProductBtn.Visibility = Visibility.Hidden;
                //    TitelEnterDetailsLbl.Content = "Change the product details for updating";
                //}
                //else//add
                //{
                //    TitelEnterDetailsLbl.Content = "Enter the product details";
                //    IdLbl.Visibility = Visibility.Hidden;
                //    IDLbl.Visibility = Visibility.Hidden;
                //    UpdateProductBtn.Visibility = Visibility.Hidden;
                //    DeleteProductBtn.Visibility = Visibility.Hidden;
                //}
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
            new ProductCatalogWindow(bl).Show();
            this.Close();
        }
    }

