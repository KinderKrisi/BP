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
                CivilRegistrationNumber = newPatientVM.CivilRegistrationNumber,
                CivilStatusCode = newPatientVM.CivilStatusCode,
                CountryIdentificationCode = newPatientVM.CountryIdentificationCode,
                CountryIdentificationCodeSst = newPatientVM.CountryIdentificationCodeSst,
                CountryIdentificationText = newPatientVM.CountryIdentificationText,
                ParishDistrictCode = newPatientVM.ParishDistrictCode,
                ParishDistrictText = newPatientVM.ParishDistrictText,
                PersonGenderCode = newPatientVM.PersonGenderCode,
                PersonName = newPatientVM.PersonName,
                PopulationDistrictCode = newPatientVM.PopulationDistrictCode,
                PopulationDistrictText = newPatientVM.PopulationDistrictText,
                PractitionerIdentificationCode = newPatientVM.PractitionerIdentificationCode,
                RegionalCode = newPatientVM.RegionalCode,
                RegionalName = newPatientVM.RegionalName,
                SocialDistrictCode = newPatientVM.SocialDistrictCode,
                SocialDistrictText = newPatientVM.SocialDistrictText,
                StatusCode = newPatientVM.StatusCode
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
            if (id < 0)
            {
                await _logRepository.AddLog(UserId, "incorect Id");
                return false;

            }

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
            if (id < 0)
            {
                await _logRepository.AddLog(UserId, "incorect Id");
                return false;
            }

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

        public async Task<Patient> UpdatePatient(Patient updatedPatient)
        {
            try
            {
                var patientToUpdate = await _context.Patients.FirstOrDefaultAsync(x => x.Id == updatedPatient.Id);
                patientToUpdate = updatedPatient;
                await _context.SaveChangesAsync();
                return patientToUpdate;
            }
            catch (Exception ex)
            {
                await _logRepository.AddLog(UserId, ex.Message, updatedPatient.Id, true);
                return null;
            }
        }
    }
}
