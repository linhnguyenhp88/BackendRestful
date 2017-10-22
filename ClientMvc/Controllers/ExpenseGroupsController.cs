using ClientMvc.Helpers;
using ClientMvc.Models;
using LinhNguyen.DTO.Expense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClientMvc.Controllers
{
    public class ExpenseGroupsController : Controller
    {
        // GET: ExpenseGroups
        public async Task<ActionResult> Index()
        {
            try
            {
                var client = ExpenseTrackerHttpClient.GetClient();

                var model = new ExpenseGroupsViewModel();

                var egsResponse = await client.GetAsync("api/ExpenseGroupStatusses");

                if (egsResponse.IsSuccessStatusCode)
                {
                    string egsContent = await egsResponse.Content.ReadAsStringAsync();
                    var lstExpenseGroupStatusses = JsonConvert
                        .DeserializeObject<IEnumerable<ExpenseGroupStatus>>(egsContent);

                    model.ExpenseGroupStatusses = lstExpenseGroupStatusses;
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
            catch (TaskCanceledException ex)
            {

                return Content($"{ex}");
            }
           

        }

        // GET: ExpenseGroups/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExpenseGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExpenseGroup expenseGroup)
        {
            try
            {
                // TODO: Add insert logic here
                var client = ExpenseTrackerHttpClient.GetClient();

                expenseGroup.ExpenseGroupStatusId = 1;
                expenseGroup.UserId = @"https://expensetrackeridsrv3/embedded_1";

                var serializedItemToCreate = JsonConvert.SerializeObject(expenseGroup);
                var response = await client.PostAsync("api/ExpenseGroup", new StringContent
                    (serializedItemToCreate,System.Text.Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
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

        // GET: ExpenseGroups/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var client = ExpenseTrackerHttpClient.GetClient();

            HttpResponseMessage responseMessage = await client.GetAsync("api/ExpenseGroups/" + id);
            string content = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<ExpenseGroup>(content);
                return View(model);
            }

            return Content($"An error occurred {content}");
        }

        // POST: ExpenseGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ExpenseGroup expenseGroup)
        {
            try
            {
                // TODO: Add update logic here
                var client = ExpenseTrackerHttpClient.GetClient();

                var serializedItemToUpdate = JsonConvert.SerializeObject(expenseGroup);

                var response = await client.PutAsync("api/ExpenseGroup" + id, new
                    StringContent(serializedItemToUpdate, System.Text.Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Content("An error occurred");
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return Content($"An error occurred ");
            }
        }

        // GET: ExpenseGroups/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = ExpenseTrackerHttpClient.GetClient();
            var model = new ExpenseGroupsViewModel();
            HttpResponseMessage responseExpenseGroup = await client.GetAsync("api/ExpenseGroups" + id);

            if (responseExpenseGroup.IsSuccessStatusCode)
            {
                var content = await responseExpenseGroup.Content.ReadAsStringAsync();
                var itemExpensGroup = JsonConvert.DeserializeObject<ExpenseGroup>(content);
                model.ExpenseGroup = itemExpensGroup;
            }
            else
            {
                return Content("An error occurred");
            }


            return View(model);
        }

        // POST: ExpenseGroups/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var client = ExpenseTrackerHttpClient.GetClient();
                var response = await client.DeleteAsync("api/ExpenseGroups" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("An error occerred");
                }
            }
            catch(Exception ex)
            {
                return Content("An error occerred");
            }
        }
    }
}
