using DataIdentityServer;
using DataIdentityServer.Enum;
using DataIdentityServer.Models;
using IdentityModel;
using IdentityServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Nobly.Extensions.ModelStateDictionary;

namespace IdentityServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserModificationController : Controller
    {
        private readonly IdentityServerDb _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserModificationController(IdentityServerDb context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Get method");
            } 
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return NotFound("User not found");

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return NotFound("User not found");
            
            await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);
            return RedirectToAction("Index");
        }
    }
}