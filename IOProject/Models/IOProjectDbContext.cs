using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IOProject.Models
{
    public class IOProjectDbContext : IdentityDbContext<SystemUser>
    {
        public IOProjectDbContext(DbContextOptions<IOProjectDbContext> options) :
    base(options)
        {

        }

        public DbSet<HelpProject> HelpProjects { get; set; }
    }
}
