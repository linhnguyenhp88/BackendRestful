using LinhNguyen.Repository;
using LinhNguyen.Repository.Factories;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SraCRM.Controllers
{
    [RoutePrefix("api")]
    public class ExpensesController : ApiController
    {
        private readonly IExpenseTrackerRepository _expenseRepository;
        private readonly ExpenseFactory _expenseFactory;

        public ExpensesController(IExpenseTrackerRepository expenseRepository, ExpenseFactory expenseFactory)
        {
            _expenseRepository = expenseRepository;
            _expenseFactory = expenseFactory;
        }


        [Route("expensegroups/{expenseGroupId}/expenses")]
        [HttpGet]
        public IHttpActionResult Get(int expenseGroupId)
        {
            try
            {
                var expenses = _expenseRepository.GetExpenses(expenseGroupId);

                if (expenses == null)
                {
                    return NotFound();
                }

                var expenseResult = expenses.ToList().Select(exp => _expenseFactory.CreateExpense(exp));

                return Ok(expenseResult);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        } 


        [Route("expensegroups/{expenseGroupId}/expenses/{id}")]
        [Route("expenses/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id, int? expenseGroupId = null)
        {
            try
            {
                LinhNguyen.Repository.Entities.Expense.Entity.Expense expense = null;

                if (expenseGroupId == null)
                {
                    expense = _expenseRepository.Getexpense(id);
                }
                else
                {
                    var expensesForGroup = _expenseRepository.GetExpenses((int)expenseGroupId);

                    if (expensesForGroup != null)
                    {
                        expense = expensesForGroup.FirstOrDefault(eg => eg.Id == id);
                    }
                }

                if (expense != null)
                {
                    var returnValue = _expenseFactory.CreateExpense(expense);
                    return Ok(returnValue);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
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
