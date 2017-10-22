using LinhNguyen.DTO.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientMvc.Models
{
    public class ExpenseGroupsViewModel
    {
        public IEnumerable<ExpenseGroup> ExpenseGroups { get; set; }

        public IEnumerable<ExpenseGroupStatus> ExpenseGroupStatusses { get; set; }

        public ExpenseGroup ExpenseGroup { get; set;}
    }
}