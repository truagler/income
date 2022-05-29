using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NETCoreWebApplication.Models;
using ASP.NETCoreWebApplication.Models.DBmodel;
using ASP.NETCoreWebApplication.Models.Enum;
using ASP.NETCoreWebApplication.Repository.Interface;

namespace ASP.NETCoreWebApplication.Repository
{
	public class BudjetRepository: IBudjetRepository
	{
		private BudjetDbContext _db;

		public BudjetRepository(BudjetDbContext db)
		{
			_db = db;
		}

		public void AddOperation(BudgetDB budgetDb)
		{
			if (budgetDb != null)
			{
				_db.Budjet.Add(budgetDb);
				_db.SaveChanges();
			}
		}

		public void RemoveOperation(Int32 id)
		{
			if (id != 0)
			{
				BudgetDB budgetDb = _db.Budjet.FirstOrDefault(budget => budget.Id == id);
				if (budgetDb != null)
				{
					budgetDb.IsRemoved = true;
					_db.SaveChanges();
				}
			}
		}

		public void RemoveOperations(Int32[] ids)
		{
			if (ids.Length != 0)
			{
				foreach (Int32 id in ids)
				{
					BudgetDB budgetDb = _db.Budjet.FirstOrDefault(budget => budget.Id == id);
					if (budgetDb != null)
					{
						budgetDb.IsRemoved = true;
						_db.SaveChanges();
					}
				}
			}
		}

		public void UpdateOperation(BudgetDB budget)
		{
			if (budget != null)
			{
				BudgetDB budgetDb = _db.Budjet.FirstOrDefault(b => b.Id == budget.Id);
				if (budgetDb != null)
				{
					budgetDb.OperationSum = budget.OperationSum;
					budgetDb.TypeOperation = budget.TypeOperation;
					budgetDb.CategoryTypeOperation = budget.CategoryTypeOperation;
					budgetDb.Description = budget.Description;
					_db.SaveChanges();
				}
			}
		}

		public BudgetDB GetOperation(Int32 id)
		{
			BudgetDB budgetDb = new BudgetDB();
			if (id != 0)
			{
				budgetDb = _db.Budjet.FirstOrDefault(b => b.Id == id);
			}

			return budgetDb;
		}

		public List<BudgetDB> GetOperations()
		{
			List<BudgetDB> budgetDbs = new List<BudgetDB>();
			
			budgetDbs.AddRange(_db.Budjet.Where(x => x.IsRemoved == false));

			return budgetDbs;
		}

		public List<BudgetDB> GetIncomeBudget()
		{
			 
			return _db.Budjet.Where(budget => budget.TypeOperation == TypeOperation.Income && budget.IsRemoved == false).ToList();;
		}

		public List<BudgetDB> GetСonsumptionBudget()
		{
			return _db.Budjet.Where(budget => budget.TypeOperation == TypeOperation.Сonsumption && budget.IsRemoved == false).ToList();
		}
		
		public List<BudgetDB> GetWeekOperations(DateTime startDate, DateTime stopDate)
		{
			List<BudgetDB> budgetDbs = new List<BudgetDB>();
			
			budgetDbs.AddRange(_db.Budjet.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= stopDate));

			return budgetDbs;
		}
		
		public List<BudgetDB> GetMonthOperations(DateTime month)
		{
			List<BudgetDB> budgetDbs = new List<BudgetDB>();
			
			budgetDbs.AddRange(_db.Budjet.Where(x => x.CreatedDate.Month == month.Month));

			return budgetDbs;
		}
		
		public List<BudgetDB> GetYearOperations(DateTime year)
		{
			List<BudgetDB> budgetDbs = new List<BudgetDB>();
			
			budgetDbs.AddRange(_db.Budjet.Where(x => x.CreatedDate.Year == year.Year));

			return budgetDbs;
		}
	}
}