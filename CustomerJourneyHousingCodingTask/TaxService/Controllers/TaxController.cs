using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxService.DataAccess;
using TaxService.Models;

namespace TaxService.Controllers
{
    public class TaxController : ControllerBase
    {
        ITaxAccessLayer tax;
        public TaxController(ITaxAccessLayer tax)
        {
            this.tax = tax;
        }

        [HttpGet]
        [Route("api/Tax/GetAll")]
        public List<Tax> GetAllTaxes()
        {
            return tax.GetAllTaxes();
        }

        [HttpGet]
        [Route("api/Tax/{municipality}/{date}/{sortby}")]
        public double GetSpecificTax(string municipality, string date, string sortBy)
        {
            DateTime parsedDate = new DateTime();
            try
            {
                parsedDate = DateTime.Parse(date);
            }
            catch
            {
                return 38808;
            }

            if (sortBy == "yearly" || sortBy == "monthly" || sortBy == "weekly" || sortBy == "daily")
                return tax.GetSpecificTax(municipality, parsedDate, sortBy);
            else
                return 38808;
        }

        [HttpPost]
        public void AddNewTax(string municipality, string taxAmount, string startDate, string endDate)
        {

        }
    }
}