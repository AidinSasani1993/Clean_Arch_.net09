using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Clean.EntityFrameworkCore.DataBaseContext
{
    public class CleanDbContextFactory : IDesignTimeDbContextFactory<CleanDbContext>
    {
        public CleanDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CleanDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-3H551EF\\SQLEXPRESS;Initial Catalog=CleanDbContext;Integrated Security=True; encrypt=false;"
            );

            return new CleanDbContext(optionsBuilder.Options);
        }
    }
}
