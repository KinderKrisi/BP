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
        {
        }

        public DbSet<HospitalProfile> Profiles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
