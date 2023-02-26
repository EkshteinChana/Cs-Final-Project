using BlApi;
using PL;
using PL.Orders;
using PL.Products;
using System;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();

    /// <summary>
    /// Constractor of MainWindow.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// A function that opens the Admin window.
    /// </summary>
    private void AdminEnter_Click(object sender, RoutedEventArgs e)
    {
        new AdminWindow(bl).Show();
        Close();
    }

    /// <summary>
    /// A function that opens the ProductCatalogWindow.
    /// </summary>
    private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
    {
        new ProductCatalogWindow(bl).Show();
        Close();
    }

    /// <summary>
    /// A function that opens the OrderTrackingWindow.
    /// </summary>
    private void TrackBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var input = 0;
            if (!int.TryParse(OrdIdTxtBx.Text, out input))
            {
                throw new InValidInputTypeException("Order ID");
            }
            int oId = Convert.ToInt32(OrdIdTxtBx.Text);
            new OrderTrackingWindow(bl, oId, this).Show();
            Close();
        }
        catch (InValidInputTypeException err)
        { 
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); 
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Simulator_Click(object sender, RoutedEventArgs e)
    {
        new Simulation().Show();
    }
}
