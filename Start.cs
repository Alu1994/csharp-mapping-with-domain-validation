using FluentValidation.Results;
using System;
using System.Linq;
using MappingWithDomainValidation.Domain;
using MappingWithDomainValidation.Extensions;
using MappingWithDomainValidation.Requests;
using static System.Console;

namespace MappingWithDomainValidation
{
    public interface IStart
    {
        void Run();
    }

    public class Start : IStart
    {
        private static readonly string TAB_BREAK_LINE = $"{Environment.NewLine}\t";

        public void Run()
        {
            WriteLine("Hello!");

            ValidateCar();

            ValidateProduct();

            ReadLine();
        }

        private static void ValidateCar()
        {
            WriteLine(Environment.NewLine);
            WriteLine("Start - Car With 'IValidation' Interface");
            WriteLine(Environment.NewLine);

            var carRequest = new CarRequest
            {
                Id = -1,
                Name = ""
            };

            WriteTabLine($"Requests.CarRequest: {carRequest.Id}");

            WriteLine(Environment.NewLine);
            CarMapDoByYourselfValidation(carRequest);

            WriteLine(Environment.NewLine);
            CarMapExceptionResult(carRequest);

            WriteLine(Environment.NewLine);
            CarMapValidationResult(carRequest);

            WriteLine(Environment.NewLine);
            CarMapDelegateValidationResult(carRequest);

            WriteLine(Environment.NewLine);
            WriteLine("End - Car With 'IValidation' Interface");
            WriteLine(Environment.NewLine);

            static void CarMapDoByYourselfValidation(CarRequest carRequest)
            {
                WriteTabLineColor(nameof(CarMapDoByYourselfValidation));

                var car = carRequest.MapTo<Car>();
                var result = car.Validate();

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(error => error.ErrorMessage);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
                else
                    WriteTabLine($"1 - Domain.Car: {car.Id}");
            }

            static void CarMapExceptionResult(CarRequest carRequest)
            {
                WriteTabLineColor(nameof(CarMapExceptionResult));

                try
                {
                    var car2 = carRequest.ValidationMapTo<Car>();
                    WriteTabLine($"2 - Domain.Car: {car2.Id}");
                }
                catch (Exception ex)
                {
                    var array = ex.Message.Split(Environment.NewLine);
                    var errors = array.Select(error => error);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
            }

            static void CarMapValidationResult(CarRequest carRequest)
            {
                WriteTabLineColor(nameof(CarMapValidationResult));

                var car3 = carRequest.ValidationResultMapTo<Car>();
                if (car3.IsFailure)
                {
                    var errors = car3.ValidationResult.Errors.Select(error => error.ErrorMessage);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
                else
                    WriteTabLine($"3 - Domain.Car: {car3.Value.Id}");
            }

            static void CarMapDelegateValidationResult(CarRequest carRequest)
            {
                WriteTabLineColor(nameof(CarMapDelegateValidationResult));

                var validationFailures = new ValidationFailure[2];
                validationFailures[0] = new ValidationFailure("Id", "'Id' deve ser superior a '0'.");
                validationFailures[1] = new ValidationFailure("Name", "'Name' deve ser informado.");

                var car4 = carRequest.ValidationResultMapTo<Car>((destination) => { return new ValidationResult(validationFailures); });
                if (car4.IsFailure)
                {
                    var errors = car4.ValidationResult.Errors.Select(error => error.ErrorMessage);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
                else
                    WriteTabLine($"4 - Domain.Car: {car4.Value.Id}");
            }
        }

        private static void ValidateProduct()
        {
            WriteLine(Environment.NewLine);
            WriteLine("Start - Product With 'Entity' Abstract Class");
            WriteLine(Environment.NewLine);

            var productRequest = new ProductRequest
            {
                Id = -1,
                Name = ""
            };

            WriteTabLine($"Requests.ProductRequest: {productRequest.Id}");

            WriteLine(Environment.NewLine);
            ProductMapDoByYourselfValidation(productRequest);

            WriteLine(Environment.NewLine);
            ProductMapExceptionResult(productRequest);

            WriteLine(Environment.NewLine);
            ProductMapValidationResult(productRequest);

            WriteLine(Environment.NewLine);
            ProductMapDelegateValidationResult(productRequest);

            WriteLine(Environment.NewLine);
            WriteLine("End - Product With 'Entity' Abstract Class");
            WriteLine(Environment.NewLine);

            static void ProductMapDoByYourselfValidation(ProductRequest request)
            {
                WriteTabLineColor(nameof(ProductMapDoByYourselfValidation));

                var product = request.MapTo<Product>();
                var result = product.Validate();

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(error => error.ErrorMessage);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
                else
                    WriteTabLine($"1 - Domain.Product: {product.Id}");
            }

            static void ProductMapExceptionResult(ProductRequest request)
            {
                WriteTabLineColor(nameof(ProductMapExceptionResult));

                try
                {
                    var product2 = request.EntityValidationMapTo<Product>();
                    WriteTabLine($"2 - Domain.Product: {product2.Id}");
                }
                catch (Exception ex)
                {
                    var array = ex.Message.Split(Environment.NewLine);
                    var errors = array.Select(error => error);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
            }

            static void ProductMapValidationResult(ProductRequest request)
            {
                WriteTabLineColor(nameof(ProductMapValidationResult));

                var product3 = request.EntityValidationResultMapTo<Product>();
                if (product3.IsFailure)
                {
                    var errors = product3.ValidationResult.Errors.Select(error => error.ErrorMessage);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
                else
                    WriteTabLine($"3 - Domain.Product: {product3.Value.Id}");
            }

            static void ProductMapDelegateValidationResult(ProductRequest request)
            {
                WriteTabLineColor(nameof(ProductMapDelegateValidationResult));

                var validationFailures = new ValidationFailure[2];
                validationFailures[0] = new ValidationFailure("Id", "'Id' deve ser superior a '0'.");
                validationFailures[1] = new ValidationFailure("Name", "'Name' deve ser informado.");

                var product4 = request.ValidationResultMapTo<Product>((destination) => { return new ValidationResult(validationFailures); });
                if (product4.IsFailure)
                {
                    var errors = product4.ValidationResult.Errors.Select(error => error.ErrorMessage);
                    WriteTabLine(string.Join(TAB_BREAK_LINE, errors));
                }
                else
                    WriteTabLine($"4 - Domain.Product: {product4.Value.Id}");
            }
        }

        private static void WriteTabLine(string message)
        {
            WriteLine($"\t{message}");
        }

        private static void WriteTabLineColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine($"\t{message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}