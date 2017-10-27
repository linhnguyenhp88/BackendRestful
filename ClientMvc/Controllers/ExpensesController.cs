using ClientMvc.Helpers;
using LinhNguyen.DTO.Expense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClientMvc.Controllers
{
    public class ExpensesController : Controller
    {
        // GET: Expenses
        public ActionResult Index()
        {
            return View();
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expenses/Create
        public ActionResult Create(int expenseGroupId)
        {
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Expense expense)
        {
            try
            {
                // TODO: Add insert logic here
                var client = ExpenseTrackerHttpClient.GetClient();

                //Serialize and Post
                var serializedItemToCreate = JsonConvert.SerializeObject(expense);
                var response = await client.PostAsync("api/expenses", new StringContent(serializedItemToCreate,
                    System.Text.Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "ExpenseGroups", new { id = expense.ExpenseGroupId });
                }
                else
                {
                    return Content("An error occerred");
                }
            }
            catch(Exception ex)
            {
                return Content("An error occurred");
            }
        }

        // GET: Expenses/Edit/5
        public async Task <ActionResult> Edit(int id)
        {
            var client = ExpenseTrackerHttpClient.GetClient();

            HttpResponseMessage response = await client.GetAsync("api/expenses" + id);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<Expense>(content);
                return View(model);
            }

            return Content("An error occurred: " + content);
        }

        // POST: Expenses/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Expense expense)
        {
            try
            {
                var client = ExpenseTrackerHttpClient.GetClient();
                var serializedItemToUpdate = JsonConvert.SerializeObject(expense);

                HttpResponseMessage response = await client.PutAsync("api/expenses" + id, 
                    new StringContent(serializedItemToUpdate, System.Text.Encoding.Unicode, "application/json"));
                // TODO: Add update logic here

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "ExpenseGroups", new { id = expense.ExpenseGroupId });
                }
                else
                {
                    return Content("An error occurred: ");
                }
            }
            catch
            {
                return Content("An error occurred: ");
            }
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Expenses/Delete/5
        [HttpPost]
        public async Task <ActionResult> Delete(int expenseGroupId, int id)
        {
            try
            {
                // TODO: Add delete logic here
                var client = ExpenseTrackerHttpClient.GetClient();
                var response = await client.DeleteAsync("api/expenses" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "ExpenseGroups", new { id = expenseGroupId });
                }
                else
                {
                    return Content("An error occurred");
                }
            }
            catch
            {
                return Content("An error occurred");
            }
        }
    }
}
