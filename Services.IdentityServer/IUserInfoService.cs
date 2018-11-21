using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IdentityServer
{
    public interface IUserInfoService
    {
        string UserId { get; set; }
        string Role { get; set; }
    }
}
