using System;
using System.Collections.Generic;
using ASP.NETCoreWebApplication.Models.Blank;
using ASP.NETCoreWebApplication.Models.ViewModel;
using ASP.NETCoreWebApplication.Service.Interface;

namespace ASP.NETCoreWebApplication.Code
{
	public class BudgetCode
	{
		private IBudgetService _budgetService;

		private Converter.Converter _converter = new Converter.Converter();

		public BudgetCode(IBudgetService budgetService)
		{
			_budgetService = budgetService;
		}

		public void AddOperation(BudgetBlank budget)
		{
			_budgetService.AddOperation(_converter.ToDomain(budget));
		}

		public void UpdateOperation(BudgetBlank budget)
		{
			_budgetService.UpdateOperation(_converter.ToDomain(budget));
		}

		public void RemoveOperation(Int32 id)
		{
			_budgetService.RemoveOperation(id);
		}

		public void RemoveOperations(Int32[] ids)
		{
			_budgetService.RemoveOperations(ids);
		}

		public BudgetView GetOperation(Int32 id)
		{
			return _converter.ToView(_budgetService.GetOperation(id));
		}

		public List<BudgetView> GetOperations()
		{
			return _converter.ToViews(_budgetService.GetOperations());
		}

		public List<BudgetView> GetWeekOperations(DateTime startDate, DateTime stopDate)
		{
			return _converter.ToViews(_budgetService.GetWeekOperations(startDate, stopDate));
		}

		public List<BudgetView> GetMonthOperations(DateTime month)
		{
			return _converter.ToViews(_budgetService.GetMonthOperations(month));
		}

		public List<BudgetView> GetYearOperations(DateTime year)
		{
			return _converter.ToViews(_budgetService.GetYearOperations(year));
		}

		public Decimal GetFullRevue()
		{
			return _budgetService.GetFullRevue();
		}
	}
}