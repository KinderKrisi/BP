using DataIdentityServer;
using DataIdentityServer.Enum;
using DataIdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IdentityServerDb>();
                context.Database.Migrate(); //Todo: Comment out if you don't want latest migration 

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var roleType in Enum.GetValues(typeof(AdminTypeEnum)))
                {
                    var roleName = roleType.ToString();
                    var role = roleManager.FindByNameAsync(roleName).Result;
                    if (role == null)
                    {
                        var result = roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                    }
                }

                var mk = userManager.FindByEmailAsync("mk@m.com").Result;
                if (mk == null)
                {
                    mk = new ApplicationUser
                    {
                        UserName = "mk",
                        Email = "mk@m.com"
                    };
                    var result = userManager.CreateAsync(mk, "Martin123").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    var type = Enum.GetName(typeof(AdminTypeEnum), AdminTypeEnum.Global);
                    if (!userManager.IsInRoleAsync(mk, type).Result)
                    {
                        result = userManager.AddToRoleAsync(mk, type).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }

                    result = userManager.AddClaimsAsync(mk, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Martin Krisko"),
                        new Claim(JwtClaimTypes.GivenName, "Martin"),
                        new Claim(JwtClaimTypes.FamilyName, "Krisko"),
                        new Claim(JwtClaimTypes.Email, "mk@m.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, 
                        @"{ 'street_address': 'CLaus Cortens Gade 5', 'locality': 'Horsens', 'postal_code': 8700, 'country': 'Denmark' }",
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "Horsens"),
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Console.WriteLine("user mk@m.com created");
                }
                else
                {
                    Console.WriteLine("user with e-mail mk@m.com already exists");
                }

                var dk = userManager.FindByEmailAsync("dk@m.com").Result;
                if (dk == null)
                {
                    dk = new ApplicationUser
                    {
                        UserName = "dk",
                        Email = "dk@m.com"
                    };
                    var result = userManager.CreateAsync(dk, "David123").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    var type = Enum.GetName(typeof(AdminTypeEnum), AdminTypeEnum.Global);
                    if (!userManager.IsInRoleAsync(dk, type).Result)
                    {
                        result = userManager.AddToRoleAsync(dk, type).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                    result = userManager.AddClaimsAsync(dk, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "David Kuts"),
                        new Claim(JwtClaimTypes.GivenName, "David"),
                        new Claim(JwtClaimTypes.FamilyName, "Kuts"),
                        new Claim(JwtClaimTypes.Email, "dk@m.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address,
                        @"{ 'street_address': 'Chr M Østergaards Vej 1A', 'locality': 'Horsens', 'postal_code': 8700, 'country': 'Denmark' }",
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "Horsens"),
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("user dk created");
                }
                else
                {
                    Console.WriteLine("user with e-mail dk@m.com already exists");
                }
            }
        }
    }
}
