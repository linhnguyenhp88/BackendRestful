using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinhNguyen.Repository.Entities.Expense.Entity;
using LinhNguyen.Repository.DAL;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace LinhNguyen.Repository
{
    public class ExpenseTrackerEFRepository : IExpenseTrackerRepository
    {
        private readonly SraContext _ctx;

        public ExpenseTrackerEFRepository(SraContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = true;
        }
       
        public RepositoryActionResult<Expense> DeleteExpense(int id)
        {
            try
            {
                var exp = _ctx.Expenses.Where(e => e.Id == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Expenses.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Expense> DeleteExpenseGroup(int id)
        {
            try
            {
                var eg = _ctx.ExpenseGroups.Where(x => x.Id == id).FirstOrDefault();
                if (eg != null)
                {
                    _ctx.ExpenseGroups.Remove(eg);
                    _ctx.SaveChanges();

                    return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.Deleted);
                }

                return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.Error, ex);
            }
        }
      
        public Expense Getexpense(int id, int? expenseGroupId = default(int?))
        {
            return _ctx.Expenses.FirstOrDefault(e => e.Id == id && (expenseGroupId == null || e.ExpenseGroupId == expenseGroupId));
        }

        public ExpenseGroup GetExpenseGroup(int id)
        {
            return _ctx.ExpenseGroups.FirstOrDefault(x => x.Id == id);
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
            return null;
        }

        public IQueryable<ExpenseGroupStatus> GetExpenseGroupStatuses()
        {
            return _ctx.ExpenseGroupStatuses;
        }

        public IQueryable<ExpenseGroup> GetExpenseGroupWithExpenses()
        {
            return _ctx.ExpenseGroups.Include("Expenses");
        }

        public ExpenseGroup GetExpenseGroupWithExpenses(int id)
        {
            return _ctx.ExpenseGroups.Include("Expenses").FirstOrDefault(eg => eg.Id == id);
        }

        public ExpenseGroup GetExpenseGroupWithExpenses(int id, string userId)
        {
            return _ctx.ExpenseGroups.Include("Expenses").FirstOrDefault(eg => eg.Id == id && eg.UserId == userId);
        }

        public IQueryable<Expense> GetExpenses()
        {
            return _ctx.Expenses;
        }

        public IQueryable<Expense> GetExpenses(int expenseGroupId)
        {
            try
            {
                var correctGroup = _ctx.ExpenseGroups.Where(eg => eg.Id == expenseGroupId).FirstOrDefault();

                if (correctGroup != null)
                {
                    return _ctx.Expenses.Where(e => e.ExpenseGroupId == expenseGroupId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public RepositoryActionResult<Expense> InsertExpense(Expense e)
        {
            try
            {
                _ctx.Expenses.Add(e);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Expense>(e, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Expense>(e, RepositoryActionStatus.NothingModified);
                }
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ExpenseGroup> InsertExpenseGroup(ExpenseGroup eg)
        {
            try
            {
                _ctx.ExpenseGroups.Add(eg);
                var result = _ctx.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<ExpenseGroup>(eg, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ExpenseGroup>(eg, RepositoryActionStatus.NothingModified);
                }
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<ExpenseGroup>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Expense> UpdateExpense(Expense e)
        {
            try
            {
                var existingExpense = _ctx.Expenses.Where(x => x.Id == e.Id).FirstOrDefault();

                if (existingExpense == null)
                {
                    return new RepositoryActionResult<Expense>(e, RepositoryActionStatus.NotFound);
                }             

                _ctx.Entry(existingExpense).State = EntityState.Detached;
                _ctx.Expenses.Attach(e);
                _ctx.Entry(e).State = EntityState.Modified;
                var result = _ctx.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<Expense>(e, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Expense>(e, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Expense>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ExpenseGroup> UpdateExpenseGroup(ExpenseGroup eg)
        {
            try
            {
                var existingEG = _ctx.ExpenseGroups.Where(x => x.Id == eg.Id).FirstOrDefault();
                if (existingEG == null)
                {
                    return new RepositoryActionResult<ExpenseGroup>(eg, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingEG).State = EntityState.Detached;
                _ctx.ExpenseGroups.Attach(eg);
                _ctx.Entry(eg).State = EntityState.Modified;

                var result = _ctx.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<ExpenseGroup>(eg, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ExpenseGroup>(eg, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ExpenseGroup>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}
