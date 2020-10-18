using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MappingWithDomainValidation.Mappers;

namespace MappingWithDomainValidation
{
    public static class MapperProfile
    {
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            var mapper = new Mapper(
                new MapperConfiguration(cfg => {
                    cfg.AddProfile<ProductMapper>();
                    cfg.AddProfile<CarMapper>();
                }));

            services.AddSingleton(typeof(IMapper), mapper);

            return services;
        }
    }
}
