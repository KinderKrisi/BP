using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Log
    {
        public int Id { get; set; }
        public string Severity { get; set; }
        public DateTime TimeOfOccurrence { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
    }
}
