using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;
using Repositories.Interfaces;
using Services.IdentityServer;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly BefordingTestContext _context;
        private readonly IUserInfoService _userInfoService;

        public ProfileRepository(BefordingTestContext context, IUserInfoService userInfoService)
        {
            _context = context;
            _userInfoService = userInfoService;
        }
        public async Task<HospitalProfile> CreateProfile(HospitalProfileVM newProfileVm)
        {
            string userId = _userInfoService.UserId;
            var dummyProfile = new HospitalProfile()
            {
                UserId = userId,
                NameOfHospital = newProfileVm.NameOfHospital,
                Address =  newProfileVm.Address,
                Rate = newProfileVm.Rate
            };
            _context.Profiles.Add(dummyProfile);
           await _context.SaveChangesAsync();

            return dummyProfile;
        }

        public async Task<bool> DeleteProfile(int id)
        {
            string userId = _userInfoService.UserId;
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
            if (profile != null)
            {
                _context.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProfileAdmin(int id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == id);
            if (profile != null)
            {
                _context.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<HospitalProfile>> GetAllProfiles()
        {
            var result = await _context.Profiles.OrderBy(x => x.UserId).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<HospitalProfile>> GetProfilesForUser()
        {
            var userId = _userInfoService.UserId;
            var userProfileList = await _context.Profiles.Where(x => x.UserId == userId).ToListAsync();

            return userProfileList;
        }
    }
}
