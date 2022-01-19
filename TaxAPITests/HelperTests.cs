using Xunit;
using Models;
using Helper;
using System.Collections.Generic;

namespace TaxAPITests
{
    public class HelperTests
    {
        [Fact]
        public void ReadTaxCalculatorsTest()
        {
            var calculators = Helper.Helper.LoadTaxCalculators();
            Assert.NotNull(calculators);
            Assert.IsType<List<TaxCalculator>>(calculators);
            Assert.True(calculators.Count > 0);
        }

        [Fact]
        public void CreateTaxCalculatorParametersTest()
        {
            var parameters = Helper.Helper.CreateTaxCalculatorParameters("Test Country", "Test zipCode", "Test state", "Test destinationCountry", "Test destinationZip", "Test destinationState", "Test amount", "Test shipping");
            Assert.NotNull(parameters);
        }

        [Fact]
        public void CreateTaxClientParametersTest()
        {
            var parameters = Helper.Helper.CreateTaxClientParameters("Test Country", "Test zipCode", "Test state", "Test destinationCountry", "Test destinationZip", "Test destinationState", "Test amount", "Test shipping", "1");
            Assert.NotNull(parameters);
        }

        [Fact]
        public void LoadTaxRateFromResponseTest()
        {
            var taxRate = Helper.Helper.LoadTaxRate("{\"rate\":{\"city\":\"CHULA VISTA\",\"city_rate\":\"0.0\",\"combined_district_rate\":\"0.015\",\"combined_rate\":\"0.0875\",\"country\":\"US\",\"country_rate\":\"0.0\",\"county\":\"SAN DIEGO\",\"county_rate\":\"0.01\",\"freight_taxable\":false,\"state\":\"CA\",\"state_rate\":\"0.0625\",\"zip\":\"91911\"}}");
            Assert.NotNull(taxRate);
        }

        [Fact]
        public void LoadTaxCalculationFromResponseTest()
        {
            var taxRate = Helper.Helper.LoadTaxCalculation("{\"tax\":{\"amount_to_collect\":1.54,\"freight_taxable\":false,\"has_nexus\":true,\"jurisdictions\":{\"city\":\"LYNWOOD\",\"country\":\"US\",\"county\":\"LOS ANGELES COUNTY\",\"state\":\"CA\"},\"order_total_amount\":16.5,\"rate\":0.1025,\"shipping\":1.5,\"tax_source\":\"destination\",\"taxable_amount\":15.0}}");
            Assert.NotNull(taxRate);
        }

        [Fact]
        public void ObjectToTextTest()
        {
            string expectedText = "Id: 1 \r\nName: Test \r\nPath: Test Path \r\nTaxParams: Test Params \r\nTaxRateParams: Test Rate Params \r\n";
            TaxCalculator taxCalculator = new TaxCalculator
            {
                Id = 1,
                Name = "Test",
                Path = "Test Path",
                TaxParams = "Test Params",
                TaxRateParams = "Test Rate Params"
            };
            var textObject = Helper.Helper.ObjectToText(taxCalculator);
            Assert.NotNull(textObject);
            Assert.Equal(textObject, expectedText);
        }
    }
}