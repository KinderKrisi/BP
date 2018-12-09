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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _repository;

        public ProfileController(IProfileRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<HospitalProfile>> CreateProfile(HospitalProfileVM newProfileVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProfile = await _repository.CreateProfile(newProfileVm);

            if (createdProfile == null)
            {
                ModelState.AddErrorMessage("Unable to create profile");
                return BadRequest(ModelState);
            }

            return Ok(createdProfile);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<HospitalProfile[]>> GetProfilesForUser()
        {

            var result = await _repository.GetProfilesForUser();
            if(result == null)
            {
                ModelState.AddErrorMessage("Unable to get profiles for user");
                return BadRequest(ModelState);
            }
            return Ok(result);
        }
        [Authorize(Roles = "Regular, Super, Global")]
        [HttpGet("[action]")]
        public async Task<ActionResult<HospitalProfile[]>> GetAllProfiles()
        {
            var result = await _repository.GetAllProfiles();
            if (result == null)
            {
                ModelState.AddErrorMessage("Unable to get all profiles");
                return BadRequest(ModelState);
            }
            return Ok(result);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteProfile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool deleted = await _repository.DeleteProfile(id);
            if(!deleted)
            {
                ModelState.AddErrorMessage("Unable to delete profile");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [Authorize(Roles = "Regular, Super, Global")]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteProfileAdmin(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool deleted = await _repository.DeleteProfileAdmin(id);
            if (!deleted)
            {
                ModelState.AddErrorMessage("Unable to delete profile");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpPut("[action]")]
        public async Task<ActionResult<Patient>> UpdateProfile(HospitalProfile updatedProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.UpdateProfile(updatedProfile);
            if (result == null)
            {
                ModelState.AddErrorMessage("Unable to update profile");
                return BadRequest(ModelState);
            }
            return Ok(result);
        }
    }
}