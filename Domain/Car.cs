using FluentValidation;
using FluentValidation.Results;

namespace MappingWithDomainValidation.Domain
{
    public sealed class Car : IValidation
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Car()
        {

        }

        private CarValidator _validator;
        public ValidationResult Validate()
        {
            _validator = new CarValidator();
            return _validator.Validate(this);
        }

        public class CarValidator : AbstractValidator<Car>
        {
            public CarValidator()
            {
                RuleFor(product => product.Id).GreaterThan(0);
                RuleFor(product => product.Name).NotEmpty().NotNull();
            }
        }
    }
}
