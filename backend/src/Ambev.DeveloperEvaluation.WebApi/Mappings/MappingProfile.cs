using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaleDto, SaleServiceDto>().ReverseMap();
            CreateMap<SaleItemDto, SaleItemServiceDto>().ReverseMap();
            CreateMap<SaleQueryParameters, SaleQueryParametersServiceDto>();
        }
    }
}
