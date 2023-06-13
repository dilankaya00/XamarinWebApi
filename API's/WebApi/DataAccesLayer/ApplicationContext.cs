using Microsoft.EntityFrameworkCore;
using WebApi.API.Entity;

namespace WebApi.API.DataAccesLayer
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
