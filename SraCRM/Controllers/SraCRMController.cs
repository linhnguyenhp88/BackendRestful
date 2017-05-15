using LinhNguyen.Repository;
using LinhNguyen.Repository.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinhNguyen.DTO;
using LinhNguyen.Repository.ViewModels;
using System.Web.Http.Cors;

namespace SraCRM.Controllers
{
    [RoutePrefix("api")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class SraCRMController : ApiController
    {
        private ICustomerRepository _customerRepository;
        private IFtagRepository _ftagRepository;
        private CustomerFactory _customerFactory = new CustomerFactory();

        private int maxPageSize = 10;

        public SraCRMController(ICustomerRepository customerRepository, IFtagRepository ftagRepository)
        {
            _customerRepository = customerRepository;
            _ftagRepository = ftagRepository;
        }
  
        public SraCRMController()
        {
            _customerRepository = new CustomerRepository(new LinhNguyen.Repository.DAL.SraContext());
            _ftagRepository = new FtagRepository(new LinhNguyen.Repository.DAL.SraContext());
        }

        [Route("customers")]
        [HttpGet]      
        public IHttpActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerRepository.GetCustomers();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("customers")]
        [HttpPost]      
        public IHttpActionResult Post([FromBody]LinhNguyen.DTO.Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                var _customer = _customerFactory.CreateCustomer(customer);
                var result = _customerRepository.InsertCustomer(_customer);

                if (result.Status == RepositoryActionStatus.Created)
                { 
                    var newCustomer = _customerFactory.CreateCustomer(result.Entity);
                    return Created<LinhNguyen.DTO.Customer>(Request.RequestUri + "/" + newCustomer.Id.ToString(), newCustomer);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginModel submitModel)
        {

            try
            {
                var model = new LoginModel
                {
                    Username = submitModel.Username,
                    Password = submitModel.Password
                };

                var _customer = _customerRepository.DoLogin(model);

                if (_customer == null)
                {
                    return BadRequest();
                }
                else
                {
                    return this.Ok(_customer);
                }
            }
            catch (Exception)
            {

                throw;
            }         
        }

        [Route("formlogin")]
        [HttpGet]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        public IHttpActionResult ListFtagLogin()
        {
            try
            {
                var ftag = _ftagRepository.FtagLogin();

                return Ok(ftag);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
