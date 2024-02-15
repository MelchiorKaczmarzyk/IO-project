using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IOProject.Models;

namespace IOProject.Models
{
    public class IOProjectDbContext : IdentityDbContext<SystemUser>
    {
        public IOProjectDbContext(DbContextOptions<IOProjectDbContext> options) :
    base(options)
        {

        }

        public DbSet<HelpProject> HelpProjects { get; set; }
        public DbSet<IOProject.Models.Application> Application { get; set; } = default!;
        public DbSet<IOProject.Models.HelpOffer> HelpOffer { get; set; } = default!;
    }
}
