using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Authorization
{
    public class UserMustBeAdministratorRequirement : IAuthorizationRequirement
    {
        public string Role { get; private set; }
        public UserMustBeAdministratorRequirement(string role)
        {
            Role = role;
        }
    }
}
