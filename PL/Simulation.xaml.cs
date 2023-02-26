using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using PL.PO;
using Simulator;
using BlApi;
namespace PL;

/// <summary>
/// Interaction logic for Simulation.xaml
/// </summary>
public partial class Simulation : Window
{
    private Stopwatch stopWatch;
    BackgroundWorker worker;
    bool stopByUser = false;

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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            Simulator.Simulator.registerChangeEvent(changeOrder);
            Simulator.Simulator.registerStopEvent(finishSimulator);
            Simulator.Simulator.Run();

            while (!worker.CancellationPending)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        catch (InvalidValueException err)
        {
            MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);   
        }
        catch (DataErrorException err)
        {
            MessageBox.Show(err.Message + " " + err?.InnerException?.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err) { MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);   }
    }

    /// <summary>
    /// A function that is called when there is change in the background.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="er"></param>
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs er)
    {
        //Update Order
        if (er.ProgressPercentage==2)
        {
            Tuple<int, BO.eOrderStatus, BO.eOrderStatus, DateTime, int> dataCntxt = (Tuple<int, BO.eOrderStatus, BO.eOrderStatus, DateTime, int>) er.UserState;           
            DataContext = dataCntxt;
            stopWatch.Restart();
        }
        //Update clock and timer
        string clockText = DateTime.Now.ToString();
        ClockTxt.Text = clockText;

        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        this.timerTextBlock.Text = timerText;
    }

    /// <summary>
    /// A function that is called when the background finishes and do the things that need to happen at that time.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.unregisterChangeEvent(changeOrder);
        Simulator.Simulator.unregisterStopEvent(finishSimulator);
        stopWatch.Stop();
        string msg = stopByUser == true ? "Bye Bye" : "There are no orders to update";
        MessageBox.Show(msg);
        Close();
    }

    /// <summary>
    /// A function that is called when there is event of ProgressChange, the function receved the details whose sent to the event
    /// and the function send them to the Worker_ProgressChanged.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void changeOrder(object sender, EventArgs e)
    {
        if (!(e is Details))
            return;
        Details? details = e as Details;
        Tuple<int, BO.eOrderStatus, BO.eOrderStatus, DateTime, int> orderDetails = new(details.id, details.PreviousStatus, details.NextStatus, DateTime.Now, details.EstimatedTime);
        worker.ReportProgress(2, orderDetails);      
    }

   /// <summary>
   /// A function that is called when there is event of StopSimulator.
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    private void finishSimulator(object sender, EventArgs e)
    {
        worker.CancelAsync();
    }

    /// <summary>
    /// A function for finish Simulator that called by user click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void finishSimulator_Click(object sender, RoutedEventArgs e)
    {
        stopByUser = true;
        Simulator.Simulator.Stop();
    }
}