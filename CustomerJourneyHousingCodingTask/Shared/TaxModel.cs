using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class TaxModel
    {
        public string City { get; set; }
        public double Tax { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
