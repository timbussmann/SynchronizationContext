using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SyncContext.ASP.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            Log($"Calling DoStuffAsync(1) on '{SynchronizationContext.Current}'");
            DoStuffAsync(1).Wait();

            Log($"Calling DoStuffAsync(2) on '{SynchronizationContext.Current}'");
            DoStuffAsync(2).Wait();

            Log($"Return view on '{SynchronizationContext.Current}'");
            return View();
        }

        async Task DoStuffAsync(int id)
        {
            Log($"Starting job {id} on '{SynchronizationContext.Current}'");
            await Task.Delay(1000);
            Log($"Finished job {id} on '{SynchronizationContext.Current}'");
        }

        private static void Log(string log)
        {
            System.Web.HttpContext.Current.Response.Write($"{DateTime.Now.ToString("T")} - {log} (Thread:{Thread.CurrentThread.ManagedThreadId}) <br/>");
        }
    }
}