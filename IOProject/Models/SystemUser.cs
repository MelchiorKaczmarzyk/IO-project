using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace IOProject.Models
{
    public class SystemUser : IdentityUser
    {
        public List<string>? tags { get; set; }
    }
}
