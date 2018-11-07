using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Authorization
{
    public class UserMustBeSuperAdministratorRequirement : IAuthorizationRequirement
    {
        public string Role { get; private set; }
        public UserMustBeSuperAdministratorRequirement(string role)
        {
            Role = role;
        }
    }
}
