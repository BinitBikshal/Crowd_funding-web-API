using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public virtual ICollection<Campaign>? Campaigns { get; set; }
        public virtual ICollection<Donation>? Donations { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
