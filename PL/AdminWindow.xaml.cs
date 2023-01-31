using System;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BlApi.IBl bl;
    /// <summary>
    /// Constractor of AdminWindow.
    /// </summary>
    public AdminWindow(BlApi.IBl Ibl)
    {
        InitializeComponent();
        bl = Ibl;
    }

    /// <summary>
    /// A function that opens the ProductListWindow.
    /// </summary>
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow(bl).Show();
        Close();
    }
    /// <summary>
    /// A function that opens the OrderListWindow.
    /// </summary>
    private void ShowOrdersButton_Click(Object sender, RoutedEventArgs e) {
        new PL.Orders.OrderListWindow(bl).Show();
        Close();
    }
    /// <summary>
    /// A function for returning to the mainWindow.
    /// </summary>
    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}
