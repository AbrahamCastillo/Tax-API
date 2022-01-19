using Xunit;
using Tax_API.Controllers;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaxAPITests
{
    public class TaxApiTests
    {
        [Fact]
        public async Task GetTaxRateTestsAsync()
        {
            TaxCallerController taxCallerController = new TaxCallerController();
            var taxRate = await taxCallerController.GetTaxRate("US", "91911", "1");
            Assert.NotNull(taxRate);
            var okResult = taxRate as OkObjectResult;
            Root taxRatesObjects = Helper.Helper.LoadTaxRate(okResult.Value.ToString());
            Assert.NotNull(taxRatesObjects);
            Assert.True(taxRatesObjects.rate.country == "US");
        }

        [Fact]
        public async void GetTaxRateTestsFalseAsync()
        {
            TaxCallerController taxCallerController = new TaxCallerController();
            var taxRate = await taxCallerController.GetTaxRate("", "", "");
            var okResult = taxRate as OkObjectResult;
            Assert.Null(okResult);
        }

        [Fact]
        public async void CalculateTaxTests()
        {
            TaxCallerController taxCallerController = new TaxCallerController();
            var taxCalculation = await taxCallerController.CalculateTax("US", "92093", "CA","US","90002","CA","15","1.5","1");
            Assert.NotNull(taxCalculation);
            var okResult = taxCalculation as OkObjectResult;
            TaxCalculation taxRatesObjects = Helper.Helper.LoadTaxCalculation(okResult.Value.ToString());
            Assert.NotNull(taxRatesObjects);
            Assert.True(taxRatesObjects.tax.jurisdictions.country == "US");
        }

        public async void CalculateTaxFalseTests()
        {
            TaxCallerController taxCallerController = new TaxCallerController();
            var taxCalculation = await taxCallerController.CalculateTax("", "", "", "", "", "", "", "", "");
            var okResult = taxCalculation as OkObjectResult;
            Assert.Null(okResult);
        }
    }
}
