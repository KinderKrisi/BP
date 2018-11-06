using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BefordingTestContext : DbContext
    {
        public BefordingTestContext(DbContextOptions<BefordingTestContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<HospitalProfile> Profiles { get; set; }
    }
}
