using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinhNguyen.DTO
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        //public virtual string FirstName { get; set; }
        //public virtual string LastName { get; set; }
    }
}
