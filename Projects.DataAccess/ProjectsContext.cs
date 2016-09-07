using Microsoft.EntityFrameworkCore;
using Projects.Account;

namespace Projects.DataAccess
{
    public class ProjectsContext : DbContext
    {
        public ProjectsContext(DbContextOptions<ProjectsContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
