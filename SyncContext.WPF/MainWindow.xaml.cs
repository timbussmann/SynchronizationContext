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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var t1 = DoWorkAsync(1);
            var t2 = DoWorkAsync(2);
            var t3 = DoWorkAsync(3);

            await Task.WhenAll(t1, t2, t3);

            Log("Finished all jobs");
        }

        static async Task DoWorkAsync(int id)
        {
            Log($"Starting job {id}");
            await Task.Delay(2000);
            Log($"Finished job {id}");
        }


        #region ConfigureAwait


        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            Log($"Start button click handler on '{SynchronizationContext.Current}'"); // DispatcherSynchronzitationContext
            await DoMoreWorkAsync();
            Log($"End button click handler on '{SynchronizationContext.Current}'"); // DispatcherSynchronzationContext again
        }

        static async Task DoMoreWorkAsync()
        {
            Log($"Starting job on '{SynchronizationContext.Current}'"); // DispatcherSynchronizationContext
            await Task.Delay(1000).ConfigureAwait(false);
            Log($"Finished job on '{SynchronizationContext.Current}'"); // null
        }

        #endregion

        private static void Log(string log)
        {
            Console.WriteLine($"{DateTime.Now.ToString("T")} - {log} (Thread:{Thread.CurrentThread.ManagedThreadId})");
        }
    }
}
