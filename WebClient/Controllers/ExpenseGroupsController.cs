using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebClient.Helpers;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class ExpenseGroupsController : Controller
    {
        // GET: ExpenseGroups
        public async Task<ActionResult> Index()
        {
            var client = ExpenseTrackerHttpClient.GetClient();

            var model = new ExpenseGroupsViewModel();

            var egsReponse = await client.GetAsync("api/expensegroupstatusses");
            return View();
        }
    }
}