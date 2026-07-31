using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Categories.Validators;

/// <summary>
/// Валидатор архивирования категории.
/// </summary>
public class ArchiveCategoryValidator : IValidator<ArchiveCategoryCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя на архивирование категории.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(ArchiveCategoryCommand command)
  {
    var result = new ValidationResult();

    ValidateCategoryId(command, result);
    
    return result;
  }

  /// <summary>
  /// Валидация Id категории
  /// </summary>
  /// <param name="command">Команда от пользователя на архивирование категории.</param>
  /// <param name="result">Коллекция сообщений об ошибках.</param>
  private void ValidateCategoryId(ArchiveCategoryCommand command, ValidationResult result)
  {
    if (command.CategoryId <= 0) 
      result.AddError(new ValidationError(nameof(command.CategoryId), "Категория не найдена."));
  }
}