using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    // src/DeveloperStore.Api/Controllers/SalesController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SaleController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleDto sale)
        {
            var createdSale = await _saleService.CreateSale(sale);
            return CreatedAtAction(nameof(GetSale), new { saleNumber = createdSale.SaleNumber }, createdSale);
        }

        [HttpGet("{saleNumber}")]
        public async Task<IActionResult> GetSale(int saleNumber)
        {
            var sale = await _saleService.GetSale(saleNumber);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        [HttpPut("{saleNumber}")]
        public async Task<IActionResult> UpdateSale(int saleNumber, [FromBody] SaleDto sale)
        {
            if (saleNumber != sale.SaleNumber) return BadRequest();
            await _saleService.UpdateSale(sale);
            return NoContent();
        }

        [HttpDelete("{saleNumber}")]
        public async Task<IActionResult> CancelSale(int saleNumber)
        {
            await _saleService.CancelSale(saleNumber);
            return NoContent();
        }

        
    }
}
