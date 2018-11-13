using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class OwinConfiguration
    {
        public string ClientOrigin { get; set; }
        public string IdentityOrigin { get; set; }
    }
}
