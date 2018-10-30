using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // POST api/user
        [HttpPost("[action]")]
        public async Task<ActionResult<User>> CreateUser(UserVM newUser)
        {

            var user = await _userRepository.CreateUser(newUser);
            return CreatedAtRoute("GetUser", new User { Id = user.Id }, user);
        }

        // PUT api/values/5
        [HttpPut("[action]")]
        public async Task<ActionResult<User>> ChangePassword(ChangePasswordVM changePasswordVm)
        {
            var user = _userRepository.ChangePassword(changePasswordVm);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
