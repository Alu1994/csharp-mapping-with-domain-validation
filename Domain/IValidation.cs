using FluentValidation.Results;

namespace MappingWithDomainValidation.Domain
{
    public interface IValidation
    {
        ValidationResult Validate();
    }
}