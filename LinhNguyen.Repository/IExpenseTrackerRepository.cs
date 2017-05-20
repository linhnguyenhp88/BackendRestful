using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository
{
    public interface IExpenseTrackerRepository
    {
        RepositoryActionResult<Entities.Expense.Entity.Expense> DeleteExpense(int id);
        RepositoryActionResult<Entities.Expense.Entity.Expense> DeleteExpenseGroup(int id);
        Entities.Expense.Entity.Expense Getexpense(int id, int? expenseGroupId = null);
        Entities.Expense.Entity.ExpenseGroup GetExpenseGroup(int id);
        Entities.Expense.Entity.ExpenseGroup GetExpenseGroup(int id, string userId);
        IQueryable<Entities.Expense.Entity.ExpenseGroup> GetExpenseGroups();
        IQueryable<Entities.Expense.Entity.ExpenseGroup> GetExpenseGroup(string userId);
        Entities.Expense.Entity.ExpenseGroupStatus GetExpenseGroupStatus(int id);
        IQueryable<Entities.Expense.Entity.ExpenseGroupStatus> GetExpenseGroupStatuses();
        IQueryable<Entities.Expense.Entity.ExpenseGroup> GetExpenseGroupWithExpenses();
        Entities.Expense.Entity.ExpenseGroup GetExpenseGroupWithExpenses(int id);
        Entities.Expense.Entity.ExpenseGroup GetExpenseGroupWithExpenses(int id, string userId);
        IQueryable<Entities.Expense.Entity.Expense> GetExpenses();
        IQueryable<Entities.Expense.Entity.Expense> GetExpenses(int expenseGroupId);

        RepositoryActionResult<Entities.Expense.Entity.Expense> InsertExpense(Entities.Expense.Entity.Expense e);
        RepositoryActionResult<Entities.Expense.Entity.ExpenseGroup> InsertExpenseGroup(Entities.Expense.Entity.ExpenseGroup eg);
        RepositoryActionResult<Entities.Expense.Entity.Expense> UpdateExpense(Entities.Expense.Entity.Expense e);
        RepositoryActionResult<Entities.Expense.Entity.ExpenseGroup> UpdateExpenseGroup(Entities.Expense.Entity.ExpenseGroup eg);
    }
}
