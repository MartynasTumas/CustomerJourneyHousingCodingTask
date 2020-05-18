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
    //  [Route("api/[controller]")]
    //  [ApiController]
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
    }
}