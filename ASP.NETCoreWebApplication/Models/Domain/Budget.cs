﻿using System;
using ASP.NETCoreWebApplication.Models.Enum;

namespace ASP.NETCoreWebApplication.Models.Domain
{
	public class Budget
	{
		public Int32 Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public TypeOperation TypeOperation { get; set; }
		public CategoryTypeOperation CategoryTypeOperation { get; set; }
		public Decimal OperationSum { get; set; }
		public String Description { get; set; }

		public Budget() { }

		public Budget(Int32 id, DateTime createdDate, TypeOperation typeOperation, CategoryTypeOperation categoryTypeOperation, Decimal operationSum, String description)
		{
			Id = id;
			CreatedDate = createdDate;
			TypeOperation = typeOperation;
			CategoryTypeOperation = categoryTypeOperation;
			OperationSum = operationSum;
			Description = description;
		}
	}
}