using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.ComponentModel;

namespace IOProject.Models
{
    public class SystemUser : IdentityUser
    {
        public List<string>? tags { get; set; }
        public virtual ICollection<HelpProject> HelpProjects { get; } = new List<HelpProject>();
    }
}
