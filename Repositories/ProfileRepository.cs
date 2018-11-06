using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly BefordingTestContext _context;
        public ProfileRepository(BefordingTestContext context)
        {
            _context = context;
        }
        public async Task<HospitalProfile> CreateProfile(HospitalProfileVM newProfileVm)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == newProfileVm.UserId);
            var dummyProfile = new HospitalProfile()
            {
                
                User = user,
                NameOfHospital = newProfileVm.NameOfHospital,
                Address =  newProfileVm.Address,
                Rate = newProfileVm.Rate
            };
            _context.Profiles.Add(dummyProfile);
           await _context.SaveChangesAsync();

            return dummyProfile;
        }
    }
}
