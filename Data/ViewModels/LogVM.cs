﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class LogVM
    {
        public string Severity { get; set; }
        public string Message { get; set; }
        public int? ProfileId { get; set; }
        public int? PatientId { get; set; }

    }
}
