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

namespace PL;

/// <summary>
/// Interaction logic for Simulation.xaml
/// </summary>
public partial class Simulation : Window
{
    BlApi.IBl bl;
    private Stopwatch stopWatch;
    //private bool isTimerRun;
    BackgroundWorker timerworker;

    BackgroundWorker worker;
    public Simulation(BlApi.IBl Ibl)
    {
        bl=Ibl;

        InitializeComponent();
        Loaded += ToolWindow_Loaded;
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();
        timerworker.DoWork += Worker_DoWork;
        timerworker.ProgressChanged += Worker_ProgressChanged;
        timerworker.WorkerReportsProgress = true;




        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;


        stopWatch.Restart();
        timerworker.RunWorkerAsync();
        string timerText = DateTime.Now.ToString();
        //timerText = timerText.Substring(0, 8);
        ClockTxt.Text = timerText;
        this.bl = bl;
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
        string timerText = DateTime.Now.ToString();
        //string timerText = stopWatch.Elapsed.ToString();
        //timerText = timerText.Substring(0, 8);
        ClockTxt.Text = timerText;
    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        while (true)
        {
            timerworker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
}