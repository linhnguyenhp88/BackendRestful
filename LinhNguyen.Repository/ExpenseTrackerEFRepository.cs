using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinhNguyen.Repository.Entities.Expense.Entity;
using LinhNguyen.Repository.DAL;

namespace LinhNguyen.Repository
{
    public class ExpenseTrackerEFRepository : IExpenseTrackerRepository
    {
        private readonly SraContext _ctx;

        public ExpenseTrackerEFRepository()
        {

        }

        public ExpenseTrackerEFRepository(SraContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = true;
        }

        public RepositoryActionResult<Expense> DeleteExpense(int id)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<Expense> DeleteExpenseGroup(int id)
        {
            throw new NotImplementedException();
        }
      
        public Expense Getexpense(int id, int? expenseGroupId = default(int?))
        {
            return _ctx.Expenses.Where(e => e.Id == id && (expenseGroupId == null || e.ExpenseGroupId == expenseGroupId)).FirstOrDefault();
        }

        public ExpenseGroup GetExpenseGroup(int id)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroup GetExpenseGroup(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExpenseGroup> GetExpenseGroup(string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExpenseGroup> GetExpenseGroups()
        {
            return _ctx.ExpenseGroups;
        }

        public ExpenseGroupStatus GetExpenseGroupStatus(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExpenseGroupStatus> GetExpenseGroupStatuses()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExpenseGroup> GetExpenseGroupWithExpenses()
        {
            throw new NotImplementedException();
        }

        public ExpenseGroup GetExpenseGroupWithExpenses(int id)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroup GetExpenseGroupWithExpenses(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Expense> GetExpenses()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Expense> GetExpenses(int expenseGroupId)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<Expense> InsertExpense(Expense e)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<ExpenseGroup> InsertExpenseGroup(ExpenseGroup eg)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<Expense> UpdateExpense(Expense e)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<ExpenseGroup> UpdateExpenseGroup(Expense eg)
        {
            throw new NotImplementedException();
        }
    }
}
