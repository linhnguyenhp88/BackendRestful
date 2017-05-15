using LinhNguyen.Repository.Entities;
using LinhNguyen.Repository.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository
{
    public interface ICustomerRepository
    {
        RepositoryActionResult<Customer> InsertCustomer(Customer customer);

        RepositoryActionResult<CustomerModel> DoLogin(LoginModel model);

        //RepositoryActionResult<CustomerModel> DoRegister(CustomerModel model);

        IQueryable<Customer> GetCustomers();
    }
}
