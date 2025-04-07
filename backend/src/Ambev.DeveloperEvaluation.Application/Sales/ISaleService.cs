
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public interface ISaleService
    {
        Task<SaleServiceDto> CreateSale(SaleServiceDto sale);
        Task<SaleServiceDto> GetSale(int saleNumber);
        Task<List<SaleServiceDto>> GetAllSales(SaleQueryParametersServiceDto queryParameters);
        Task<SaleServiceDto> UpdateSale(SaleServiceDto sale);
        Task CancelSale(int saleNumber);
    }
}
