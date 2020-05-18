using System;
using System.Collections.Generic;

namespace TaxService.Models
{
    public partial class Tax
    {
        public string Municipality { get; set; }
        public double TaxAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
    }
}
