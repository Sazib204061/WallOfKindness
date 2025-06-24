using Microsoft.EntityFrameworkCore;

namespace MomotarJhuri.Infractructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
