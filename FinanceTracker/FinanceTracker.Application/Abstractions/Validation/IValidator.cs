using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Abstractions.Validation;

public interface IValidator<T>
{
  ValidationResult Validate(T model);
}