﻿using System;
using System.Windows;


namespace PL;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BlApi.IBl bl;
    public AdminWindow(BlApi.IBl Ibl)
    {
        InitializeComponent();
        this.bl = Ibl;
    }

    /// <summary>
    /// A function that opens the ProductListWindow.
    /// </summary>
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow(bl).Show();
        Close();
    }
    private void ShowOrdersButton_Click(Object sender, RoutedEventArgs e) {
        new PL.Orders.OrderListWindow(bl).Show();
        this.Close();
    }
}