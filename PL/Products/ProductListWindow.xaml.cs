using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        ObservableCollection<PO.ProductForList> ProdList = new();
        ///// <summary>
        ///// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
        ///// </summary>
        private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
        {
            //PO.ProductForList p = new();
            //p.GetType().GetProperties().Where(pPr => pPr.Name != "Category").Select(pPr => { pPr.SetValue(p, bP.GetType().GetProperty(pPr.Name)?.GetValue(bP)); return pPr; }).ToList();
            //p.Category = bP.Category;
            //return p;

            PO.ProductForList p = new();
            p.Name = bP.Name;   
            p.Price = bP.Price; 
            p.Id = bP.Id;
            p.Category = (eCategory?)bP.Category ?? eCategory.Others; 
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
            //ProductsListview.ItemsSource = bl.Product.ReadProdsList();
            //CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            IEnumerable<ProductForList?> bProds = bl.Product.ReadProdsList();
            //IEnumerable<PO.ProductForList?> tmpPrdLst = new List<PO.ProductForList>(bProds.Count());
            //List<PO.ProductForList?> PrdLst = tmpPrdLst?.ToList();
            bProds.Select(bP =>
            {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                ProdList?.Add(p);
                return bP;
            }).ToList();
            ProductsListview.DataContext = ProdList;
            //ProductsListview.ItemsSource = PrdLst;
        }

        /// <summary>
        /// A function that filters the products by category.
        /// </summary>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //BO.eCategory ctgry = (BO.eCategory)CategorySelector.SelectedItem;
            //ProductsListview.ItemsSource = bl.Product.ReadProdsByCategory(ctgry);
            IEnumerable<ProductForList?> bProds = bl.Product.ReadProdsList();
            ProdList.Clear();
            bProds.Where(bP => bP?.Category == (eCategory)CategorySelector.SelectedItem).Select(bP =>
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
            new ProductWindow(bl,null).Show();
            this.Close();
        }
        /// <summary>
        /// A function that opens the ProductWindow for updating or deleting a product.
        /// </summary>
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductForList p = (ProductForList)((ListView)sender).SelectedItem;
            //new ProductWindow(bl,p.Id).Show();
            this.Close();
        }
        /// <summary>
        /// A function that show all the product
        /// </summary>
        public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            //ProductsListview.DataContext = bl.Product.ReadProdsList();
            ProdList.Clear();
            IEnumerable<ProductForList?> bProds = bl.Product.ReadProdsList();   
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
