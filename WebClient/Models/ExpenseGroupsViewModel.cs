using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class ExpenseGroupsViewModel
    {
        public IEnumerable<LinhNguyen.DTO.Expense.ExpenseGroup> ExpenseGroups { get; set; }
        public IEnumerable<LinhNguyen.DTO.Expense.ExpenseGroupStatus> ExpenseGroupStatuses { get; set; }
    }
}