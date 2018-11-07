using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Authorization
{
    public class UserMustBeSuperAdministratorRequirementHandler : AuthorizationHandler<UserMustBeSuperAdministratorRequirement>
    {


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserMustBeSuperAdministratorRequirement requirement)
        {
           if (context.User.IsInRole("Super"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
