using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Entities.Expense.Entity
{
    [Table("ExpenseGroupStatus")]
    public class ExpenseGroupStatus
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
