using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.DataAccess
{
    public class TaxAccessLayer : ITaxAccessLayer
    {
        MunicipalityTaxContext context;

        public TaxAccessLayer(MunicipalityTaxContext context)
        {
            this.context = context;
        }

        public List<Tax> GetAllTaxes()
        {
            try
            {
                List<Tax> taxes = context.Tax.ToList();
                return taxes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
