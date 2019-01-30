using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class PatientVM
    {
        public string CivilRegistrationNumber { get; set; }
        public string CivilStatusCode { get; set; }
        public string CountryIdentificationCode { get; set; }
        public string CountryIdentificationCodeSst { get; set; }
        public string CountryIdentificationText { get; set; }
        public string ParishDistrictCode { get; set; }
        public string ParishDistrictText { get; set; }
        public string PersonGenderCode { get; set; }
        public string PopulationDistrictCode { get; set; }
        public string PopulationDistrictText { get; set; }
        public string PractitionerIdentificationCode { get; set; }
        public string RegionalCode { get; set; }
        public string RegionalName { get; set; }
        public string SocialDistrictCode { get; set; }
        public string SocialDistrictText { get; set; }
        public string StatusCode { get; set; }

        public Address Address { get; set; }
        public PersonName PatientName { get; set; }
    }
}
