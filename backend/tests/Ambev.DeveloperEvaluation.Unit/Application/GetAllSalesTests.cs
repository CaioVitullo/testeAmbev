using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Builder;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class SaleServiceGetAllTests
    {
        private readonly ISaleService _saleService;
        private readonly ISaleRepository<SaleServiceDto> _saleRepository;
        private readonly IMapper _mapper;

        public SaleServiceGetAllTests()
        {
            _saleRepository = Substitute.For<ISaleRepository<SaleServiceDto>>();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SaleServiceDto, SaleServiceDto>().ReverseMap();
                cfg.CreateMap<SaleItemServiceDto, SaleItemServiceDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();
            _saleService = new SaleService(_saleRepository, _mapper);
        }


        [Fact]
        public async Task GetAllSales_ShouldReturnAllSales_WhenNoFiltersApplied()
        {
            // Arrange
            var sales = new List<SaleServiceDto>
            {
                SaleBuilder.New().WithSaleNumber(1).Build(),
                SaleBuilder.New().WithSaleNumber(2).Build()
            };

            _saleRepository.Query().Returns(sales.AsQueryable());

            var queryParameters = new SaleQueryParametersServiceDto
            {
                Page = 1,
                Size = 10,
                Order = null,
                Customer = null
            };

            // Act
            var result = await _saleService.GetAllSales(queryParameters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllSales_ShouldApplyCustomerFilter_WhenProvided()
        {
            // Arrange
            var sales = new List<SaleServiceDto>
            {
                SaleBuilder.New().WithCustomer("Test Customer").Build(),
                SaleBuilder.New().WithCustomer("Another Customer").Build()
            };

            _saleRepository.Query().Returns(sales.AsQueryable());

            var queryParameters = new SaleQueryParametersServiceDto
            {
                Page = 1,
                Size = 10,
                Customer = "Test Customer"
            };

            // Act
            var result = await _saleService.GetAllSales(queryParameters);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Customer", result.First().Customer);
        }

        [Fact]
        public async Task GetAllSales_ShouldApplyOrdering_WhenProvided()
        {
            // Arrange
            var sales = new List<SaleServiceDto>
            {
                SaleBuilder.New().WithSaleNumber(2).Build(),
                SaleBuilder.New().WithSaleNumber(1).Build()
            };

            _saleRepository.Query().Returns(sales.AsQueryable());

            var queryParameters = new SaleQueryParametersServiceDto
            {
                Page = 1,
                Size = 10,
                Order = "SaleNumber desc"
            };

            // Act
            var result = await _saleService.GetAllSales(queryParameters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.First().SaleNumber); // Check order
        }

        [Fact]
        public async Task GetAllSales_ShouldApplyPagination_WhenProvided()
        {
            // Arrange
            var sales = new List<SaleServiceDto>
            {
                SaleBuilder.New().WithSaleNumber(1).Build(),
                SaleBuilder.New().WithSaleNumber(2).Build(),
                SaleBuilder.New().WithSaleNumber(3).Build()
            };

            _saleRepository.Query().Returns(sales.AsQueryable());

            var queryParameters = new SaleQueryParametersServiceDto
            {
                Page = 1,
                Size = 2 // Requesting only 2 items
            };

            // Act
            var result = await _saleService.GetAllSales(queryParameters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Should return only 2 items
            Assert.Equal(1, result.First().SaleNumber); // Check first item
        }
    }
}




