using Data;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> CreatePatient(PatientVM newPatientVM);
        Task<IEnumerable<Patient>> GetAllPatientsForUser();
        Task<IEnumerable<Patient>> GetAllPatientsAdmin();
        Task<bool> DeletePatient(int id);
        Task<bool> DeletePatientAdmin(int id);
    }
}
