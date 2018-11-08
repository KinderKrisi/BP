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
            var createdProfile = await _repository.CreateProfile(newProfileVm);

            if (createdProfile == null)
            {
                return BadRequest();
            }

            return Ok(createdProfile);
        }
        [Authorize]
        [HttpGet]
        public IActionResult bla()
        {
            var list = new List<string>() { "Hello can you hear me I am server" };
            return Ok(list);
        }
    }
}