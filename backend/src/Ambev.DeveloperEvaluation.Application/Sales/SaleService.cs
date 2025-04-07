using Ambev.DeveloperEvaluation.Common.Helper.Query;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository; // Assuming you have a repository pattern
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleServiceDto> CreateSale(SaleServiceDto sale)
        {
            // Calculate total sale amount and apply business rules here
            sale.TotalSaleAmount = sale.Items.Sum(item => item.TotalAmount);
            await _saleRepository.AddAsync(sale);
            return _mapper.Map<SaleServiceDto>(sale);
        }

        public async Task<SaleServiceDto> GetSale(int saleNumber)
        {
            var sale = await _saleRepository.GetByIdAsync(saleNumber);
            return _mapper.Map<SaleServiceDto>(sale);
        }

        public async Task<List<SaleServiceDto>> GetAllSales(SaleQueryParametersServiceDto queryParameters)
        {
            var query = _saleRepository.Query(); // Get IQueryable from your repository

            // Apply filtering
            if (!string.IsNullOrEmpty(queryParameters.Customer))
            {
                query = query.Where(s => s.Customer.Contains(queryParameters.Customer));
            }
            if (queryParameters.MinTotalAmount.HasValue)
            {
                query = query.Where(s => s.TotalSaleAmount >= queryParameters.MinTotalAmount);
            }
            if (queryParameters.MaxTotalAmount.HasValue)
            {
                query = query.Where(s => s.TotalSaleAmount <= queryParameters.MaxTotalAmount);
            }

            // Apply ordering
            if (!string.IsNullOrEmpty(queryParameters.Order))
            {
                var orderParams = queryParameters.Order.Split(',').Select(o => o.Trim()).ToList();
                foreach (var orderParam in orderParams)
                {
                    var isDescending = orderParam.EndsWith("desc");
                    var propertyName = isDescending ? orderParam[0..^4].Trim() : orderParam.Trim();

                    query = query.OrderBy<SaleServiceDto>(propertyName, isDescending);
                }
            }

            // Pagination
            var totalItems = await query.CountAsync();
            var sales = await query.Skip((queryParameters.Page - 1) * queryParameters.Size)
                                   .Take(queryParameters.Size)
                                   .ToListAsync();

            return sales.Select(s => _mapper.Map<SaleServiceDto>(s)).ToList();
        }

        public async Task<SaleServiceDto> UpdateSale(SaleServiceDto saleDto)
        {
            var sale = await _saleRepository.GetByIdAsync(saleDto.SaleNumber);
            if (sale == null) return null;
           
            await _saleRepository.UpdateAsync(sale);
            return _mapper.Map<SaleServiceDto>(sale);
        }

        public async Task CancelSale(int saleNumber)
        {
            var sale = await _saleRepository.GetByIdAsync(saleNumber);
            if (sale != null)
            {
                sale.IsCancelled = true;
                await _saleRepository.UpdateAsync(sale);
            }
        }
    }
}
