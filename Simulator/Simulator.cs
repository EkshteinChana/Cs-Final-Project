using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BlImplementation;
using BO;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace Simulator;

public static class Simulator
{
    private static BlApi.IBl bl = BlApi.Factory.Get();
    private static bool continuing = true;
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;
    public static event EventHandler StatusChange;
    public static event EventHandler Error;

    /// <summary>
    /// A function that update statuses of orders and call the ProgressChange event for each updating.
    /// </summary>
    private static void ChangeStatuses()
    {
        while (continuing)
        {
            try
            {
                continuing = true;
                int? id = bl.Order.GetOldestOrder();
                if (id == null)
                {
                    Stop();
                    break;
                }
                Order crrntOrder = bl.Order.ReadOrd((int)id);
                Random rnd = new Random();
                int seconds = rnd.Next(3, 9);
                Details details = new Details(crrntOrder.Id, (eOrderStatus)crrntOrder.status, (eOrderStatus)((int)crrntOrder.status + 1), seconds);
                if (ProgressChange != null)
                    ProgressChange(null, details);
                Thread.Sleep(seconds * 1000);
                if (crrntOrder.status == eOrderStatus.confirmed)
                {
                    if (crrntOrder.ShipDate == null || crrntOrder.ShipDate == DateTime.MinValue)
                        bl.Order.UpdateOrdShipping(crrntOrder.Id);
                }
                else
                {
                    if (crrntOrder.DeliveryDate == null || crrntOrder.DeliveryDate == DateTime.MinValue)
                        bl.Order.UpdateOrdDelivery(crrntOrder.Id);
                }
                Num idOrder = new Num(crrntOrder.Id);
                if (StatusChange != null)
                    StatusChange(null, idOrder);
            }
            catch (IllegalActionException err)
            {
                throw err;
            }
            catch (InvalidValueException err)
            {
                throw err;
            }
            catch (DataErrorException dataError)
            {
                throw dataError;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }

    /// <summary>
    /// A function that start the thread.
    /// </summary>
    public static void Run()
    {
        Thread changeStatuses = new Thread(ChangeStatuses);
        changeStatuses.Start();
    }

    /// <summary>
    /// A function that stop the thread and call the StopSimulator event.
    /// </summary>
    public static void Stop()
    {
        continuing = false;
        if (StopSimulator != null)
            StopSimulator(null, EventArgs.Empty);
    }
    /// <summary>
    /// Registration function for StopSimulator event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerStopEvent(EventHandler func)
    {
        StopSimulator += func;
    }
    /// <summary>
    /// Unregistration function for StopSimulator event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterStopEvent(EventHandler func)
    {
        StopSimulator -= func;
    }
    /// <summary>
    /// Registration function for ProgressChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerChangeEvent(EventHandler func)
    {
        ProgressChange += func;
    }
    /// <summary>
    /// Unregistration function for ProgressChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterChangeEvent(EventHandler func)
    {
        ProgressChange -= func;
    }
    /// <summary>
    /// Registration function for StatusChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerChangeStatusEvent(EventHandler func)
    {
        StatusChange += func;
    }
    /// <summary>
    /// Unregistration function for StatusChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterChangeStatusEvent(EventHandler func)
    {
        StatusChange -= func;
    }
}


public class Details : EventArgs
{
    public int id;
    public eOrderStatus PreviousStatus;
    public eOrderStatus NextStatus;
    public int EstimatedTime;
    /// <summary>
    /// constractor of Details Class.
    /// </summary>
    public Details(int i, eOrderStatus PStatus, eOrderStatus NStatus, int Time)
    {
        id = i;
        PreviousStatus = PStatus;
        NextStatus = NStatus;
        EstimatedTime = Time;
    }
}

public class Num : EventArgs
{
    public int id;
    /// <summary>
    /// constractor of Num Class.
    /// </summary>
    public Num(int i)
    {
        id = i;
    }
}