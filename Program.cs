using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace MappingWithDomainValidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Start: This is for Serilog
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");
            // End

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((configure, services) =>
                {
                    services.AddMapperProfiles();
                    services.AddTransient<IStart, Start>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<Start>(host.Services);

            ServiceLocator.ServiceProvider = host.Services;

            svc.Run();
        }

        // Start: This is for Serilog
        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
        // End
    }
}
