using System;
using System.Collections.Generic;
using ASP.NETCoreWebApplication.Models.Domain;

namespace ASP.NETCoreWebApplication.Service.Interface
{
	public interface IBudgetService
	{
		void AddOperation(Budget budget);
		void UpdateOperation(Budget budget);
		void RemoveOperation(Int32 id);
		void RemoveOperations(Int32[] ids);
		Budget GetOperation(Int32 id);
		List<Budget> GetOperations();
		List<Budget> GetWeekOperations(DateTime startDate, DateTime stopDate);
		List<Budget> GetMonthOperations(DateTime month);
		List<Budget> GetYearOperations(DateTime year);
		Decimal GetFullRevue();
	}
}