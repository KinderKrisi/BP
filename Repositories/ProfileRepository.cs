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
        private readonly ILogRepository _logRepository;

        private string UserId;

        public ProfileRepository(BefordingTestContext context, IUserInfoService userInfoService, ILogRepository logRepository)
        {
            _context = context;
            _userInfoService = userInfoService;
            _logRepository = logRepository;


            UserId = _userInfoService.UserId;

        }
        public async Task<HospitalProfile> CreateProfile(HospitalProfileVM newProfileVm)
        {
            var dummyProfile = new HospitalProfile()
            {
                UserId = UserId,
                NameOfHospital = newProfileVm.NameOfHospital,
                Address =  newProfileVm.Address,
                Rate = newProfileVm.Rate
            };
            try
            {
                _context.Profiles.Add(dummyProfile);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message);
                return null;
            }
            return dummyProfile;
        }

        public async Task<bool> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == UserId && x.Id == id);
            if (profile != null)
            {
                try
                {
                    _context.Remove(profile);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await _logRepository.AddLog(UserId, ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> DeleteProfileAdmin(int id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == id);
            if (profile != null)
            {
                try
                {
                    _context.Remove(profile);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await _logRepository.AddLog(UserId, ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<HospitalProfile>> GetAllProfiles()
        {
            try
            {
                var result = await _context.Profiles.OrderBy(x => x.UserId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message);
                return null;
            }

        }

        public async Task<IEnumerable<HospitalProfile>> GetProfilesForUser()
        {
            try
            {
                var userProfileList = await _context.Profiles.Where(x => x.UserId == UserId).ToListAsync();
                return userProfileList;
            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message);
                return null;
            }
        }
    }
}
