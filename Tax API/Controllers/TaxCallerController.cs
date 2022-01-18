using Microsoft.AspNetCore.Mvc;
using Helper;
using Models;

namespace Tax_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCallerController : ControllerBase
    {
        static HttpClient client = new HttpClient();
        private List<TaxCalculator> taxCalculatorList;
        private TaxCalculator selectedCalculator;

        public TaxCallerController()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");
            taxCalculatorList = Helper.Helper.LoadTaxCalculators();
        }

        [HttpGet(Name = "GetTaxRate")]
        public async Task<IActionResult> GetTaxRate(string country, string zipCode, string calculatorId)
        {
            selectedCalculator = taxCalculatorList.FirstOrDefault(a => a.Id.ToString() == calculatorId);
            var response = await client.GetAsync(String.Format($"{selectedCalculator.Path}{selectedCalculator.TaxRateParams}",zipCode,country));

            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsStringAsync());
            else return BadRequest("There was an error ");
        }

        [HttpPost(Name = "PostCalculateTax")]
        public async Task<IActionResult> CalculateTax(string country, string zipCode, string state, string destinationCountry, string destinationZip, string destinationState, string amount, string shipping, string calculatorId)
        {
            selectedCalculator = taxCalculatorList.FirstOrDefault(a => a.Id.ToString() == calculatorId);
            var parameters = Helper.Helper.CreateTaxCalculatorParameters(country, zipCode, state, destinationCountry, destinationZip, destinationState, amount, shipping);
            var response = await client.PostAsync($"{selectedCalculator?.Path}{selectedCalculator?.TaxParams}", parameters);

            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsStringAsync());
            else return BadRequest("There was an error ");
        }
    }
}
