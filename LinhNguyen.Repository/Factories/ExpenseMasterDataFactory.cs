using LinhNguyen.Repository.Entities.Expense.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Factories
{
    public class ExpenseMasterDataFactory
    {
        public ExpenseMasterDataFactory()
        {

        }

        public ExpenseGroupStatus CreateExpenseGroupStatus(DTO.Expense.ExpenseGroupStatus expenseGroupStatus)
        {
            return new ExpenseGroupStatus()
            {
                Description = expenseGroupStatus.Description,
                Id = expenseGroupStatus.Id
            };
        }

        public DTO.Expense.ExpenseGroupStatus CreateExpenseGroupStatus(ExpenseGroupStatus expenseGroupStatus)
        {
            return new DTO.Expense.ExpenseGroupStatus
            {
                Description = expenseGroupStatus.Description,
                Id = expenseGroupStatus.Id
            };
        }
    }
}
