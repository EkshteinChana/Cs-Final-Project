using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
//using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;

        /// <summary>
        /// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
        /// </summary>
        private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
        {
            //PO.ProductForList p = new();
            //p.GetType().GetProperties().Where(pPr => pPr.Name != "Category").Select(pPr => { pPr.SetValue(p, bP.GetType().GetProperty(pPr.Name)?.GetValue(bP)); return pPr; }).ToList();
            //p.Category = bP.Category;
            //return p;

            PO.ProductForList p = new();
            p.GetType().GetProperties().Select(pPr => { pPr.SetValue(p, bP.GetType().GetProperty(pPr.Name)?.GetValue(bP)); return pPr; }).ToList();     
            return p;
        }

        /// <summary>
        /// constractor of ProductListWindow which imports the list of products.
        /// </summary>
        public ProductListWindow(IBl Ibl,string user)
        {
            InitializeComponent();
            bl = Ibl;
            IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
            IEnumerable<PO.ProductForList?> tmpPrdLst = new List<PO.ProductForList>(bProds.Count());
            List<PO.ProductForList?> PrdLst = tmpPrdLst?.ToList();
            bProds.Select(bP => {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                PrdLst?.Add(p);
                return bP;
            }).ToList();
            ProductsListview.ItemsSource = PrdLst;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        /// <summary>
        /// A function that filters the products by category.
        /// </summary>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.eCategory ctgry = (BO.eCategory)CategorySelector.SelectedItem;

            IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsByCategory(ctgry);
            IEnumerable<PO.ProductForList?> tmpPrdLst = new List<PO.ProductForList>(bProds.Count());
            List<PO.ProductForList?> PrdLst = tmpPrdLst.ToList();
            bProds.Select(bP => {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                PrdLst.Add(p);
                return bP;
            }).ToList();
            ProductsListview.ItemsSource = PrdLst;

            //ProductsListview.ItemsSource = bl.Product.ReadProdsByCategory(ctgry);
        }
        /// <summary>
        /// A function that opens the ProductWindow for adding a product.
        /// </summary>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl, this, null).Show();
            this.Hide();
        }
        /// <summary>
        /// A function that opens the ProductWindow for updating or deleting a product.
        /// </summary>
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//???
            PO.ProductForList p = (PO.ProductForList)((ListView)sender).SelectedItem;
            new ProductWindow(bl, this, p.Id).Show();
            this.Hide();
        }
        /// <summary>
        /// A function that show all the product
        /// </summary>
        private void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
            IEnumerable<PO.ProductForList?> tmpPrdLst = new List<PO.ProductForList>(bProds.Count());
            List<PO.ProductForList?> PrdLst = tmpPrdLst.ToList();
            bProds.Select(bP => {
                PO.ProductForList p = convertBoProdForLstToPoProdForLst(bP);
                PrdLst.Add(p);
                return bP;
            }).ToList();
            ProductsListview.ItemsSource = PrdLst;
            //ProductsListview.ItemsSource = bl.Product.ReadProdsList();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
