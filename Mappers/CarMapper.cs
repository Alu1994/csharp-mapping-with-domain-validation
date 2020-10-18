using AutoMapper;
using MappingWithDomainValidation.Domain;
using MappingWithDomainValidation.Requests;

namespace MappingWithDomainValidation.Mappers
{
    public sealed class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<CarRequest, Car>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
        }
    }
}
