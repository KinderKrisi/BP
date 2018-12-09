using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;

namespace Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<HospitalProfile> CreateProfile(HospitalProfileVM newProfileVm);
        Task<IEnumerable<HospitalProfile>> GetProfilesForUser();
        Task<IEnumerable<HospitalProfile>> GetAllProfiles();
        Task<bool> DeleteProfile(int id);
        Task<bool> DeleteProfileAdmin(int id);
        Task<HospitalProfile> UpdateProfile(HospitalProfile updatedProfile);
    }
}
