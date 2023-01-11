using BlApi;

using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace PL.Orders;

//private PO.ProductForList convertBoProdForLstToPoProdForLst(BO.ProductForList bP)
//{
//    PO.ProductForList p = new();
//    p.Name = bP.Name;
//    p.Price = bP.Price;
//    p.Id = bP.Id;
//    p.Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others;
//    return p;
//}


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
            status = (BO.eOrderStatus?)bO.status ?? BO.eOrderStatus.confirmed,
            AmountOfItems = bO.AmountOfItems,
            TotalPrice = bO.TotalPrice 
        };
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

    }
}



