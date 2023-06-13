using Microsoft.EntityFrameworkCore;
using WebApi.API.Entity;

namespace WebApi.API.DataAccesLayer
{
    public class ProductsContext:DbContext
    {
        public ProductsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
