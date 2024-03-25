using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _02_TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer? _timer = null;
        private string Path;
        private Process? process = null;
        public MainWindow()
        {
            InitializeComponent();
            grid.ItemsSource = Process.GetProcesses();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            grid.ItemsSource = Process.GetProcesses();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            grid.ItemsSource = Process.GetProcesses();
        }
        private void Refresh1(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
        private void Refresh2(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timer.Interval = new TimeSpan(0, 0, 2);
            _timer.Start();
        }
        private void Refresh5(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timer.Interval = new TimeSpan(0, 0, 5);
            _timer.Start();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            Path = line.Text;
            Process.Start(Path);
        }
        private void End(object sender, RoutedEventArgs e)
        {
            process.Kill();
        }


        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel = (grid.SelectedItem as Process);
            if (sel == null) return;
            var pr = Process.GetProcessById(sel.Id);
            process = pr;
            string message = $"Process ID: {pr.Id}\n" +
                 $"Process Name: {pr.ProcessName}\n" +
                 $"Main Window Title: {pr.MainWindowTitle}\n" +
                 $"Start Time: {pr.StartTime}\n" +
                 $"Priority Class: {pr.PriorityClass}\n" +
                 $"Base Priority: {pr.BasePriority}\n" +
                 $"Responding: {pr.Responding}\n" +
                 $"Virtual Memory Size: {pr.VirtualMemorySize64}\n" +
                 $"Physical Memory Usage: {pr.WorkingSet64}\n" +
                 $"Total Processor Time: {pr.TotalProcessorTime}\n" +
                 $"Threads Count: {pr.Threads.Count}";
            MessageBox.Show(message);
        }
    }
}
