using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientController (IPatientRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<Patient[]>> GetAllPatientsForUser()
        {
            var userPatientList = await _repository.GetAllPatientsForUser();
            if(userPatientList == null)
            {
                return BadRequest();
            }
            return Ok(userPatientList);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Patient>> CreatePatient(PatientVM newPatientVM)
        {
            var newPatient = await _repository.CreatePatient(newPatientVM);

            if(newPatient == null)
            {
                return BadRequest();
            }
            return Ok(newPatient);
        }
    }
}
