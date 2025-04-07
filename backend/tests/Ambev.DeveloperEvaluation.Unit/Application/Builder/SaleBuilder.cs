using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Builder
{
    public class SaleBuilder
    {
        private SaleServiceDto _saleDto;

        private SaleBuilder()
        {
            _saleDto = new SaleServiceDto
            {
                SaleNumber = 1,
                SaleDate = DateTime.Now,
                Customer = "Test Customer",
                TotalSaleAmount = 100.00m,
                Branch = "Test Branch",
                Items = new List<SaleItemServiceDto>
            {
                new SaleItemServiceDto
                {
                    Product = "Test Product",
                    Quantity = 5,
                    UnitPrice = 20.00m,
                    Discount = 0.00m
                }
            },
                IsCancelled = false
            };
        }

        public static SaleBuilder New() => new SaleBuilder();

        public SaleBuilder WithDefaultValues()
        {
            return this;
        }

        public SaleBuilder WithSaleNumber(int saleNumber)
        {
            _saleDto.SaleNumber = saleNumber;
            return this;
        }

        public SaleBuilder WithCustomer(string customer)
        {
            _saleDto.Customer = customer;
            return this;
        }

        public SaleServiceDto Build() => _saleDto;
    }
}
