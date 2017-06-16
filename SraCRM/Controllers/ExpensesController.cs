using LinhNguyen.Repository;
using LinhNguyen.Repository.Factories;
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
    }
}
