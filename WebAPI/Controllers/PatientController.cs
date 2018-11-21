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
using Nobly.Extensions.ModelStateDictionary;


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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPatientList = await _repository.GetAllPatientsForUser();
            if(userPatientList == null)
            {
                ModelState.AddErrorMessage("Unable to get data");
                return BadRequest(ModelState);
            }
            return Ok(userPatientList);
            
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Patient>> CreatePatient(PatientVM newPatientVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPatient = await _repository.CreatePatient(newPatientVM);

            if(newPatient == null)
            {
                ModelState.AddErrorMessage("Unable to create Patient");
                return BadRequest(ModelState);
            }
            return Ok(newPatient);
        }
        [Authorize(Roles = "Regular, Super, Global")]
        [HttpGet("[action]")]
        public async Task<ActionResult<Patient[]>> GetAllPatientsAdmin()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userPatientListAdmin = await _repository.GetAllPatientsAdmin();

            if(userPatientListAdmin == null)
            {
                ModelState.AddErrorMessage("Unable to get data");
                return BadRequest(ModelState);
            }
            return Ok(userPatientListAdmin);
        }

        [Authorize(Roles = "Regular, Super, Global")]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Patient>> DeletePatientAdmin(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDeleted = await _repository.DeletePatientAdmin(id);
            if (!userDeleted)
            {
                ModelState.AddErrorMessage("Unable to delete patient");
                return BadRequest(ModelState);
            }

            return Ok();
        }
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDeleted = await _repository.DeletePatient(id);
            if (!userDeleted)
            {
                ModelState.AddErrorMessage("Unable to delete patient");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
