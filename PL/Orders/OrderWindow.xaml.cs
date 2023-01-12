using BlApi;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    IBl bl;
    ObservableCollection<PO.OrderForList?> currentOrderList;
    Window sourcWindow;

    private PO.Order convertBoOrdToPoOrd(BO.Order bo)
    {
        PO.Order po = new()
        {
            CustomerAddress = bo.CustomerAddress,
            CustomerEmail = bo.CustomerEmail,
            CustomerName = bo.CustomerName,
            DeliveryDate = bo.DeliveryDate,
            OrderDate = bo.OrderDate,
            ShipDate = bo.ShipDate,
            TotalPrice = bo.TotalPrice
        };
        //if(bo.status == )
        //Items
        return po;
    }
    public OrderWindow(IBl Ibl, Window w, int id, ObservableCollection<PO.OrderForList?> cl)
    {
        try
        {
            InitializeComponent();
            bl = Ibl;
            currentOrderList = cl;
            sourcWindow = w;
            if (id != null)
            {
                //BO.Product bP = bl.Product.ReadProdManager((int)id);
                PO.Order o = convertBoOrdToPoOrd(bl.Order.ReadOrd(id));
                //DataContext = p;
                //CategorySelector.SelectedItem = p.Category;
                //AddProductBtn.Visibility = Visibility.Hidden;
                //TitelEnterDetailsLbl.Content = "Change the product details for updating";

            }
        }
        catch {

        }
    }

    private void OrderDateTxtBx_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void UpdateOrdeBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ShowProductListBtn_Click(object sender, RoutedEventArgs e)
    {

    }
}
