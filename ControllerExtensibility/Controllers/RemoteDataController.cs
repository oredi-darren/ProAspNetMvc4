using ControllerExtensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers
{
    public class RemoteDataController : AsyncController
    {
        public async Task<ActionResult> Data()
        {
            var data = await Task<string>.Factory.StartNew(() => { return new RemoteService().GetRemoteData(); });
            return View(data as object);
        }

        public async Task<ActionResult> ConsumeAsyncMethod()
        {
            var data = await new RemoteService().GetRemoteDataAsync();
            return View(data as object);
        }

    }
}
