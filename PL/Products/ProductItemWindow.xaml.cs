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
                bl = Ibl;
                ProductItem p = bl.Product.ReadProdCustomer((int)id, cart);
                DataContext = p;                
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

