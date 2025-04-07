namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    public class SaleDto //GetSaleRequest
    {
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; }
        public List<SaleItemDto> Items { get; set; }
        public bool IsCancelled { get; set; }
    }
}
