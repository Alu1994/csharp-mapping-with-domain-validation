using System;

namespace MappingWithDomainValidation
{
    public class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static TService GetService<TService>() =>
            (TService)ServiceProvider.GetService(typeof(TService));
    }
}
