namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    public class SaleRequest //GetSaleRequest
    {
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; } = string.Empty;
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleItemRequest> Items { get; set; }
        public bool IsCancelled { get; set; }
    }
}
