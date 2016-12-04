using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

#pragma warning disable 1998

namespace PopQuiz
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
            Log($"1: {SynchronizationContext.Current}"); // DispatcherSynchronizationContext
            await AsyncOperation1().ConfigureAwait(false);
            Log($"2: {SynchronizationContext.Current}");
            await AsyncOperation2().ConfigureAwait(false);
            Log($"3: {SynchronizationContext.Current}");
            await AsyncOperation3().ConfigureAwait(false);
            Log($"4: {SynchronizationContext.Current}");
        }

        Task AsyncOperation1()
        {
            return Task.CompletedTask;
        }

        async Task AsyncOperation2()
        {
        }

        async Task AsyncOperation3()
        {
            await Task.Delay(0);
        }

        private static void Log(string log)
        {
            Console.WriteLine($"{DateTime.Now.ToString("T")} - {log} (Thread:{Thread.CurrentThread.ManagedThreadId})");
        }
    }
}
