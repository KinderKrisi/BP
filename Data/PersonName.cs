using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class PersonName
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public override string ToString()
        {
            var name = "";
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                name += FirstName + " ";
            }
            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                name += MiddleName + " ";
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                name += LastName;
            }

            return name;
        }
    }
}
