using DataIdentityServer;
using DataIdentityServer.Enum;
using DataIdentityServer.Models;
using IdentityModel;
using IdentityServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace IdentityServer.Controllers
{ 
    [Authorize(Roles = "Super, Global")]
    public class UsersController : Controller
    {
        private readonly IdentityServerDb _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(IdentityServerDb context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var vm = new UsersVM();
            vm.Users = _context.Users.Select(x =>
            new ApplicationUserVm
            {
                ApplicationUser = new ApplicationUser
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email
                }
            }).ToList();

            foreach (var user in vm.Users)
            {
                var userRoles = await _userManager.GetRolesAsync(user.ApplicationUser);
                user.Roles = string.Join(",", userRoles.Select(x => x));
            }

            return View(vm);
        }

        public async Task<IActionResult> AddUser()
        {
            var vm = new RegisterUserVM();
            vm.AdminTypes = new List<SelectListItem>()
            {
            new SelectListItem { Value = AdminTypeEnum.None.ToString(), Text = "None" },
            new SelectListItem { Value = AdminTypeEnum.Regular.ToString(), Text = "Regular" },
            };
            var user = await _userManager.FindByIdAsync(User.FindFirst("sub").Value);
            if (await _userManager.IsInRoleAsync(user, "Global"))
            {
                vm.AdminTypes.Add(new SelectListItem { Value = AdminTypeEnum.Global.ToString(), Text = "Super" });
                vm.AdminTypes.Add(new SelectListItem { Value = AdminTypeEnum.Global.ToString(), Text = "Global" });
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterUserVM userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userVm.Email);
            if (user == null)
            {
                var applicationUser = new ApplicationUser();
                applicationUser.Email = userVm.Email;
                applicationUser.UserName = userVm.UserName;
                var result = await _userManager.CreateAsync(applicationUser, "Test123"); //Todo change to real pass
                if (result.Succeeded)
                {
                    user = await _userManager.FindByIdAsync(applicationUser.Id);

                    if (userVm.AdminType == AdminTypeEnum.Global.ToString())
                    {
                        if (await IsLoggedInUserGlobalAdmin())
                        {
                            var roleResultGlobal = await _userManager.AddToRoleAsync(user, userVm.AdminType);
                            if (!roleResultGlobal.Succeeded) return BadRequest($"Error adding {userVm.AdminType} admin role to user");
                        }
                        else
                        {
                            await _userManager.DeleteAsync(user);
                            return RedirectToAction("AccessDenied", "Authorization");
                        }
                    }

                    var roleResult = await _userManager.AddToRoleAsync(user, userVm.AdminType);
                    if (!roleResult.Succeeded) return BadRequest("Error adding role to user");
                    var claimResult = await _userManager.AddClaimsAsync(user,
                        new List<Claim> {
                            new Claim(JwtClaimTypes.Email, user.Email),
                            new Claim("usertype",userVm.UserType),
                        });
                    if (!claimResult.Succeeded) return BadRequest("Error adding usertype and section to user");
                    //Todo: Confirmation mail
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationEmail = Url.Action("ConfirmEmailAddress", "Home",
                        new { token = token, email = user.Email }, Request.Scheme);
                    //System.IO.File.WriteAllText("confirmationLink.txt", confirmationEmail);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError($"{error.Code}", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditUserClaims([FromRoute] string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");
            var claims = await _userManager.GetClaimsAsync(user);

            if (!await IsLoggedInUserGlobalAdmin()) //Either global or super
            {
                if (await _userManager.IsInRoleAsync(user, "Global") || await _userManager.IsInRoleAsync(user, "Super"))
                    return RedirectToAction("AccessDenied", "Authorization");
            }
            return View(new UserClaimsVM
            {
                Claims = claims.ToList(),
                UserID = user.Id
            });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> AddUserClaims([FromRoute] string id)
        {
            var claims = new List<Claim>();
            for (int i = 0; i < 6; i++)
            {
               claims.Add(new Claim("", ""));
            }
            return View(new UserClaimsVM
            {
                Claims = claims,
                UserID = id
            });
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> AddUserClaims([FromRoute] string id, UserClaimsVM vm)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            if (!await IsLoggedInUserGlobalAdmin()) //Either global or super
            {
                if (await _userManager.IsInRoleAsync(user, "Global") || await _userManager.IsInRoleAsync(user, "Super"))
                    return RedirectToAction("AccessDenied", "Authorization");
            }

            var claims = new List<Claim>();
            if (!string.IsNullOrWhiteSpace(vm.NewClaimKey1)) claims.Add(new Claim(vm.NewClaimKey1, vm.NewClaimValue1));
            if (!string.IsNullOrWhiteSpace(vm.NewClaimKey2)) claims.Add(new Claim(vm.NewClaimKey2, vm.NewClaimValue2));
            if (!string.IsNullOrWhiteSpace(vm.NewClaimKey3)) claims.Add(new Claim(vm.NewClaimKey3, vm.NewClaimValue3));
            if (!string.IsNullOrWhiteSpace(vm.NewClaimKey4)) claims.Add(new Claim(vm.NewClaimKey4, vm.NewClaimValue4));
            if (!string.IsNullOrWhiteSpace(vm.NewClaimKey5)) claims.Add(new Claim(vm.NewClaimKey5, vm.NewClaimValue5));

            if (claims.Any())
            {
                await _userManager.AddClaimsAsync(user, claims);
            }
            return RedirectToAction("EditUserClaims", "Users", new { id = user.Id });
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> DeleteUserClaim([FromRoute] string id, string type, string value)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");
            var claim = new Claim(type, value);
            await _userManager.RemoveClaimAsync(user, claim);
            return RedirectToAction("EditUserClaims", "Users", new { id = user.Id });
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trialfinderUser = await _userManager.FindByIdAsync(id);
            if (trialfinderUser == null)
            {
                return NotFound();
            }
            if (await IsUserGlobalOrSuperAdmin(trialfinderUser))
            {
                if (await IsLoggedInUserGlobalAdmin())
                {
                    await _userManager.DeleteAsync(trialfinderUser);
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Authorization");
                }
            }
            if (await IsUserGlobalOrSuperAdmin(trialfinderUser))
            {
                if (await IsLoggedInUserGlobalAdmin())
                {
                    await _userManager.DeleteAsync(trialfinderUser);
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Authorization");
                }
            }
            await _userManager.DeleteAsync(trialfinderUser);
            return RedirectToAction("Index");
        }

        private async Task<bool> IsUserGlobalOrSuperAdmin(ApplicationUser user)
        {
            if (await _userManager.IsInRoleAsync(user, "Global"))
                return true;
            if (await _userManager.IsInRoleAsync(user, "Super"))
                return true;
            return false;
        }
        private async Task<bool> IsLoggedInUserGlobalAdmin()
        {
            var currentLoggedInUser = await _userManager.FindByIdAsync(User.FindFirst("sub").Value);
            return await _userManager.IsInRoleAsync(currentLoggedInUser, "Global");
        }

    }
}