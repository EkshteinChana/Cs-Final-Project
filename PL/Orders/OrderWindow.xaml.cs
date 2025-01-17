﻿using BlApi;
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
    Tuple<bool, bool, PO.Order> data;
    bool adminUse = false;
    bool customerChange = false;
    ObservableCollection<PO.eOrderStatus> Statusoptions;

    /// <summary>
    /// constractor of OrderWindow for watching and updating a order.
    /// </summary>
    public OrderWindow(IBl Ibl, Window w, int id, ObservableCollection<PO.OrderForList?> cl = null)
    {
        try
        {
            InitializeComponent();
            adminUse = cl != null;
            bl = Ibl;
            currentOrderList = cl;
            sourcWindow = w;
            if (id > -1)
            {
                BO.Order bo = bl.Order.ReadOrd(id);
                po = convertBoOrdToPoOrd(bo);
                Statusoptions = new();
                Statusoptions.Add(PO.eOrderStatus.provided);
                if (po.DeliveryDate == DateTime.MinValue)
                {
                    Statusoptions.Add(PO.eOrderStatus.Sent);
                }
                if (po.ShipDate == DateTime.MinValue)
                {
                    Statusoptions.Add(PO.eOrderStatus.confirmed);
                    customerChange = cl == null;
                }
                data = new Tuple<bool, bool, PO.Order>(adminUse, customerChange, po);
                orderDetails.DataContext = data;
                ItemsList.DataContext = po.Items;
                StatusSelector.ItemsSource = Statusoptions;
                Simulator.Simulator.registerChangeStatusEvent(refreshStatus);
            }
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    //-------------------if the admin enter 

    /// <summary>
    /// a function that updates the dates that regards to the order
    /// </summary>
    private void UpdateOrderStatusBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if ((PO.eOrderStatus)StatusSelector.SelectedItem == po.status && currentOrderList != null)
            {
                MessageBox.Show("No changes have been entered", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (po.status < PO.eOrderStatus.Sent)
            {
                bl.Order.UpdateOrdShipping(po.Id);
            }
            if ((PO.eOrderStatus)StatusSelector.SelectedItem == PO.eOrderStatus.provided)
            {
                bl.Order.UpdateOrdDelivery(po.Id);
            }
            updateCrrnOrdLst();
            MessageBox.Show("The update was successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            sourcWindow.Show();
            Simulator.Simulator.unregisterChangeStatusEvent(refreshStatus);
            Close();
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (IllegalActionException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    //------------------- if the customer update the items list

    /// <summary>
    /// a function for deleting an item from the order.
    /// </summary>
    private void DeletItmBtn_Click(object sender, RoutedEventArgs e)     
    {
        PO.OrderItem CurrntOitm = (PO.OrderItem)((Button)sender).DataContext;
        if (MessageBox.Show($"Are you sure you want to delete the item {CurrntOitm.Name} from the order?",
                                $"Delete {CurrntOitm.Name}",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            try
            {
                bl.Order.UpdateOrd(po.Id, CurrntOitm.ProductId, 0 , BO.eUpdateOrder.delete);
                BO.Order bo = bl.Order.ReadOrd(po.Id);
                po = convertBoOrdToPoOrd(bo);
            }
            catch (InvalidValueException err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IllegalActionException err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DataErrorException err)
            {
                MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    /// <summary>
    /// a function for updating the amount of an item in the order.
    /// </summary>
    private void updatItmBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem CurrntOitm = (PO.OrderItem)((Button)sender).DataContext;
        try
        {           
            if (CurrntOitm.AmountUpdated < 0) //check if the input is correct.
                throw new InValidInputTypeException("amount of item");
            if (MessageBox.Show($"Are you sure you want to update the amount of {CurrntOitm.Name}?",
                                    $"update amount of {CurrntOitm.Name}",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (CurrntOitm.AmountUpdated != CurrntOitm.Amount)
                {
                    if (CurrntOitm.AmountUpdated == 0)
                    {
                        bl.Order.UpdateOrd(po.Id, CurrntOitm.ProductId, 0, BO.eUpdateOrder.delete);
                    }
                    else
                    {
                        bl.Order.UpdateOrd(po.Id, CurrntOitm.ProductId, CurrntOitm.AmountUpdated, BO.eUpdateOrder.changeAmount);
                    }
                    BO.Order bo = bl.Order.ReadOrd(po.Id);
                    po = convertBoOrdToPoOrd(bo);                  
                }
                CurrntOitm.AmountUpdated = 0;
            }
        }
        catch (InValidInputTypeException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (IllegalActionException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// a function for adding an item to the order.
    /// </summary>
    private void AddOrdItmBtn_Click(object sender, RoutedEventArgs e)
    {
        new PL.Products.ProductCatalogWindow(bl, null, this, po.Id, UpdateSpecificOrder).Show();
        Hide();
    }
    /// <summary>
    /// A private help function for updating the current order.
    /// </summary>
    private void UpdateSpecificOrder()
    {
        try
        {
            BO.Order bo = bl.Order.ReadOrd(po.Id);
            po = convertBoOrdToPoOrd(bo);          
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); ;
        }
    }
    /// <summary>
    /// A function for returning to the source Window.
    /// </summary>
    private void ReturnBackBtn_Click(object sender, RoutedEventArgs e)
    {
        sourcWindow.Show();
        Simulator.Simulator.unregisterChangeStatusEvent(refreshStatus);
        Close();
    }

    /// <summary>
    /// A private function that update the order in the screan if its status changed in the simulator. 
    /// </summary>
    private void refreshStatus(object sender, EventArgs e)
    {
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(refreshStatus, sender, e);
        }
        else
        {
            int? oId = (e as Simulator.Num)?.id ?? null;
            if (oId == po.Id)
            {
                try
                {
                    po = convertBoOrdToPoOrd(bl.Order.ReadOrd(po.Id));
                    Statusoptions.Clear();
                    Statusoptions.Add(PO.eOrderStatus.provided);
                    if (po.DeliveryDate == DateTime.MinValue)
                    {
                        Statusoptions.Add(PO.eOrderStatus.Sent);
                    }
                    customerChange = false;
                    data = new Tuple<bool, bool, PO.Order>(adminUse, customerChange, po);          
                }
                catch (InvalidValueException err)
                {
                    MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (DataErrorException err)
                {
                    MessageBox.Show(err.Message + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    /// <summary>
    /// A private help function to convert BO.Order entity to PO.Order entity.
    /// </summary>
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
        po.status = (PO.eOrderStatus)bo.status;
        po.Items.Clear();
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
    /// <summary>
    /// A private help function to convert BO.OrderForList entity to PO.OrderForList entity.
    /// </summary>
    private PO.OrderForList convertBoOrdLstToPoOrdLst(BO.OrderForList bO)
    {
        PO.OrderForList po = new()
        {
            Id = bO.Id,
            CustomerName = bO.CustomerName,
            AmountOfItems = bO.AmountOfItems,
            TotalPrice = bO.TotalPrice
        };
        po.status = (PO.eOrderStatus)bO.status;
        return po;
    }
    /// <summary>
    /// A private help function for updating the current order list.
    /// </summary>
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
}





