using Xunit;
using NSubstitute;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Unit.Application.Builder;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class SaleServiceTests
    {
        private readonly ISaleService _saleService;
        private readonly ISaleRepository<SaleServiceDto> _saleRepository;
        private readonly IMapper _mapper;

        public SaleServiceTests()
        {
            _saleRepository = Substitute.For<ISaleRepository<SaleServiceDto>>();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SaleServiceDto, SaleServiceDto>().ReverseMap();
                cfg.CreateMap<SaleItemServiceDto, SaleItem>().ReverseMap();
            });
            _mapper = config.CreateMapper();
            _saleService = new SaleService(_saleRepository, _mapper);
        }

        [Fact]
        public async Task CreateSale_ShouldReturnCreatedSale()
        {
            // Arrange
            var saleDto = SaleBuilder.New().WithDefaultValues().Build();
            var saleEntity = _mapper.Map<SaleServiceDto>(saleDto);
            _saleRepository.AddAsync(Arg.Any<SaleServiceDto>()).Returns(Task.CompletedTask);
            _saleRepository.GetByIdAsync(Arg.Any<int>()).Returns(saleEntity);

            // Act
            var result = await _saleService.CreateSale(saleDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(saleDto.SaleNumber, result.SaleNumber);
        }

        [Fact]
        public async Task GetSale_ShouldReturnSale_WhenExists()
        {
            // Arrange
            var saleDto = SaleBuilder.New().WithDefaultValues().Build();
            var saleEntity = _mapper.Map<SaleServiceDto>(saleDto);
            _saleRepository.GetByIdAsync(saleEntity.SaleNumber).Returns(saleEntity);

            // Act
            var result = await _saleService.GetSale(saleEntity.SaleNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(saleEntity.SaleNumber, result.SaleNumber);
        }

        [Fact]
        public async Task UpdateSale_ShouldReturnUpdatedSale()
        {
            // Arrange
            var saleDto = SaleBuilder.New().WithDefaultValues().Build();
            var saleEntity = _mapper.Map<SaleServiceDto>(saleDto);
            _saleRepository.GetByIdAsync(saleEntity.SaleNumber).Returns(saleEntity);

            // Act
            saleDto.Customer = "Updated Customer";
            var result = await _saleService.UpdateSale(saleDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Customer", result.Customer);
        }

        [Fact]
        public async Task CancelSale_ShouldMarkSaleAsCancelled()
        {
            // Arrange
            var saleDto = SaleBuilder.New().WithDefaultValues().Build();
            var saleEntity = _mapper.Map<SaleServiceDto>(saleDto);
            _saleRepository.GetByIdAsync(saleEntity.SaleNumber).Returns(saleEntity);

            // Act
            await _saleService.CancelSale(saleEntity.SaleNumber);

            // Assert
            Assert.True(saleEntity.IsCancelled);
            await _saleRepository.Received(1).UpdateAsync(saleEntity);
        }
    }
}
