using DataIdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataIdentityServer
{
    public class IdentityServerDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityServerDb(DbContextOptions<IdentityServerDb> options)
             : base(options)
        {
        }
    }
}
