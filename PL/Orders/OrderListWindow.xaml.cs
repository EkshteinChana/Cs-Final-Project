using BlApi;

using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private IBl bl;
    
    private ObservableCollection<PO.OrderForList?> currentOrderList { get; set; }//the list of the products 
    private PO.OrderForList convertBoOrdLstToPoOrdLst(BO.OrderForList bO)
    {
        PO.OrderForList po = new()
        {
            Id = bO.Id,
            CustomerName = bO.CustomerName,
            AmountOfItems = bO.AmountOfItems,
            TotalPrice = bO.TotalPrice 
        };
        if(bO.status == BO.eOrderStatus.confirmed) { po.status = PO.eOrderStatus.confirmed; }
        else if(bO.status == BO.eOrderStatus.provided) { po.status = PO.eOrderStatus.provided; }
        else { po.status=PO.eOrderStatus.Sent; }
        return po;
    }

    public OrderListWindow(IBl Ibl)
    {
        InitializeComponent();
        bl = Ibl;
        IEnumerable<BO.OrderForList?> bOrds = bl.Order.ReadOrdsManager();
        currentOrderList = new();
        bOrds?.Select(bO =>
        {
            PO.OrderForList o = convertBoOrdLstToPoOrdLst(bO);
            currentOrderList.Add(o);
            return bO;
        }).ToList();
        OrdersListview.DataContext = currentOrderList;
    }

    public void DisplayAllOrdersBtn_Click(object sender, RoutedEventArgs e)
    {
       //CategorySelector.SelectedItem = null;
    }

    private void OrdersListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        PO.OrderForList O = (PO.OrderForList)((ListView)sender).SelectedItem;
        new OrderWindow(bl, this, O.Id, currentOrderList).Show();
        Hide();
    }

    private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void ReturnBackBtn_Click(object sender, RoutedEventArgs e)
    {
        new PL.AdminWindow(bl).Show();
        Close();
    }
}



