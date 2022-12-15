using Microsoft.EntityFrameworkCore;

namespace learnchess.Configuration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

    }
}
