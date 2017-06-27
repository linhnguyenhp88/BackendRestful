using LinhNguyen.Repository;
using LinhNguyen.Repository.Factories;
using Marvin.JsonPatch;
using SraCRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SraCRM.Controllers
{
    [RoutePrefix("api")]
    public class ExpenseGroupsController : ApiController
    {
        private readonly IExpenseTrackerRepository _expenseTrackerRepository;
        private readonly ExpenseGroupFactory _expenseGroupFactory;
        private const int maxPageSize = 10;
       
        public ExpenseGroupsController(IExpenseTrackerRepository expenseTrackerRepository, ExpenseGroupFactory expenseGroupFactory)
        {
            _expenseTrackerRepository = expenseTrackerRepository;
            _expenseGroupFactory = expenseGroupFactory;
        }

        [Route("ExpenseGroups")]
        [HttpGet]
        public IHttpActionResult Get(string sort = "id", string status = null, string userId = null, 
            int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                int statusId = -1;
                if (status != null)
                {
                    switch (status.ToLower())
                    {
                        case "open": statusId = 1;
                            break;
                        case "confirmed": statusId = 2;
                            break;
                        case "processed": statusId = 3;
                            break;
                        default:
                            break;
                    }
                }

                //var expenseGroups = _expenseTrackerRepository.GetExpenseGroups();

                //return Ok(expenseGroups.ToList()
                //    .Select(eg => _expenseGroupFactory.CreateExpenseGroup(eg)));

                var expenseGroups = _expenseTrackerRepository.GetExpenseGroups()
                    .ApplySort(sort)
                    .Where(eg => (statusId == -1 || eg.ExpenseGroupStatusId == statusId))
                    .Where(eg => (userId == null || eg.UserId == userId));

                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                var totalCount = expenseGroups.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("ExpenseGroupsList",
                    new { page = page - 1, pageSize = pageSize, sort = sort, status = status, userId = userId }) : "";
                var newLink = page < totalPages ? urlHelper.Link("ExpenseGroupsList", 
                    new {page = page +1 , pageSize = pageSize , sort = sort, status = status, userId = userId}) : "";

                var aginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize
                };

                return Ok(expenseGroups);
            }
            catch (Exception ex)
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

        [Route("ExpenseGroup/{id:int}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] LinhNguyen.DTO.Expense.ExpenseGroup expenseGroup)
        {
            try
            {
                if (expenseGroup == null)
                {
                    return BadRequest();
                }

                var eg = _expenseGroupFactory.CreateExpenseGroup(expenseGroup);
                var result = _expenseTrackerRepository.UpdateExpenseGroup(eg);

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var updatedExpenseGroup = _expenseGroupFactory.CreateExpenseGroup(result.Entity);

                    return Ok(updatedExpenseGroup);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }


        [Route("ExpenseGroup/{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, 
            [FromBody]JsonPatchDocument<LinhNguyen.DTO.Expense.ExpenseGroup> expenseGroupPatchDocument)
        {
            try
            {
                if (expenseGroupPatchDocument == null)
                {
                    return BadRequest();
                }

                var expenseGroup = _expenseTrackerRepository.GetExpenseGroup(id);

                if (expenseGroup == null)
                {
                    return NotFound();
                }

                var eg = _expenseGroupFactory.CreateExpenseGroup(expenseGroup);

                expenseGroupPatchDocument.ApplyTo(eg);

                var result = _expenseTrackerRepository.UpdateExpenseGroup(_expenseGroupFactory.CreateExpenseGroup(eg));

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    var patchedExpenseGroup = _expenseGroupFactory.CreateExpenseGroup(result.Entity);
                    return Ok(patchedExpenseGroup);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }


        [Route("ExpenseGroup/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _expenseTrackerRepository.DeleteExpenseGroup(id);

                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if(result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

    }
}
