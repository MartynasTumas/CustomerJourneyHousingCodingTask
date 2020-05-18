using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxService.Controllers;
using TaxService.DataAccess;
using TaxService.Models;
using Xunit;

namespace Consumer
{
    public class TaxServiceTests
    {
        private string baseUrl = "https://localhost:44399"; //probably needs to be changed
        [Fact]
        private async Task GetAllTaxes()
        {
            HttpClient apiClient = new HttpClient();       
            HttpResponseMessage apiResponse = await apiClient.GetAsync($"{baseUrl}/api/Tax/GetAll");

            Assert.True(apiResponse.IsSuccessStatusCode);
        }

        [Fact]

        public async Task GetSpecificTax()
        {
            HttpClient apiClient = new HttpClient();
            HttpResponseMessage apiResponse = await apiClient.GetAsync($"{baseUrl}/api/Tax/Klaipeda/2020-07-01/daily");
            string response = await apiResponse.Content.ReadAsStringAsync();

            Assert.Equal(1.1, double.Parse(response));
        }

        [Fact]
        public async Task AddTax()
        {
            Tax newTax = new Tax()
            {
                Municipality = "Klaipeda",
                TaxAmount = 1.1,
                StartDate = new DateTime(2020,07,01),
                Type = "daily"
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(newTax), Encoding.UTF8, "application/json");
            HttpClient apiClient = new HttpClient();

            HttpResponseMessage apiResponse = await apiClient.PostAsync($"{baseUrl}/api/Tax/New", stringContent);

            Assert.True(apiResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task AddTaxFromFile()
        {

            var stringContent = new StringContent(JsonConvert.SerializeObject(@"../../TaxData.csv"), Encoding.UTF8, "application/json");
            HttpClient apiClient = new HttpClient();

            HttpResponseMessage apiResponse = await apiClient.PostAsync($"{baseUrl}/api/Tax/AddFromFile", stringContent);

            Assert.True(apiResponse.IsSuccessStatusCode);
        }
    }
}
