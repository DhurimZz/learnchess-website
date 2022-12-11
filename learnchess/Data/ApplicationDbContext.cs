using Microsoft.EntityFrameworkCore;

namespace learnchess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

    }
}
