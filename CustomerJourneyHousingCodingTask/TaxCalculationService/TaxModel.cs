using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculationService
{
    public class TaxModel
    {
        private string City { get; set; }
        private double Tax { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
    }
}
