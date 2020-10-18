using AutoMapper;
using MappingWithDomainValidation.Domain;
using MappingWithDomainValidation.Requests;

namespace MappingWithDomainValidation.Mappers
{
    public sealed class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductRequest, Product>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
        }
    }
}
