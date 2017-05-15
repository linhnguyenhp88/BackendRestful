using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Entities.Expense.Entity
{
    [Table("ExpenseGroup")]

    public class ExpenseGroup
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ExpenseGroupStatusId { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
