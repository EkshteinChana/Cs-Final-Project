using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BlApi;
using PL.PO;

namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl;
    Window sourcWindow;
    BO.eCategory? catagory;
    private ObservableCollection<PO.ProductForList?> currentProductList;
    PO.Product currentProd = new();
    /// <summary>
    /// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
    /// </summary>
    private PO.ProductForList convertBoPrdLstToPoPrdLst(BO.ProductForList bP)
    {
        PO.ProductForList p = new()
        {
            Name = bP.Name,
            Price = bP.Price,
            Id = bP.Id,
            Category = (BO.eCategory?)bP.Category
        };
        return p;
    }

    /// <summary>
    /// A private help function for updating the currentProductList.
    /// </summary>
    private void UpdateCrrntPrdLst()
    {
        currentProductList.Clear();
        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList(catagory);
        bProds.Select(bP =>
        {
            PO.ProductForList p = convertBoPrdLstToPoPrdLst(bP);
            currentProductList.Add(p);
            return bP;
        }).ToList();
    }

    /// <summary>
    /// A private help function to convert BO.Product entity to PO.Product entity.
    /// </summary>
    private PO.Product convertBoProdToPoProd(BO.Product bP)
    {
        PO.Product p = new()
        {
            Id = bP.Id,
            Name = bP.Name,
            Price = bP.Price,
            InStock = bP.InStock,
            Category = (BO.eCategory?)bP.Category
        };
        return p;
    }

    /// <summary>
    /// A private help function to check if the type of the input is incorrect.
    /// </summary>
    private void checkTypeInput()
    {
        double inputP = 0;
        if (!double.TryParse(PriceTxtBx.Text, out inputP))
            throw new InValidInputTypeException("price");
        int inputI = 0;
        if (!int.TryParse(InStockTxtBx.Text, out inputI))
            throw new InValidInputTypeException("amount in stock");
    }

    /// <summary>
    /// A private help function to convert PO.Product entity to BO.Product entity.
    /// </summary>
    private BO.Product convertPoProdToBoProd(PO.Product p)
    {
        BO.Product bP = new()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            InStock = p.InStock,
            Category = (BO.eCategory?)p.Category
        };
        return bP;
    }


    /// <summary>
    /// Constractor of ProductWindow for add, delete or update an a product.
    /// </summary>
    public ProductWindow(IBl Ibl, Window w, BO.eCategory? ctgry, int? id, ObservableCollection<PO.ProductForList?> cl)
    {
        try
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            bl = Ibl;
            sourcWindow = w;
            catagory = ctgry;
            currentProductList = cl;
            if (id != null)//update and delete
            {
                BO.Product bP = bl.Product.ReadProdManager((int)id);
                currentProd = convertBoProdToPoProd(bP);
                TitelEnterDetailsLbl.Content = "Change the product details for updating";
            }
            else//add
            {
                TitelEnterDetailsLbl.Content = "Enter the product details";
            }
            DataContext = currentProd;
        }
        catch (InvalidValueException exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (DataErrorException dataError)
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
    private void AddProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            checkTypeInput();
            BO.Product prd = convertPoProdToBoProd(currentProd);
            bl.Product.CreateProd(prd);
            MessageBox.Show("The addition was made successfully");
            UpdateCrrntPrdLst();
            sourcWindow.Show();
            this.Close();
        }
        catch (InValidInputTypeException exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (InvalidValueException exc)
        {
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
    private void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            checkTypeInput();
            BO.Product prd = convertPoProdToBoProd(currentProd);
            bl.Product.UpdateProd(prd);
            MessageBox.Show("The update was successful");
            UpdateCrrntPrdLst();
            sourcWindow.Show();
            this.Close();
        }
        catch (InValidInputTypeException exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (InvalidValueException exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (DataErrorException dataError)
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
            bl.Product.DeleteProd(currentProd.Id);
            MessageBox.Show("The deletion was successful");
            UpdateCrrntPrdLst();
            sourcWindow.Show();
            this.Close();
        }
        catch (IllegalActionException exc)
        {
            MessageBox.Show(exc.Message);
        }
        catch (DataErrorException dataError)
        {
            MessageBox.Show(dataError.Message + " " + dataError?.InnerException?.Message);
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message);
        }
    }

    /// <summary>
    /// A function that opens the ProductListWindow.
    /// </summary>
    private void ShowProductListBtn_Click(object sender, RoutedEventArgs e)
    {
        sourcWindow.Show();
        this.Close();
    }
}
