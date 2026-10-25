using Microsoft.EntityFrameworkCore;
using WebApiBasicsTut.Models;

namespace WebApiBasicsTut.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
