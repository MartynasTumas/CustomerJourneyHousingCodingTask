using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.DataAccess
{
    public interface ITaxAccessLayer
    {
        List<Tax> GetAllTaxes();

        double GetSpecificTax(string municipality, DateTime date, string sortBy);

        void AddNewTax(string municipality, string taxAmount, string startDate, string endDate);
    }
}
