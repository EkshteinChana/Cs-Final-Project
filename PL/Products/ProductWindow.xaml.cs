using System;
using System.Linq;
using System.Windows;
using BlApi;
//using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>

    public partial class ProductWindow : Window
    {
        private IBl bl;
        Window mainWindow;

        // <summary>
        /// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
        /// </summary>
        private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
        {
            PO.ProductForList p = new();
            p.GetType().GetProperties().Select(pPr => { pPr.SetValue(p, bP.GetType().GetProperty(pPr.Name)?.GetValue(bP)); return pPr; }).ToList();
            return p;
        }

        // <summary>
        /// A private help function to convert BO.Product entity to PO.Product entity.
        /// </summary>
        private PO.Product convertBoProdToPoProd(BO.Product bP)
        {
            PO.Product p = new();
            p.GetType().GetProperties().Select(pPr => { pPr.SetValue(p, bP.GetType().GetProperty(pPr.Name)?.GetValue(bP)); return pPr; }).ToList();
            return p;
        }


        /// <summary>
        /// Constractor of ProductWindow for add, delete or update an a product.
        /// </summary>
        public ProductWindow(IBl Ibl,Window w,int ?id )
        {
            try
            {
                InitializeComponent();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                bl = Ibl;
                mainWindow = w;

                if (id != null)
                {
                    BO.Product bP = bl.Product.ReadProdManager((int)id);
                    PO.Product p = convertBoProdToPoProd(bP);
                    DataContext = p; 
                    CategorySelector.SelectedItem = p.Category;
                    AddProductBtn.Visibility = Visibility.Hidden;
                    TitelEnterDetailsLbl.Content = "Change the product details for updating";
                }
                else//add
                {
                    TitelEnterDetailsLbl.Content = "Enter the product details";
                    IdLbl.Visibility = Visibility.Hidden;
                    IDLbl.Visibility = Visibility.Hidden;
                    UpdateProductBtn.Visibility = Visibility.Hidden;
                    DeleteProductBtn.Visibility = Visibility.Hidden;
                }
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
        /// A function for adding a new product (in the PL layer).
        /// </summary>
        private void AddProductBtn_Click(object sender, RoutedEventArgs e )
        {
            try
            {
                BO.Product prd = new();
                prd.Name = NameTxtBx.Text;
                prd.Price = Convert.ToDouble(PriceTxtBx.Text);
                prd.Category = (BO.eCategory)CategorySelector.SelectedItem;
                prd.InStock = Convert.ToInt32(InStockTxtBx.Text);
                bl.Product.CreateProd(prd);
                MessageBox.Show("The addition was made successfully");
                mainWindow.Show();
                this.Hide();    
            }
            catch (InvalidValue exc) {
                MessageBox.Show(exc.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// A function for updating a product (in the PL layer).
        /// </summary>
        private void UpdateProductBtn_Click(object sender, RoutedEventArgs e )
        {
            try
            {
                BO.Product prd = new();
                prd.Id = Convert.ToInt32(IDLbl.Content);
                prd.Name = NameTxtBx.Text;
                prd.Price = Convert.ToDouble(PriceTxtBx.Text);
                prd.Category = (BO.eCategory)CategorySelector.SelectedItem;
                prd.InStock = Convert.ToInt32(InStockTxtBx.Text);
                bl.Product.UpdateProd(prd);
                MessageBox.Show("The update was successful");
                mainWindow.Show();  
                this.Hide();
            }
            catch (InvalidValue exc)
            {
                MessageBox.Show(exc.Message);
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
        /// A function for deleting a product (in the PL layer).
        /// </summary>
        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(IDLbl.Content);
                bl.Product.DeleteProd(id);
                MessageBox.Show("The deletion was successful");
            }
            catch (IllegalAction exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (DataError dataError)
            {
                MessageBox.Show(dataError.Message + " " + dataError?.InnerException?.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                mainWindow.Show();
                this.Hide();
            }
            
        }

        /// <summary>
        /// A function that opens the ProductListWindow.
        /// </summary>
        private void ShowProductListBtn_Click(object sender, RoutedEventArgs e )
        {
            mainWindow.Show();
            this.Hide();
        }
    }
}
