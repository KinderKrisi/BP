using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Authorization
{
    public class UserMustBeAdministratorRequirementHandler : AuthorizationHandler<UserMustBeAdministratorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserMustBeAdministratorRequirement requirement)
        {
            var role = context.User.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;
            if (role == requirement.Role)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }

            return Task.CompletedTask;
        }
    }
}
