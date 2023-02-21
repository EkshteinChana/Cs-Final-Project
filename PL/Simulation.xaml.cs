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
namespace PL;

/// <summary>
/// Interaction logic for Simulation.xaml
/// </summary>
public partial class Simulation : Window
{
    //private Stopwatch stopWatch;
    //private bool isTimerRun;
    //BackgroundWorker timerworker;
    BackgroundWorker worker;
    public Simulation()
    {
        InitializeComponent();
        Loaded += ToolWindow_Loaded;
        //stopWatch = new Stopwatch();     
        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;

        worker.RunWorkerAsync();
        string clockText = DateTime.Now.ToString();
        ClockTxt.Text = clockText;
        //stopWatch.Restart();
        //timerText = timerText.Substring(0, 8);
    }


    //Hide X Button
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;

    //public event PropertyChangedEventHandler? PropertyChanged;

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

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

        string clockText = DateTime.Now.ToString();
        ClockTxt.Text = clockText;
        //string timerText = stopWatch.Elapsed.ToString();
        //timerText = timerText.Substring(0, 8);
    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.ProgressChange += changeOrder;
        Simulator.Simulator.Run();
        while (true)
        {
            worker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private void changeOrder(object sender, EventArgs e)
    {
        if (!(e is Details))
            return;
        Details details = (Details)e;
        DataContext = details;
    }
    private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

    private void finishSimulator_Click(object sender, RoutedEventArgs e)
    {

    }
}