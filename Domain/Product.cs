using FluentValidation;
using FluentValidation.Results;

namespace MappingWithDomainValidation.Domain
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }

        public Product()
        {

        }

        private ProductValidator _validator;
        public override ValidationResult Validate()
        {
            _validator = new ProductValidator();
            return _validator.Validate(this);
        }

        public class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                RuleFor(product => product.Id).GreaterThan(0);
                RuleFor(product => product.Name).NotEmpty().NotNull();
            }
        }
    }
}
