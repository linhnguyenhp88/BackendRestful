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
    public class ExpenseGroupsController : ApiController
    {
        private readonly IExpenseTrackerRepository _expenseTrackerRepository;

        private readonly ExpenseGroupFactory _expenseGroupFactory = new ExpenseGroupFactory();

        //public ExpenseGroupsController()
        //{
        //    _expenseTrackerRepository = new ExpenseTrackerEFRepository(new LinhNguyen.Repository.DAL.SraContext());
        //}

        public ExpenseGroupsController(IExpenseTrackerRepository expenseTrackerRepository)
        {
            _expenseTrackerRepository = expenseTrackerRepository;
        }

        [Route("ExpenseGroups")]
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

        [Route("ExpenseGroups/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var expenseGroup = _expenseTrackerRepository.GetExpenseGroup(id);
                if (expenseGroup == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_expenseGroupFactory.CreateExpenseGroup(expenseGroup));
                }
                
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [Route("ExpenseGroup")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] LinhNguyen.DTO.Expense.ExpenseGroup expenseGroup)
        {
            try
            {
                if (expenseGroup == null)
                {
                    return BadRequest();
                }

                var eg = _expenseGroupFactory.CreateExpenseGroup(expenseGroup);
                var result = _expenseTrackerRepository.InsertExpenseGroup(eg);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    var newEG = _expenseGroupFactory.CreateExpenseGroup(result.Entity);

                    return Created(Request.RequestUri + "/" + newEG.Id.ToString(), newEG);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

    }
}
