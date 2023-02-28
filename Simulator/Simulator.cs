using BlApi;
using BO;

namespace Simulator;

public static class Simulator
{
    private static BlApi.IBl bl = BlApi.Factory.Get();
    private static bool continuing = true;
    private static bool alreadyStart = false; 
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;
    public static event EventHandler StatusChange;
    public static event EventHandler ErrorEvent;

    /// <summary>
    /// A function that update statuses of orders and call the ProgressChange event for each updating.
    /// </summary>
    private static void ChangeStatuses()
    {
        continuing = true;
        while (continuing)
        {
            try
            {
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
                    bl.Order.UpdateOrdShipping(crrntOrder.Id);
                }
                else
                {
                    bl.Order.UpdateOrdDelivery(crrntOrder.Id);
                }
                Num idOrder = new Num(crrntOrder.Id);
                if (StatusChange != null)
                    StatusChange(null, idOrder);
            }
            catch (IllegalActionException err)
            {
            }
            catch (InvalidValueException err)
            {
                MyException excp = new MyException(err);
                if (ErrorEvent != null)
                    ErrorEvent(null, excp);
            }
            catch (DataErrorException err)
            {
                MyException excp = new MyException(err);
                if (ErrorEvent != null)
                    ErrorEvent(null, excp);
            }
            catch (Exception err)
            {
                MyException excp = new MyException(err);
                if (ErrorEvent != null)
                    ErrorEvent(null, excp);
            }
        }
    }

    /// <summary>
    /// A function that start the thread.
    /// </summary>
    public static void Run()
    {
        try { 
        Thread changeStatuses = new Thread(ChangeStatuses);
        changeStatuses.Start();
            alreadyStart= true;
        }
        catch (Exception err)
        {
            MyException excp = new(err);                  
            if (ErrorEvent != null)
                ErrorEvent(null, excp);
        }
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
    public static void registerStopEvent(EventHandler func)
    {
        StopSimulator += func;
    }
    /// <summary>
    /// Unregistration function for StopSimulator event.
    /// </summary>
    public static void unregisterStopEvent(EventHandler func)
    {
        StopSimulator -= func;
    }
    /// <summary>
    /// Registration function for ProgressChange event.
    /// </summary>
    public static void registerChangeEvent(EventHandler func)
    {
        ProgressChange += func;
    }
    /// <summary>
    /// Unregistration function for ProgressChange event.
    /// </summary>
    public static void unregisterChangeEvent(EventHandler func)
    {
        ProgressChange -= func;
    }
    /// <summary>
    /// Registration function for StatusChange event.
    /// </summary>
    public static void registerChangeStatusEvent(EventHandler func)
    {
        StatusChange += func;
    }
    /// <summary>
    /// Unregistration function for StatusChange event.
    /// </summary>
    public static void unregisterChangeStatusEvent(EventHandler func)
    {
        StatusChange -= func;
    }
    /// <summary>
    /// Registration function for StatusChange event.
    /// </summary>
    public static void registerErrorEvent(EventHandler func)
    {
        ErrorEvent += func;
    }
    /// <summary>
    /// Unregistration function for StatusChange event.
    /// </summary>
    public static void unregisterErrorEvent(EventHandler func)
    {
        ErrorEvent -= func;
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
public class MyException : EventArgs
{
    public Exception exc;
    /// <summary>
    /// constractor of Num Class.
    /// </summary>
    public MyException(Exception err)
    {
        exc = err;
    }
}