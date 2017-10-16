using ClientMvc.Helpers;
using ClientMvc.Models;
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
    public class ExpenseGroupsController : Controller
    {
        // GET: ExpenseGroups
        public async Task<ActionResult> Index()
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseGroups/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExpenseGroups/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseGroups/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExpenseGroups/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
