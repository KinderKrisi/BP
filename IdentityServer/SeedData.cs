﻿using DataIdentityServer;
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

namespace IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IdentityServerDb>();
                //context.Database.Migrate(); //comment this if you don't want to have the latest migration

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var roleType in Enum.GetValues(typeof(AdminTypeEnum)))
                {
                    var roleName = roleType.ToString();
                    var role = roleMgr.FindByNameAsync(roleName).Result;
                    if (role == null)
                    {
                        var result = roleMgr.CreateAsync(new IdentityRole(roleName)).Result;
                    }
                }

                var st = userMgr.FindByEmailAsync("mk@m.com").Result;
                if (st == null)
                {
                    st = new ApplicationUser
                    {
                        UserName = "mk",
                        Email = "mk@m.com"
                    };
                    var result = userMgr.CreateAsync(st, "123456").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    var type = Enum.GetName(typeof(AdminTypeEnum), AdminTypeEnum.Global);
                    if (!userMgr.IsInRoleAsync(st, type).Result)
                    {
                        result = userMgr.AddToRoleAsync(st, type).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }

                    result = userMgr.AddClaimsAsync(st, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Martin Krisko"),
                        new Claim(JwtClaimTypes.GivenName, "Martin"),
                        new Claim(JwtClaimTypes.FamilyName, "Krisko"),
                        new Claim(JwtClaimTypes.Email, "mk@m.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'CLaus Cortens Gade 5', 'locality': 'Horsens', 'postal_code': 8700, 'country': 'Denmark' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
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

                var dk = userMgr.FindByEmailAsync("dk@m.com").Result;
                if (dk == null)
                {
                    dk = new ApplicationUser
                    {
                        UserName = "dk",
                        Email = "dk@m.com"
                    };
                    var result = userMgr.CreateAsync(dk, "123456").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    var type = Enum.GetName(typeof(AdminTypeEnum), AdminTypeEnum.Global);
                    if (!userMgr.IsInRoleAsync(dk, type).Result)
                    {
                        result = userMgr.AddToRoleAsync(dk, type).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }


                    result = userMgr.AddClaimsAsync(dk, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "David Kuts"),
                        new Claim(JwtClaimTypes.GivenName, "David"),
                        new Claim(JwtClaimTypes.FamilyName, "Kuts"),
                        new Claim(JwtClaimTypes.Email, "dk@m.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Chr M Østergaards Vej 1A', 'locality': 'Horsens', 'postal_code': 8700, 'country': 'Denmark' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
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
