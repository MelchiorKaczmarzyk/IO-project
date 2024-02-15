using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.ComponentModel;

namespace IOProject.Models
{
    public class SystemUser : IdentityUser
    {
        public List<string>? tags { get; set; }
        public ICollection<HelpProject> HelpProjects { get; set; }
    }
}
