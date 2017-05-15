using LinhNguyen.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Factories
{
    public class CustomerFactory
    {
        public CustomerFactory()
        {

        }

        public DTO.Customer CreateCustomer(Customer customer)
            => new DTO.Customer()
            {
                Username = customer.Username,
                Password = customer.Password
            };

        public Customer CreateCustomer(DTO.Customer customer)
            => new Customer()
            {
                Username = customer.Username,
                Password = customer.Password
            };
    }
}
