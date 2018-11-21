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
                await _logRepository.AddLog(UserId, ex.Message);
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
                await _logRepository.AddLog(UserId, ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAdmin()
        {
            try
            {
               var userPatientListAdmin = await _context.Patients.OrderBy(x => x.UserId).ToListAsync();
                return userPatientListAdmin;
            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message);
                return null;
            }
        }

        public async Task<bool> DeletePatient(int id)
        {
            try
            {
                var userToDelete = await _context.Patients.FirstOrDefaultAsync(x => x.UserId == UserId && x.Id == id);
                _context.Patients.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePatientAdmin(int id)
        {
            try
            {
                var userToDelete = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
                _context.Patients.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message);
                return false;
            }
        }
    }
}
