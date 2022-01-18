using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TaxCalculator
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Path { get; set; }

        public string? TaxParams { get; set; }

        public string? TaxRateParams { get; set; }
    }
}
