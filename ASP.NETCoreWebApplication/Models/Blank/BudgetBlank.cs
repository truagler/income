using System;
using ASP.NETCoreWebApplication.Models.Enum;

namespace ASP.NETCoreWebApplication.Models.Blank
{
	public class BudgetBlank
	{
		public Int32 Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public TypeOperation TypeOperation { get; set; }
		public CategoryTypeOperation CategoryTypeOperation { get; set; }
		public Decimal OperationSum { get; set; }
		public String Description { get; set; }
	}
}