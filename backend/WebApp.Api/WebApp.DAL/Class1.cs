using Microsoft.EntityFrameworkCore;

namespace WebApp.DAL
{
    public class WebAppDbContext : DbContext
    {
        //public DbSet<>

        public WebAppDbContext() : base()
        {

        }

        public WebAppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
