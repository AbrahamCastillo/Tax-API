using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Helper
{
    public class Helper
    {
        public static List<TaxCalculator> LoadTaxCalculators()
        {
            List<TaxCalculator> listCalculators = new List<TaxCalculator>();
            using (StreamReader r = new StreamReader("./Tax Calculators/TaxCalculators.json"))
            {
                string json = r.ReadToEnd();
                listCalculators = JsonConvert.DeserializeObject<List<TaxCalculator>>(json);
            }
            return listCalculators;
        }

        public static FormUrlEncodedContent CreateTaxCalculatorParameters(string country, string zipCode, string state, string destinationCountry, string destinationZip, string destinationState, string amount, string shipping)
        {
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
            return content;
        }
    }
}