using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Linq;

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
            LoadTaxCalculators();
        }

        private void LoadTaxCalculators()
        {
            using (StreamReader r = new StreamReader("./Tax Calculators/TaxCalculators.json"))
            {
                string json = r.ReadToEnd();
                taxCalculatorList = JsonConvert.DeserializeObject<List<TaxCalculator>>(json);
            }
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
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from_country", country),
                new KeyValuePair<string, string>("from_zip", zipCode),
                new KeyValuePair<string, string>("from_state", state),
                new KeyValuePair<string, string>("to_country", destinationCountry),
                new KeyValuePair<string, string>("to_zip", destinationZip),
                new KeyValuePair<string, string>("to_state", destinationState),
                new KeyValuePair<string, string>("amount", amount),
                new KeyValuePair<string, string>("shipping", shipping)
            });
            var response = await client.PostAsync($"{selectedCalculator.Path}{selectedCalculator.TaxParams}",content);

            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsStringAsync());
            else return BadRequest("There was an error ");
        }
    }
}
