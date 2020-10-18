using AutoMapper;
using FluentValidation.Results;
using System;
using System.Linq;
using MappingWithDomainValidation.Domain;
using MappingWithDomainValidation.Utils;

namespace MappingWithDomainValidation.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object obj)
        {
            var mapper = ServiceLocator.GetService<IMapper>();
            return mapper.Map<TDestination>(obj);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource obj)
        {
            var mapper = ServiceLocator.GetService<IMapper>();
            return mapper.Map<TSource, TDestination>(obj);
        }

        #region[Mapper for IValidation Interface]

        /// <summary>
        ///     This method maps to the expected destination executing the Destination Fluent Validation.
        /// </summary>
        /// <typeparam name="TDestination">Destination mapping type</typeparam>
        /// <param name="obj">Current object</param>
        /// <returns>Returns mapped object if is valid, otherwise it will throw an exception.</returns>
        public static TDestination ValidationMapTo<TDestination>(this object obj) where TDestination : IValidation
        {
            var destination = obj.MapTo<TDestination>();
            var validation = destination.Validate();

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(error => error.ErrorMessage);
                var concatedErrors = string.Join(Environment.NewLine, errors);
                throw new InvalidOperationException(concatedErrors);
            }

            return destination;
        }

        /// <summary>
        ///     This method maps to the expected destination executing the Destination Fluent Validation.
        /// </summary>
        /// <typeparam name="TDestination">Destination mapping type</typeparam>
        /// <param name="obj">Current object</param>
        /// <returns>Returns mapped object if is valid, otherwise it will return only the Result with the ValidationResult.</returns>
        public static Result<TDestination> ValidationResultMapTo<TDestination>(this object obj) where TDestination : IValidation
        {
            var destination = obj.MapTo<TDestination>();
            var validation = destination.Validate();
            return new Result<TDestination>(destination, validation);
        }

        #endregion

        #region[Mapper for Entity Abstract Class]

        /// <summary>
        ///     This method maps to the expected destination executing the Destination Fluent Validation.
        /// </summary>
        /// <typeparam name="TDestination">Destination mapping type</typeparam>
        /// <param name="obj">Current object</param>
        /// <returns>Returns mapped object if is valid, otherwise it will throw an exception.</returns>
        public static TDestination EntityValidationMapTo<TDestination>(this object obj) where TDestination : Entity
        {
            var destination = obj.MapTo<TDestination>();
            var validation = destination.Validate();

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(error => error.ErrorMessage);
                var concatedErrors = string.Join(Environment.NewLine, errors);
                throw new InvalidOperationException(concatedErrors);
            }

            return destination;
        }

        /// <summary>
        ///     This method maps to the expected destination executing the Destination Fluent Validation.
        /// </summary>
        /// <typeparam name="TDestination">Destination mapping type</typeparam>
        /// <param name="obj">Current object</param>
        /// <returns>Returns mapped object if is valid, otherwise it will return only the Result with the ValidationResult.</returns>
        public static Result<TDestination> EntityValidationResultMapTo<TDestination>(this object obj) where TDestination : Entity
        {
            var destination = obj.MapTo<TDestination>();
            var validation = destination.Validate();
            return new Result<TDestination>(destination, validation);
        }
        
        #endregion


        /// <summary>
        ///     This method maps to the expected destination executing the informed param validation.
        /// </summary>
        /// <typeparam name="TDestination">Destination mapping type</typeparam>
        /// <param name="obj">Current object</param>
        /// <param name="validate">Validation delegate</param>
        /// <returns>Returns mapped object if is valid, otherwise it will return only the Result with the ValidationResult.</returns>
        public static Result<TDestination> ValidationResultMapTo<TDestination>(this object obj, Func<TDestination, ValidationResult> validate)
        {
            var destination = obj.MapTo<TDestination>();
            var validation = validate(destination);
            return new Result<TDestination>(destination, validation);
        }
    }
}
