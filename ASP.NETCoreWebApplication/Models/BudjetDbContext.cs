using ASP.NETCoreWebApplication.Models.DBmodel;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebApplication.Models
{
	public sealed class BudjetDbContext: DbContext
	{
		public DbSet<BudgetDB> Budjet { get; set; }

		public BudjetDbContext(DbContextOptions<BudjetDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}