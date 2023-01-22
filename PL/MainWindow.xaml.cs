﻿using PL;
using PL.Products;
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

    private void TrackBtn_Click(object sender, RoutedEventArgs e)
    {
        new OrderTrackingWindow(bl).Show();
    }
}
