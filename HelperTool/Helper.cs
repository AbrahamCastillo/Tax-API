using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace Helper
{
    public class Helper
    {
        private static string solutionPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        public static List<TaxCalculator> LoadTaxCalculators()
        {
            List<TaxCalculator> listCalculators = new List<TaxCalculator>();
            using (StreamReader r = new StreamReader($"{solutionPath}Tax API/../../Tax API/Tax Calculators/TaxCalculators.json"))
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

        public static FormUrlEncodedContent CreateTaxClientParameters(string country, string zipCode, string state, string destinationCountry, string destinationZip, string destinationState, string amount, string shipping, string calculatorId = "1")
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("country", country),
                new KeyValuePair<string, string>("zipCode", zipCode),
                new KeyValuePair<string, string>("state", state),
                new KeyValuePair<string, string>("destinationCountry", destinationCountry),
                new KeyValuePair<string, string>("destinationZip", destinationZip),
                new KeyValuePair<string, string>("destinationState", destinationState),
                new KeyValuePair<string, string>("amount", amount),
                new KeyValuePair<string, string>("shipping", shipping),
                new KeyValuePair<string, string>("calculatorId", calculatorId)
            });
            return content;
        }

        public static Root LoadTaxRate(string jsonTaxRate)
        {
            var result = JsonConvert.DeserializeObject<Root>(jsonTaxRate);
            return result;
        }
        public static TaxCalculation LoadTaxCalculation(string jsonTaxRate)
        {
            var result = JsonConvert.DeserializeObject<TaxCalculation>(jsonTaxRate);
            return result;
        }

        public static string ObjectToText(object objectData, string dataText = "")
        {
            string textTaxRate = dataText;
            foreach (PropertyInfo propertyInfo in objectData.GetType().GetProperties())
            {
                if (propertyInfo.GetValue(objectData, null) is Jurisdictions)
                {
                    textTaxRate += ObjectToText(propertyInfo.GetValue(objectData, null), textTaxRate);
                }
                else
                {
                    textTaxRate += $"{propertyInfo.Name}: {propertyInfo.GetValue(objectData, null)} \r\n";
                }
                
            }
            return textTaxRate;
        }
    }
}