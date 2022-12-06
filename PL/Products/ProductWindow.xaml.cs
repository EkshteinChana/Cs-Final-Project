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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>

    public partial class ProductWindow : Window
    {
        private IBl bl;
        public ProductWindow(IBl Ibl,int ?id)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            bl = Ibl;
            if (id != null)
            {
                Product p=bl.Product.ReadProdManager((int)id);
                IDLbl.Content = p.Id;
                NameTxtBx.Text = p.Name;
                PriceTxtBx.Text = p.Price.ToString();
                CategorySelector.SelectedItem= p.Category;
                InStockTxtBx.Text = p.InStock.ToString();
                AddProductBtn.Visibility = Visibility.Hidden;
                TitelEnterDetailsLbl.Content = "Change the product details for updating";
            }
            else
            {
                TitelEnterDetailsLbl.Content = "Enter the product details";
                IdLbl.Visibility= Visibility.Hidden;
                IDLbl.Visibility = Visibility.Hidden;
                UpdateProductBtn.Visibility = Visibility.Hidden;
                DeleteProductBtn.Visibility = Visibility.Hidden;
            }
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Product prd = new();
            prd.Name=NameTxtBx.Text;
            prd.Price = Convert.ToDouble(PriceTxtBx.Text);
            prd.Category = (BO.eCategory)CategorySelector.SelectedItem;
            prd.InStock = Convert.ToInt32(InStockTxtBx.Text);
            bl.Product.CreateProd(prd);
        }

        private void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Product prd = new();
            prd.Id = Convert.ToInt32(IDLbl.Content);
            prd.Name = NameTxtBx.Text;
            prd.Price = Convert.ToDouble(PriceTxtBx.Text);
            prd.Category = (BO.eCategory)CategorySelector.SelectedItem;
            prd.InStock = Convert.ToInt32(InStockTxtBx.Text);
            bl.Product.UpdateProd(prd);
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(IDLbl.Content);
            bl.Product.DeleteProd(id);
        }
    }
}
