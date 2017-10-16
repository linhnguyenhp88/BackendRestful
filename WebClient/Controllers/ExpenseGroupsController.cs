using LinhNguyen.DTO.Expense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            HttpResponseMessage egsResponse = await client.GetAsync("api/ExpenseGroupStatusses");

            if (egsResponse.IsSuccessStatusCode)
            {
                string egsContent = await egsResponse.Content.ReadAsStringAsync();
                var lstExpenseGroupStatusses = JsonConvert
                    .DeserializeObject<IEnumerable<ExpenseGroupStatus>>(egsContent);

                model.ExpenseGroupStatuses = lstExpenseGroupStatusses;
            }
            else
            {
                return Content("An error occurred.");
            }

            HttpResponseMessage response = await client.GetAsync("api/ExpenseGroups");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var lstExpenseGroups = JsonConvert.DeserializeObject<IEnumerable<ExpenseGroup>>(content);

                model.ExpenseGroups = lstExpenseGroups;

            }
            else
            {
                return Content("An error occurred.");
            }
            return View(model);
        }
    }
}