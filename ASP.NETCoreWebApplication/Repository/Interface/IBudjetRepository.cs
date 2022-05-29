using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NETCoreWebApplication.Models.DBmodel;

namespace ASP.NETCoreWebApplication.Repository.Interface
{
	public interface IBudjetRepository
	{
		void AddOperation(BudgetDB budgetDb);
		void RemoveOperation(Int32 id);
		void RemoveOperations(Int32[] ids);
		void UpdateOperation(BudgetDB budget);
		BudgetDB GetOperation(Int32 id);
		List<BudgetDB> GetOperations();
		List<BudgetDB> GetIncomeBudget();
		List<BudgetDB> GetСonsumptionBudget();
		List<BudgetDB> GetWeekOperations(DateTime startDate, DateTime stopDate);
		List<BudgetDB> GetMonthOperations(DateTime month);
		List<BudgetDB> GetYearOperations(DateTime year);
	}
}