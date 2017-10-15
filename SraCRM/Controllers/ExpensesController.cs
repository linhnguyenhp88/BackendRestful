using LinhNguyen.Repository;
using LinhNguyen.Repository.Entities.Expense.Entity;
using LinhNguyen.Repository.Factories;
using Marvin.JsonPatch;
using SraCRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SraCRM.Controllers
{
    [RoutePrefix("api")]
    public class ExpensesController : ApiController
    {
        private readonly IExpenseTrackerRepository _expenseRepository;
        private readonly ExpenseFactory _expenseFactory;
        private const int maxPageSize = 10;

        public ExpensesController(IExpenseTrackerRepository expenseRepository, ExpenseFactory expenseFactory)
        {
            _expenseRepository = expenseRepository;
            _expenseFactory = expenseFactory;
        }


        [Route("expensegroups/{expenseGroupId}/expenses", Name = "ExpensesForGroup")]
        [HttpGet]
        public IHttpActionResult Get(int expenseGroupId, string fields = null, string sort = "date"
            , int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                //var expenses = _expenseRepository.GetExpenses(expenseGroupId);

                //if (expenses == null)
                //{
                //    return NotFound();
                //}

                //var expenseResult = expenses.ToList().Select(exp => _expenseFactory.CreateExpense(exp));

                //return Ok(expenseResult);

                var lstOfFielfs = new List<string>();

                if (fields != null)
                {
                    lstOfFielfs = fields.ToLower().Split(',').ToList();                 
                }
                var expenses = _expenseRepository.GetExpenses(expenseGroupId);

                if (expenses == null)
                {
                    return NotFound();
                }

                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                var totalCount = expenses.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHepler = new UrlHelper(Request);

                var preLink = page > 1 ? urlHepler.Link("ExpensesForGroup",
                    new { page = page - 1, pageSize = pageSize, expenseGroupId = expenseGroupId, fields = fields, sort = sort }) : "";
                var nextLink = page < totalPages ? urlHepler.Link("ExpensesForGroup",
                    new { page = page + 1, pageSize = pageSize, expenseGroupId = expenseGroupId, fields = fields, sort }) : "";

                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previosPageLink = preLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

                var expensesResult = expenses.ApplySort(sort)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(exp => _expenseFactory.CreateDataShapedObject(exp, lstOfFielfs));

                return Ok(expensesResult);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        } 


        [VersionedRoute("expensegroups/{expenseGroupId}/expenses/{id}",1)]
        [VersionedRoute("expenses/{id}",1)]
        [HttpGet]
        public IHttpActionResult Get(int id, int? expenseGroupId = null, string fields = null)
        {
            try
            {              
                var lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }
                Expense expense = null;

                if (expenseGroupId == null)
                {
                    expense = _expenseRepository.Getexpense(id);
                }
                else
                {
                    var expensesForGroup = _expenseRepository.GetExpenses((int)expenseGroupId);

                    if (expensesForGroup != null)
                    {
                        expense = expensesForGroup.Where(eg => eg.Id == id).FirstOrDefault();
                    }
                }

                if (expense != null)
                {
                    var returnValue = _expenseFactory.CreateDataShapedObject(expense, lstOfFields);

                    return Ok(returnValue);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [VersionedRoute("expensegroups/{expenseGroupId}/expenses/{id}", 2)]
        [VersionedRoute("expenses/{id}", 2)]
        [HttpGet]
        public IHttpActionResult GetV2(int id, int? expenseGroupId = null, string fields = null)
        {
            try
            {
                var lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }
                Expense expense = null;

                if (expenseGroupId == null)
                {
                    expense = _expenseRepository.Getexpense(id);
                }
                else
                {
                    var expensesForGroup = _expenseRepository.GetExpenses((int)expenseGroupId);

                    if (expensesForGroup != null)
                    {
                        expense = expensesForGroup.Where(eg => eg.Id == id).FirstOrDefault();
                    }
                }

                if (expense != null)
                {
                    var returnValue = _expenseFactory.CreateDataShapedObject(expense, lstOfFields);

                    return Ok(returnValue);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [Route("expenses")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]LinhNguyen.DTO.Expense.Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest();
                }

                // map
                var exp = _expenseFactory.CreateExpense(expense);

                var result = _expenseRepository.InsertExpense(exp);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto
                    var newExp = _expenseFactory.CreateExpense(result.Entity);
                    return Created<LinhNguyen.DTO.Expense.Expense>(Request.RequestUri + "/" + newExp.Id.ToString(), newExp);
                }

                return BadRequest();

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

       
        [Route("expenses/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]LinhNguyen.DTO.Expense.Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest();
                }

                // map
                var exp = _expenseFactory.CreateExpense(expense);

                var result = _expenseRepository.UpdateExpense(exp);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var updatedExpense = _expenseFactory.CreateExpense(result.Entity);
                    return Ok(updatedExpense);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("expenses/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _expenseRepository.DeleteExpense(id);
                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if(result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [Route("expenses/{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]JsonPatchDocument<LinhNguyen.DTO.Expense.Expense> expensePatchDocument)
        {
            try
            {
                if (expensePatchDocument == null)
                {
                    return BadRequest();
                }

                var expense = _expenseRepository.Getexpense(id);

                if (expense == null)
                {
                    return NotFound();
                }

                var exp = _expenseFactory.CreateExpense(expense);

                expensePatchDocument.ApplyTo(exp);

                var result = _expenseRepository.UpdateExpense(_expenseFactory.CreateExpense(exp));

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updateExpense = _expenseFactory.CreateExpense(result.Entity);

                    return Ok(updateExpense);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }
    }
}
