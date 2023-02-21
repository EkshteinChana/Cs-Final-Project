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
    //private static Order crrntOrder;
    private static bool continuing = true;
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;


    private static void ChangeStatuses()
    {
        while (continuing)
        {
            try
            {
                int? id = bl.Order.GetOldestOrder();
                if (id == null)
                {
                    throw new Exception("No orders to update");
                }
                Order crrntOrder = bl.Order.ReadOrd((int)id);
                Random rnd = new Random();
                int seconds = rnd.Next(1, 6);
                Details details = new Details(crrntOrder,seconds);
                //if(ProgressChange!=null)
                   // ProgressChange(null,details);
                Thread.Sleep(seconds*1000);
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
        Thread changeStatuses=new Thread(ChangeStatuses);
        changeStatuses.Start();      
    }
    public static void Stop()
    {
        continuing = false;

    }


}


public class Details : EventArgs
{
    Order order;
    int seconds;
    public Details(Order ord,int sec)
    {
        order = ord;
        seconds = sec;
    }
}
//while (continuing)
//    new Thread(() =>
//    {
//        try
//        {
//            int? id = bl.Order.GetOldestOrder();
//            if (id == null)
//            {
//                throw new Exception("No orders to update");
//            }
//            currentOrder = bl.Order.ReadOrd((int)id);
//            Random rnd = new Random();
//            int seconds = rnd.Next(1000, 5000);
//            Thread.Sleep(seconds);
//            if (currentOrder.status == eOrderStatus.confirmed)
//            {
//                bl.Order.UpdateOrdShipping(currentOrder.Id);
//            }
//            else
//            {
//                bl.Order.UpdateOrdDelivery(currentOrder.Id);
//            }
//        }
//        catch (InvalidValueException err)
//        {
//            throw err;
//        }
//        catch (DataErrorException dataError)
//        {
//            throw dataError;
//        }
//        catch (Exception err)
//        {
//            throw err;
//        }
//    }).Start();