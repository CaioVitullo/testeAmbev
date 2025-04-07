using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;


namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing sale operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of SalesController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public SaleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale
        /// </summary>
        /// <param name="request">The sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new SaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<SaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<SaleResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves a sale by its ID
        /// </summary>
        /// <param name="saleNumber">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        [HttpGet("{saleNumber}")]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] int saleNumber, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { SaleNumber = saleNumber };
            var validator = new GetSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            if (response == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Sale not found"
                });
            }

            return Ok(new ApiResponseWithData<SaleResponse>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = _mapper.Map<SaleResponse>(response)
            });
        }

        /// <summary>
        /// Updates an existing sale
        /// </summary>
        /// <param name="saleNumber">The unique identifier of the sale</param>
        /// <param name="request">The sale update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the sale was updated</returns>
        [HttpPut("{saleNumber}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale([FromRoute] int saleNumber, [FromBody] SaleRequest request, CancellationToken cancellationToken)
        {
            if (saleNumber != request.SaleNumber) return BadRequest();

            var validator = new SaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Cancels a sale by its ID
        /// </summary>
        /// <param name="saleNumber">The unique identifier of the sale to cancel</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the sale was canceled</returns>
        [HttpDelete("{saleNumber}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSale([FromRoute] int saleNumber, CancellationToken cancellationToken)
        {
            var command = new CancelSaleCommand { SaleNumber = saleNumber };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}