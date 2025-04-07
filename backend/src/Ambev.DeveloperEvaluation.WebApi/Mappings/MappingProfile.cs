using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaleRequest, SaleServiceDto>().ReverseMap();
            CreateMap<SaleItemRequest, SaleItemServiceDto>().ReverseMap();
            CreateMap<SaleQueryParameters, SaleQueryParametersServiceDto>();
            CreateMap<SaleServiceDto, SaleResponse>();
            CreateMap<SaleItemServiceDto, SaleItemResponse>();
        }
    }
}
