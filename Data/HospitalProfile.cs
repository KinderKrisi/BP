using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class HospitalProfile
    {
        public int? Id { get; set; }
        public string NameOfHospital { get; set; }
        public string Address { get; set; }
        public float Rate { get; set; }
        public string UserId { get; set; }
    }
}
