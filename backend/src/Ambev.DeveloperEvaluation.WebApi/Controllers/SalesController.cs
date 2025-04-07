using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common;
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
            var saleResponses = _mapper.Map<List<SaleResponse>>(sales);
            return Ok(new PaginatedList<SaleResponse>(saleResponses, _totalItens, queryParameters.Page, queryParameters.Size));
           
        }
    }
}
