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
    }
}
