using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SyncContext.ASP.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ViewResult> Index()
        {
            Log($"Calling DoStuffAsync(1) on '{SynchronizationContext.Current}'");
            await DoStuffAsync(1);

            Log($"Calling DoStuffAsync(2) on '{SynchronizationContext.Current}'");
            await DoStuffAsync(2);

            Log($"Return view on '{SynchronizationContext.Current}'");
            return View();
        }

        async Task DoStuffAsync(int id)
        {
            Log($"Starting job {id} on '{SynchronizationContext.Current}'");
            await Task.Delay(1000).ConfigureAwait(false);
            Log($"Finished job {id} on '{SynchronizationContext.Current}'");
        }

        private void Log(string log)
        {
            HttpContext.Response.Write($"{DateTime.Now.ToString("T")} - {log} (Thread:{Thread.CurrentThread.ManagedThreadId}) <br/>");
        }
    }
}