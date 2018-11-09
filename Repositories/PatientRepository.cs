using Data;
using Data.ViewModels;
using Repositories.Interfaces;
using Services.IdentityServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly BefordingTestContext _context;
        private readonly IUserInfoService _userInfoService;

        public PatientRepository(BefordingTestContext context, IUserInfoService userInfoService)
        {
            _context = context;
            _userInfoService = userInfoService;
        }

        public async Task<Patient> CreatePatient(PatientVM newPatientVM)
        {
            string userId = _userInfoService.UserId;
            var dummyPatient = new Patient()
            {
                UserId = userId,
                Address = newPatientVM.Address,
                Name = newPatientVM.Name
            };
            _context.Patients.Add(dummyPatient);
            await _context.SaveChangesAsync();

            return dummyPatient;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsForUser()
        {
            string userId = _userInfoService.UserId;
            var userPatientList = await _context.Patients.Where(x => x.UserId == userId).ToListAsync();

            return userPatientList;
        }
    }
}
