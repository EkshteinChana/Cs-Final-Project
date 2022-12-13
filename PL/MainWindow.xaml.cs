using System.Windows;
using BlImplementation;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = new Bl();
        /// <summary>
        /// Constractor of MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A function that opens the ProductListWindow.
        /// </summary>
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
            this.Hide();
        }
    }
}
