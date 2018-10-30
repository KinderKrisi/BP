using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ChangePasswordVM
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
