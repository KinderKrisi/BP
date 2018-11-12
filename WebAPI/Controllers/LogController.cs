using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _repository;

        public LogController(ILogRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddLogFE(LogVM logVM) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddLogFE(logVM);

            return Ok();
        }

    }
}
