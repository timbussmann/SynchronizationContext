using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncContext.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = DoWorkAsync(1);
            var t2 = DoWorkAsync(2);
            var t3 = DoWorkAsync(3);

            Task.WaitAll(t1, t2, t3);

            Log("Finished all jobs");
            System.Console.ReadKey();
        }

        static async Task DoWorkAsync(int id)
        {
            Log($"Starting job {id}");
            await Task.Delay(2000);
            Log($"Finished job {id}");
        }

        private static void Log(string log)
        {
            System.Console.WriteLine($"{DateTime.Now.ToString("T")} - {log} (Thread:{Thread.CurrentThread.ManagedThreadId})");
        }
    }
}
