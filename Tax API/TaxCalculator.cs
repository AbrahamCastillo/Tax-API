namespace Tax_API
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
