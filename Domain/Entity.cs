using FluentValidation.Results;

namespace MappingWithDomainValidation.Domain
{
    public abstract class Entity
    {
        public int Id { get; }
        public abstract ValidationResult Validate();
    }
}