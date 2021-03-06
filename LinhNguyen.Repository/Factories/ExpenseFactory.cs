﻿using LinhNguyen.Repository.Entities.Expense.Entity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinhNguyen.Repository.Factories
{
    public class ExpenseFactory
    {
        public ExpenseFactory()
        {

        }

        public DTO.Expense.Expense CreateExpense(Expense expense)
        {
            return new DTO.Expense.Expense
            {
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                ExpenseGroupId = expense.ExpenseGroupId,
                Id = expense.Id
            };
        }

        public Expense CreateExpense(DTO.Expense.Expense expense)
        {
            return new Expense
            {
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                ExpenseGroupId = expense.ExpenseGroupId,
                Id = expense.Id
            };
        }

        public object CreateDataShapedObject(Expense expense, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreateExpense(expense), lstOfFields);
        }

        public object CreateDataShapedObject(DTO.Expense.Expense expense, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return expense;
            }
            else
            {
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    var fieldValue = expense.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(expense, null);

                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}
