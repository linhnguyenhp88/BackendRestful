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
    public class ExpenseGroupStatussesController : ApiController
    {
        private readonly IExpenseTrackerRepository _expenseRepository;
        private readonly ExpenseMasterDataFactory _expenseMasterDataFactory;

        public ExpenseGroupStatussesController(IExpenseTrackerRepository expenseRepository, ExpenseMasterDataFactory expenseMasterDataFactory)
        {
            _expenseRepository = expenseRepository;
            _expenseMasterDataFactory = expenseMasterDataFactory;
        }

        [Route("ExpenseGroupStatusses")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var expenseGroupStatusses = _expenseRepository.GetExpenseGroupStatuses().ToList()
                    .Select(egs => _expenseMasterDataFactory.CreateExpenseGroupStatus(egs));

                return Ok(expenseGroupStatusses);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
