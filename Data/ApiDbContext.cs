using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WebApicrud.Models;

namespace WebApicrud.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)

        {
            
        }

        public DbSet<Students> Students { get; set; }

    }
}
