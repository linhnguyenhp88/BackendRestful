using LinhNguyen.Repository.Entities.Expense.Entity;
using LinhNguyen.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Factories
{
    public class ExpenseGroupFactory
    {
        private readonly ExpenseFactory _expenseFactory = new ExpenseFactory();

        public ExpenseGroupFactory()
        {
        }

        public ExpenseGroup CreateExpenseGroup(DTO.Expense.ExpenseGroup expenseGroup)
        {
            return new ExpenseGroup
            {
                Id = expenseGroup.Id,
                UserId = expenseGroup.UserId,
                Title = expenseGroup.Title,
                Description = expenseGroup.Description,
                ExpenseGroupStatusId = expenseGroup.ExpenseGroupStatusId,
                Expenses = expenseGroup.Expenses == null ? new List<Expense>() : expenseGroup.Expenses.Select(e => _expenseFactory.CreateExpense(e)).ToList()
            };
        }

        public DTO.Expense.ExpenseGroup CreateExpenseGroup(ExpenseGroup expenseGroup)
        {
            return new DTO.Expense.ExpenseGroup
            {
                Id = expenseGroup.Id,
                UserId = expenseGroup.UserId,
                Title = expenseGroup.Title,
                Description = expenseGroup.Description,
                ExpenseGroupStatusId = expenseGroup.ExpenseGroupStatusId,
                Expenses = expenseGroup.Expenses.Select(e => _expenseFactory.CreateExpense(e)).ToList()
            };
        }

        public object CreateDataShapedDateObject(LinhNguyen.Repository.Entities.Expense.Entity.ExpenseGroup expenseGroup, List<string> lstFields)
            => CreateDataShapedDateObject(CreateExpenseGroup(expenseGroup), lstFields);

        private object CreateDataShapedDateObject(LinhNguyen.DTO.Expense.ExpenseGroup expenseGroup, List<string> lstFields)
        {
            var lstOfFieldsToWorkWith = new List<string>(lstFields);

            if (!lstOfFieldsToWorkWith.Any())
            {
                return expenseGroup;
            }
            else
            {
                var listOfExpenseFields = lstOfFieldsToWorkWith.Where(x => x.Contains("expenses")).ToList();
                bool returnPartialExpense = listOfExpenseFields.Any() && !listOfExpenseFields.Contains("expense");

                if (returnPartialExpense)
                {
                    lstOfFieldsToWorkWith.RemoveRange(listOfExpenseFields);
                    listOfExpenseFields = lstOfFieldsToWorkWith.Select(x => x.Substring(x.IndexOf(".") +1)).ToList();
                }
                else
                {
                    listOfExpenseFields.Remove("expenses");
                    lstOfFieldsToWorkWith.RemoveRange(listOfExpenseFields);
                }



                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var filed in lstOfFieldsToWorkWith)
                {
                    var fieldValue = expenseGroup.GetType()
                        .GetProperty(filed, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(expenseGroup, null);
                    ((IDictionary<String, Object>)objectToReturn).Add(filed, fieldValue);

                }

                if (returnPartialExpense)
                {
                    var expenses = new List<object>();
                    foreach (var expense in expenseGroup.Expenses)
                    {
                        expenses.Add(_expenseFactory.CreateDataShapedObject(expense, listOfExpenseFields));
                    }

                    ((IDictionary<String, Object>)objectToReturn).Add("expenses", expenses);
                }
                return objectToReturn;
            }
            
        }
    }
}
