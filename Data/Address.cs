using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Address
    {
        public string CityName { get; set; }
        public string Floor { get; set; }
        public string HouseLetter { get; set; }
        public string HouseNumber { get; set; }
        public string PostCodeIdentifier { get; set; }
        public string SideOrDoor { get; set; }
        public string StreetName { get; set; }
        public string MunicipalityCode { get; set; }
        public string StreetNumber { get; set; }

        //In case we need to print the result
        public override string ToString()
        {
            var address = "";
            if (!string.IsNullOrWhiteSpace(StreetName))
                address = StreetName + " ";

            if (!string.IsNullOrWhiteSpace(HouseNumber))
                address += HouseNumber;

            if (!string.IsNullOrWhiteSpace(HouseLetter))
                address += HouseLetter;

            address += Environment.NewLine;

            if (!string.IsNullOrWhiteSpace(PostCodeIdentifier))
                address += PostCodeIdentifier;

            if (!string.IsNullOrWhiteSpace(CityName))
                address += " " + CityName;


            return address;
        }
    }
}
