using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using Simulator;
using BlApi;
using System.Windows.Interop;

namespace PL;

/// <summary>
/// Interaction logic for Simulation.xaml
/// </summary>
public partial class Simulation : Window
{
    private Stopwatch stopWatch;
    BackgroundWorker worker;
    bool stopByUser = false;
    bool stopByError = false;

    /// <summary>
    /// constractor of Simulation Window.
    /// </summary>
    public Simulation()
    {
        InitializeComponent();
        Loaded += ToolWindow_Loaded;
        stopWatch = new Stopwatch();
        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();

        string clockText = DateTime.Now.ToString();
        ClockTxt.Text = clockText;

        stopWatch.Restart();
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        this.timerTextBlock.Text = timerText;
    }


    //Hide X Button
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    void ToolWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Code to remove close box from window
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    /// <summary>
    /// A function that is called when the background starts and do the things that need to happen at that time.
    /// </summary>
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            Simulator.Simulator.registerChangeEvent(changeOrder);
            Simulator.Simulator.registerStopEvent(finishSimulator);
            Simulator.Simulator.registerErrorEvent(errorCalled);
            Simulator.Simulator.Run();

            while (!worker.CancellationPending)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        catch (IllegalActionException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            stopByError = true;
            Simulator.Simulator.Stop();
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            stopByError = true;
            Simulator.Simulator.Stop();
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            stopByError = true;
            Simulator.Simulator.Stop();
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            stopByError = true;
            Simulator.Simulator.Stop();
        }
    }

    /// <summary>
    /// A function that is called when there is change in the background.
    /// </summary>
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs er)
    {
        if (er.ProgressPercentage == 3)//error 
        {
            Exception? err = er.UserState as Exception;
            if (err != null)
            {
                stopByError = true;
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Simulator.Simulator.Stop();
            }
        }
        //Update Order
        else
        {
            if (er.ProgressPercentage == 2)
            {
                Tuple<int, BO.eOrderStatus, BO.eOrderStatus, DateTime, int> dataCntxt = (Tuple<int, BO.eOrderStatus, BO.eOrderStatus, DateTime, int>)er.UserState;
                DataContext = dataCntxt;
                stopWatch.Restart();
            }
            //Update clock and timer
            string clockText = DateTime.Now.ToString();
            ClockTxt.Text = clockText;

            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            timerTextBlock.Text = timerText;
        }
    }

    /// <summary>
    /// A function that is called when the background finishes and do the things that need to happen at that time.
    /// </summary>
    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.unregisterChangeEvent(changeOrder);
        Simulator.Simulator.unregisterStopEvent(finishSimulator);
        Simulator.Simulator.unregisterErrorEvent(errorCalled);
        stopWatch.Stop();
        string msg = stopByUser == true ? "Bye Bye" : stopByError == true ? "Stop By Error" : "There are no orders to update";
        MessageBox.Show(msg, "Simulator", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }

    /// <summary>
    /// A function that is called when there is event of ProgressChange, the function receved the details whose sent to the event
    /// and the function send them to the Worker_ProgressChanged.
    /// </summary>
    private void changeOrder(object sender, EventArgs e)
    {
        if (!(e is Details))
            return;
        Details? details = e as Details;
        Tuple<int, BO.eOrderStatus, BO.eOrderStatus, DateTime, int> orderDetails = new(details.id, details.PreviousStatus, details.NextStatus, DateTime.Now, details.EstimatedTime);
        worker.ReportProgress(2, orderDetails);
    }

    /// <summary>
    /// A function that is called when there is event of Error
    /// </summary>
    private void errorCalled(object sender, EventArgs e)
    {
        if (!(e is MyException))
            return;
        MyException? myExc = e as MyException;
        worker.ReportProgress(3, myExc.exc);
    }

    /// <summary>
    /// A function that is called when there is event of StopSimulator.
    /// </summary>
    private void finishSimulator(object sender, EventArgs e)
    {
        worker.CancelAsync();
    }

    /// <summary>
    /// A function for finish Simulator that called by user click.
    /// </summary>
    private void finishSimulator_Click(object sender, RoutedEventArgs e)
    {
        stopByUser = true;
        Simulator.Simulator.Stop();
    }
}