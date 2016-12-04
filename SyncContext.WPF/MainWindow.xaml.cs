using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
// ReSharper disable ArrangeTypeMemberModifiers

namespace SyncContext.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var t1 = DoWorkAsync(1);
            var t2 = DoWorkAsync(2);
            var t3 = DoWorkAsync(3);

            Task.WaitAll(t1, t2, t3);

            Log("Finished all jobs");
        }

        static async Task DoWorkAsync(int id)
        {
            Log($"Starting job {id}");
            await Task.Delay(2000);
            Log($"Finished job {id}");
        }


        #region ConfigureAwait


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Log($"Start button click handler on '{SynchronizationContext.Current}'");
            DoMoreWorkAsync().Wait();
            Log($"End button click handler on '{SynchronizationContext.Current}'");
        }

        static async Task DoMoreWorkAsync()
        {
            Log($"Starting job on '{SynchronizationContext.Current}'");
            await Task.Delay(1000);
            Log($"Finished job on '{SynchronizationContext.Current}'");
        }

        #endregion

        private static void Log(string log)
        {
            Console.WriteLine($"{DateTime.Now.ToString("T")} - {log} (Thread:{Thread.CurrentThread.ManagedThreadId})");
        }
    }
}
