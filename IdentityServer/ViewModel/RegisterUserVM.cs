using DataIdentityServer.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.ViewModel
{
    public class RegisterUserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public string UserType { get; set; }
        public List<SelectListItem> UserTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = UserTypeEnum.Tester.ToString(), Text = "Tester" }
        };
        [Required]
        public string AdminType { get; set; }
        public List<SelectListItem> AdminTypes { get; set; } = new List<SelectListItem>();
    }
}
