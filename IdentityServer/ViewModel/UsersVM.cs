using DataIdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.ViewModel
{
    public class UsersVM
    {
        public ICollection<ApplicationUserVm> Users { get; set; } = new HashSet<ApplicationUserVm>();
    }
    public class ApplicationUserVm
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string Roles { get; set; }
    }
}
