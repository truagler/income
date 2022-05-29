using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NETCoreWebApplication.Models.Domain;
using ASP.NETCoreWebApplication.Repository.Interface;
using ASP.NETCoreWebApplication.Service.Interface;
using ASP.NETCoreWebApplication.Tools;

namespace ASP.NETCoreWebApplication.Service
{
	public class BudgetService: IBudgetService
	{
		private IBudjetRepository _budjetRepository;

		private Converter.Converter _converter = new Converter.Converter();

		public BudgetService(IBudjetRepository budjetRepository)
		{
			_budjetRepository = budjetRepository;
		}

		public void AddOperation(Budget budget)
		{
			budget.Id = ID.NewId();
			_budjetRepository.AddOperation(_converter.ToDb(budget));
		}

		public void UpdateOperation(Budget budget)
		{
			_budjetRepository.UpdateOperation(_converter.ToDb(budget));
		}
		
		public void RemoveOperation(Int32 id)
		{
			_budjetRepository.RemoveOperation(id);
		}

		public void RemoveOperations(Int32[] ids)
		{
			_budjetRepository.RemoveOperations(ids);
		}

		public Budget GetOperation(Int32 id)
		{
			return _converter.ToDomain(_budjetRepository.GetOperation(id));
		}

		public List<Budget> GetOperations()
		{
			return _converter.ToDomains(_budjetRepository.GetOperations());
		}

		private List<Budget> GetIncomeBudget()
		{
			return _converter.ToDomains(_budjetRepository.GetIncomeBudget());
		}

		private List<Budget> GetСonsumptionBudget()
		{
			return _converter.ToDomains(_budjetRepository.GetСonsumptionBudget());
		}

		public List<Budget> GetWeekOperations(DateTime startDate, DateTime stopDate)
		{
			return _converter.ToDomains(_budjetRepository.GetWeekOperations(startDate, stopDate));
		}

		public List<Budget> GetMonthOperations(DateTime month)
		{
			return _converter.ToDomains(_budjetRepository.GetMonthOperations(month));
		}

		public List<Budget> GetYearOperations(DateTime year)
		{
			return _converter.ToDomains(_budjetRepository.GetYearOperations(year));
		}

		public Decimal GetFullRevue()
		{
			Decimal incomeValue = Decimal.Zero;
			Decimal consumptionValue = Decimal.Zero;
			
			List<Budget> income = GetIncomeBudget();
			
			if (income.Count != 0)
			{
				incomeValue = income.Sum(x => x.OperationSum);
			}
			
			List<Budget> consumption = GetСonsumptionBudget();

			if (consumption.Count != 0)
			{
				consumptionValue = consumption.Sum(x => x.OperationSum);
			}

			return incomeValue - consumptionValue;
		}
	}
}