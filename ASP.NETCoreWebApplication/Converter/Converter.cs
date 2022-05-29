using System.Collections.Generic;
using System.Linq;
using ASP.NETCoreWebApplication.Models.Blank;
using ASP.NETCoreWebApplication.Models.DBmodel;
using ASP.NETCoreWebApplication.Models.Domain;
using ASP.NETCoreWebApplication.Models.ViewModel;

namespace ASP.NETCoreWebApplication.Converter
{
	public class Converter
	{
		public BudgetDB ToDb(Budget budget)
		{
			return new BudgetDB(budget.Id, budget.CreatedDate, budget.TypeOperation, budget.CategoryTypeOperation, budget.OperationSum, budget.Description, false);
		}

		public Budget ToDomain(BudgetDB budgetDb)
		{
			return new Budget(budgetDb.Id, budgetDb.CreatedDate, budgetDb.TypeOperation, budgetDb.CategoryTypeOperation, budgetDb.OperationSum, budgetDb.Description);
		}

		public List<Budget> ToDomains(List<BudgetDB> budgetDbs)
		{
			return budgetDbs.Select(ToDomain).ToList();
		}

		public BudgetView ToView(Budget budget)
		{
			return new BudgetView(budget.Id, budget.CreatedDate, budget.TypeOperation, budget.CategoryTypeOperation, budget.OperationSum, budget.Description);
		}

		public List<BudgetView> ToViews(List<Budget> budgets)
		{
			return budgets.Select(ToView).ToList();
		}
		
		public Budget ToDomain(BudgetBlank blank)
		{
			return new Budget(0, blank.CreatedDate, blank.TypeOperation, blank.CategoryTypeOperation, blank.OperationSum, blank.Description);
		}
	}
}