using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinhNguyen.Repository.Entities;
using LinhNguyen.Repository.DAL;
using LinhNguyen.Repository.ViewModels;

namespace LinhNguyen.Repository
{
    public class CustomerRepository : ICustomerRepository 
    {
        private SraContext _ctx;

        public CustomerRepository(SraContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = true;
        }

        public RepositoryActionResult<CustomerModel> DoLogin(LoginModel model)
        {
            try
            {
                var customer = _ctx.Customers.Where(x => x.Username == model.Username
                              && x.Password == model.Password)
                              .Select(x => new CustomerModel
                              {
                                  Username = x.Username,
                                  Password = x.Password
                              }).FirstOrDefault();
                return new RepositoryActionResult<CustomerModel>(customer, RepositoryActionStatus.Ok);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<CustomerModel>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public IQueryable<Customer> GetCustomers()
        {
            return _ctx.Customers;
        }

        public RepositoryActionResult<Customer> InsertCustomer(Customer customer)
        {
            try
            {
                _ctx.Customers.Add(customer);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Customer>(customer, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Customer>(customer, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Customer>(null, RepositoryActionStatus.Error, ex);
            }            
        }        
    }
}
