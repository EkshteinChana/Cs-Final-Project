using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    IBl? bl;
    ObservableCollection<PO.OrderForList?>? currentOrderList;
    Window sourcWindow;
    PO.Order po;
    private PO.Order convertBoOrdToPoOrd(BO.Order bo)
    {
        if (bo.DeliveryDate == null) { bo.DeliveryDate = DateTime.MinValue; }
        if (bo.ShipDate == null) { bo.ShipDate = DateTime.MinValue; }
        if (bo.OrderDate == null) { bo.OrderDate = DateTime.MinValue; }
        PO.Order po = new()
        {
            Id = bo.Id,
            CustomerAddress = bo.CustomerAddress,
            CustomerEmail = bo.CustomerEmail,
            CustomerName = bo.CustomerName,
            DeliveryDate = bo.DeliveryDate,
            OrderDate = bo.OrderDate,
            ShipDate = bo.ShipDate,
            TotalPrice = bo.TotalPrice
        };
        if (bo.status == BO.eOrderStatus.confirmed) { po.status = PO.eOrderStatus.confirmed; }
        else if (bo.status == BO.eOrderStatus.provided) { po.status = PO.eOrderStatus.provided; }
        else { po.status = PO.eOrderStatus.Sent; }
        bo.Items.Select(boi =>
        {
            PO.OrderItem poi = new()
            {
                Id = boi.Id,
                ProductId = boi.ProductId,
                Name = boi.Name,
                Price = boi.Price,
                Amount = boi.Amount,
                TotalPrice = boi.TotalPrice
            };
            po.Items.Add(poi);
            return boi;
        }).ToList();
        return po;
    }
    private PO.OrderForList convertBoOrdLstToPoOrdLst(BO.OrderForList bO)
    {
        PO.OrderForList po = new()
        {
            Id = bO.Id,
            CustomerName = bO.CustomerName,
            AmountOfItems = bO.AmountOfItems,
            TotalPrice = bO.TotalPrice
        };
        if (bO.status == BO.eOrderStatus.confirmed) { po.status = PO.eOrderStatus.confirmed; }
        else if (bO.status == BO.eOrderStatus.provided) { po.status = PO.eOrderStatus.provided; }
        else { po.status = PO.eOrderStatus.Sent; }
        return po;
    }
    private void updateCrrnOrdLst()
    {
        currentOrderList.Clear();
        IEnumerable<BO.OrderForList?> bOrds = bl.Order.ReadOrdsManager();
        bOrds?.Select(bO =>
        {
            PO.OrderForList o = convertBoOrdLstToPoOrdLst(bO);
            currentOrderList.Add(o);
            return bO;
        }).ToList();
    }
    
    public OrderWindow(IBl Ibl, Window w, int id, ObservableCollection<PO.OrderForList?> cl=null)
    {
        try
        {
            InitializeComponent();
            bl = Ibl;
            currentOrderList = cl ;
            sourcWindow = w;
            if (id > -1)
            {
                BO.Order bo = bl.Order.ReadOrd(id);
                po = convertBoOrdToPoOrd(bo);
                orderDetails.DataContext = po;
                List<PO.eOrderStatus> Statusoptions = new();
                Statusoptions.Add(PO.eOrderStatus.provided);
                if (po.DeliveryDate == DateTime.MinValue)
                {
                    Statusoptions.Add(PO.eOrderStatus.Sent);
                }
                if(po.ShipDate == DateTime.MinValue)
                {
                    Statusoptions.Add(PO.eOrderStatus.confirmed);
                }
                ItemsList.DataContext = po.Items;
                StatusSelector.ItemsSource = Statusoptions;
            }
        }
        catch (Exception e){
           MessageBox.Show(e.Message);
        }
    }


    private void UpdateOrdeBtn_Click(object sender, RoutedEventArgs e)
    {
        if ((PO.eOrderStatus)StatusSelector.SelectedItem == po.status)
        {
            MessageBox.Show("No changes have been entered");
            return;
        }
        try
        {
            if (po.status < PO.eOrderStatus.Sent)
            {
                bl.Order.UpdateOrdShipping(po.Id);
            }
            if ((PO.eOrderStatus)StatusSelector.SelectedItem == PO.eOrderStatus.provided)
            {
                bl.Order.UpdateOrdDelivery(po.Id);
            }
            updateCrrnOrdLst();
            MessageBox.Show("The update was successful ✔");           
            sourcWindow.Show();
            Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message+" ❌");
        }
    }

    private void ReturnBackBtn_Click(object sender, RoutedEventArgs e)
    {
        sourcWindow.Show(); 
        Close();    
    }

    private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       

    }
}
