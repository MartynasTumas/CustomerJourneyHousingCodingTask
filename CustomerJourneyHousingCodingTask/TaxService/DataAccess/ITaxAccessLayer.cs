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
    }
}
