using System;
using System.Windows;
using BlApi;
namespace PL.Products;
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private IBl bl;
        Window ProductCatalogWindow;
        PO.ProductItem p = new();
        /// <summary>
        /// Constractor of ProductItemWindow for watching a productItem.
        /// </summary>
        public ProductItemWindow(BlApi.IBl Ibl, int id, BO.Cart cart, Window w)
        {
            try
            {
                InitializeComponent();
                ProductCatalogWindow = w;   
                bl = Ibl;
                
                bl.Product.ReadProdCustomer((int)id, cart);
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
            ProductCatalogWindow.Show();
            this.Close();
        }
    }

