using System;
using System.Collections.Generic;
using ASP.NETCoreWebApplication.Code;
using ASP.NETCoreWebApplication.Models.Blank;
using ASP.NETCoreWebApplication.Models.ViewModel;
using ASP.NETCoreWebApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebApplication.Controllers
{
	public class BudgetController : ControllerBase
	{
		private BudgetCode _budgetCode;

		public BudgetController(IBudgetService budgetService)
		{
			_budgetCode = new BudgetCode(budgetService);
		}

		[HttpPost]
		[Route("addoperation")]
		public void AddOperation([FromBody] BudgetBlank budget)
		{
			_budgetCode.AddOperation(budget);
		}

		[HttpPost]
		[Route("updateoperation")]
		public void UpdateOperation([FromBody] BudgetBlank budget)
		{
			_budgetCode.UpdateOperation(budget);
		}

		[HttpPost]
		[Route("removeoperation")]
		public void RemoveOperation([FromBody] Int32 id)
		{
			_budgetCode.RemoveOperation(id);
		}

		[HttpPost]
		[Route("removeoperations")]
		public void RemoveOperations([FromBody] Int32[] ids)
		{
			_budgetCode.RemoveOperations(ids);
		}

		[HttpGet]
		[Route("getoperation/{id?}")]
		public BudgetView GetOperation(Int32 id)
		{
			return _budgetCode.GetOperation(id);
		}

		[HttpGet]
		[Route("getoperations")]
		public List<BudgetView> GetOperations()
		{
			return _budgetCode.GetOperations();
		}

		[HttpGet]
		[Route("getweekoperations/{startdate?}/{enddate?}")]
		public List<BudgetView> GetWeekOperations(DateTime startDate, DateTime stopDate)
		{
			return _budgetCode.GetWeekOperations(startDate, stopDate);
		}

		[HttpGet]
		[Route("getmonthoperations/{month?}")]
		public List<BudgetView> GetMonthOperations(DateTime month)
		{
			return _budgetCode.GetMonthOperations(month);
		}

		[HttpGet]
		[Route("getyearoperations/{year?}")]
		public List<BudgetView> GetYearOperations(DateTime year)
		{
			return _budgetCode.GetYearOperations(year);
		}

		[HttpGet]
		[Route("getfullrevenue")]
		public Decimal GetFullRevue()
		{
			return _budgetCode.GetFullRevue();
		}
	}
}