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
    //[RoutePrefix("api")]
    public class ExpenseGroupsController : ApiController
    {
        private readonly IExpenseTrackerRepository _expenseTrackerRepository;

        private readonly ExpenseGroupFactory _expenseGroupFactory = new ExpenseGroupFactory();

        public ExpenseGroupsController()
        {

        }

        public ExpenseGroupsController(IExpenseTrackerRepository expenseTrackerRepository)
        {
            _expenseTrackerRepository = expenseTrackerRepository;
        }

        //[Route("expensegroups")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var expenseGroups = _expenseTrackerRepository.GetExpenseGroups();

                return Ok(expenseGroups.ToList()
                    .Select(eg => _expenseGroupFactory.CreateExpenseGroup(eg)));
                    
            }
            catch (Exception)
            {

                return InternalServerError();
            }
                
        }
    }
}
