using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Abstractions.Validation;

/// <summary>
/// Валидация передаваемых параметров. 
/// </summary>
public interface IValidator<T>
{
  /// <summary>
  /// Основной метод валидации для вызова всей валидации.
  /// </summary>
  /// <param name="model">Модель передаваемых параметров.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  ValidationResult Validate(T model);
}