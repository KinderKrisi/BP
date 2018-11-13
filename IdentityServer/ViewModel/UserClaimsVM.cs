using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.ViewModel
{
    public class UserClaimsVM
    {
        public string UserID { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public string NewClaimKey1 { get; set; }
        public string NewClaimKey2 { get; set; }
        public string NewClaimKey3 { get; set; }
        public string NewClaimKey4 { get; set; }
        public string NewClaimKey5 { get; set; }
        public string NewClaimValue1 { get; set; }
        public string NewClaimValue2 { get; set; }
        public string NewClaimValue3 { get; set; }
        public string NewClaimValue4 { get; set; }
        public string NewClaimValue5 { get; set; }
    }
}
