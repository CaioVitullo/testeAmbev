namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleQueryParametersServiceDto
    {
        public int Page { get; set; } = 1; // Default page number
        public int Size { get; set; } = 10; // Default page size
        public string Order { get; set; } = string.Empty; // Ordering
        public string Customer { get; set; } = string.Empty;// Filtering by customer
        public decimal? MinTotalAmount { get; set; } // Filtering by minimum total amount
        public decimal? MaxTotalAmount { get; set; } // Filtering by maximum total amount
    }
}
