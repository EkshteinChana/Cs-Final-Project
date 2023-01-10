using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;
        ObservableCollection<PO.ProductForList> ProdList = new();
        ///// <summary>
        ///// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
        ///// </summary>
        private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
        {
            PO.ProductForList p = new();
            p.Name = bP.Name;   
            p.Price = bP.Price; 
            p.Id = bP.Id;
            p.Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others; 
            return p;
        }

        /// <summary>
        /// constractor of ProductListWindow which imports the list of products.
        /// </summary>

        ///
        public ProductListWindow(IBl Ibl)
        {
            InitializeComponent();
            bl = Ibl;
            IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
            bProds.Select(bP =>
            {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                ProdList?.Add(p);
                return bP;
            }).ToList();
            ProductsListview.DataContext = ProdList;
        }

        /// <summary>
        /// A function that filters the products by category.
        /// </summary>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
            ProdList.Clear();
            bProds.Where(bP => bP?.Category == (BO.eCategory)CategorySelector.SelectedItem).Select(bP =>
            {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                ProdList.Add(p);
                return bP;
            }).ToList();
            ProductsListview.DataContext = ProdList;    
        }
        /// <summary>
        /// A function that opens the ProductWindow for adding a product.
        /// </summary>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl,this,null).Show();
            this.Hide();
        }
        /// <summary>
        /// A function that opens the ProductWindow for updating or deleting a product.
        /// </summary>
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO. ProductForList p = (BO. ProductForList)((ListView)sender).SelectedItem;
            new ProductWindow(bl,this,p.Id).Show();
            this.Hide();
        }
        /// <summary>
        /// A function that show all the product
        /// </summary>
        public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProdList.Clear();
            IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();   
            bProds.Select(bP =>
            {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                ProdList.Add(p);
                return bP;
            }).ToList();
            ProductsListview.DataContext = ProdList;
        }
        

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
