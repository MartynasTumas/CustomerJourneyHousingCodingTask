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

        public double GetSpecificTax(string municipality, DateTime date, string sortBy)
        {
            try
            {
                switch (sortBy)
                {
                    case "yearly":
                        {
                            Tax tax = context.Tax.Where(x => x.Type.ToLower() == sortBy.ToLower() && x.Municipality == municipality).FirstOrDefault();

                            if (tax != null)
                                return tax.TaxAmount;
                            break;
                        }
                    case "monthly":
                        {
                            Tax tax = context.Tax.Where(x => x.Type.ToLower() == sortBy.ToLower() && x.Municipality == municipality &&
                            x.StartDate.Month == date.Month).FirstOrDefault();

                            if (tax != null)
                                return tax.TaxAmount;
                            break;
                        }
                    case "weekly":
                        {
                            break;
                        }
                    case "daily":
                        {
                            Tax tax = context.Tax.Where(x => x.Type.ToLower()==sortBy.ToLower() && x.Municipality == municipality &&
                            x.StartDate.DayOfYear == date.DayOfYear).FirstOrDefault();
                            
                            if (tax != null)
                                return tax.TaxAmount;
                            break;
                        }
                }

                return 38808;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewTax(string municipality, string taxAmount, string startDate, string endDate)
        {

        }
    }
}
