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

namespace Simulator;

public static class Simulator
{
    private static BlApi.IBl bl = BlApi.Factory.Get();
    private static bool continuing = true;
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;

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
                }
                Order crrntOrder = bl.Order.ReadOrd((int)id);
                Random rnd = new Random();
                int seconds = rnd.Next(1, 6);
                Details details = new Details(crrntOrder.Id, (eOrderStatus)crrntOrder.status, (eOrderStatus)((int)crrntOrder.status + 1), seconds);
                if (ProgressChange != null)
                    ProgressChange(null, details);
                Thread.Sleep(seconds * 1000);
                if (crrntOrder.status == eOrderStatus.confirmed)
                {
                    bl.Order.UpdateOrdShipping(crrntOrder.Id);
                }
                else
                {
                    bl.Order.UpdateOrdDelivery(crrntOrder.Id);
                }
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
    public static void Run()
    {
        Thread changeStatuses = new Thread(ChangeStatuses);
        changeStatuses.Start();
    }
    public static void Stop()
    {
        continuing = false;
        if(StopSimulator!=null) 
            StopSimulator(null,EventArgs.Empty);
    }


}


public class Details : EventArgs
{
    public int id;
    public eOrderStatus PreviousStatus;
    public eOrderStatus NextStatus;
    public int EstimatedTime;
    public Details(int i, eOrderStatus PStatus, eOrderStatus NStatus, int Time)
    {
        id = i;
        PreviousStatus = PStatus;
        NextStatus = NStatus;        
        EstimatedTime = Time;
    }
}