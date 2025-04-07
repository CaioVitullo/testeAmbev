using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    public class SalesController : Controller
    {

        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSales([FromQuery] SaleQueryParameters queryParametersDto)
        {
            var queryParameters = _mapper.Map<SaleQueryParametersServiceDto>(queryParametersDto);
            var sales = await _saleService.GetAllSales(queryParameters);
            int _totalItens = sales.Count();
            return Ok(new
            {
                Data = sales,
                TotalItems = _totalItens, // Adjust if needed
                CurrentPage = queryParameters.Page,
                TotalPages = (int)Math.Ceiling((double)_totalItens / queryParameters.Size) // Assuming totalItems is available
            });
        }
    }
}
