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
        /*{
    "municipality": "Vilnius",
    "taxAmount": 1.2,
    "startDate": "2020-07-01",
    "endDate": "2020-07-31",
    "type": "monthly"
  }*/
        [HttpPost]
        [Route("api/Tax/New")]
        public string AddNewTax([FromBody]Tax newTax)
        {

            if (string.IsNullOrEmpty(newTax.Municipality))
                throw new Exception();


            if (string.IsNullOrEmpty(newTax.Type))
                throw new Exception();

            if (newTax.StartDate.Ticks == 0)
                throw new Exception();

            if (newTax.TaxAmount == 0)
                throw new Exception();

            try
            {
                if (ModelState.IsValid)
                {
                    tax.AddNewTax(newTax);

                    return "New tax added";
                }
            }
            catch
            {
                throw;
            }

            throw new Exception();
        }
    }
}