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
        private readonly ILogRepository _logRepository;

        private string UserId;

        public PatientRepository(BefordingTestContext context, IUserInfoService userInfoService, ILogRepository logRepository)
        {
            _context = context;
            _userInfoService = userInfoService;
            _logRepository = logRepository;

            UserId = _userInfoService.UserId;
        }

        public async Task<Patient> CreatePatient(PatientVM newPatientVM)
        {
            var dummyPatient = new Patient()
            {
                UserId = UserId,
                Address = newPatientVM.Address,
                Name = newPatientVM.Name
            };
            try
            {
                _context.Patients.Add(dummyPatient);
                await _context.SaveChangesAsync();
                return dummyPatient;
            }
            catch(Exception ex)
            {
                var newLog = new Log()
                {
                    Severity = "Error",
                    Message = ex.Message,
                    UserId = UserId,
                    TimeOfOccurrence = DateTime.Now
                };
                await _logRepository.AddLog(newLog);
                return null;
            }
            
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsForUser()
        {
            try
            {
                var userPatientList  = await _context.Patients.Where(x => x.UserId == UserId).ToListAsync();
                return userPatientList;
            }
            catch (Exception ex)
            {
                var newLog = new Log()
                {
                    Severity = "Error",
                    Message = ex.Message,
                    UserId = UserId,
                    TimeOfOccurrence = DateTime.Now
                };
                await _logRepository.AddLog(newLog);
                return null;
            }
        }

        public async Task<IEnumerable<Patient>> GetAllUserPatientsAdmin(string userId)
        {
            try
            {
               var userPatientListAdmin = await _context.Patients.Where(x => x.UserId == userId).ToListAsync();
                return userPatientListAdmin;
            }
            catch (Exception ex)
            {
                var newLog = new Log()
                {
                    Severity = "Error",
                    Message = ex.Message,
                    UserId = UserId,
                    TimeOfOccurrence = DateTime.Now
                };
                await _logRepository.AddLog(newLog);
                return null;
            }
        }
    }
}
