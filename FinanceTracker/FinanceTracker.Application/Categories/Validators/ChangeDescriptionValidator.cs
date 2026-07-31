using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Categories.Validators;

/// <summary>
/// Валидатор изменения описания категории
/// </summary>
public class ChangeDescriptionValidator : IValidator<ChangeDescriptionCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя на изменение описания категории.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(ChangeDescriptionCommand command)
  {
    var result = new ValidationResult();
    
    ValidateCategoryId(command, result);
    
    return result;
  }
  
  /// <summary>
  /// Валидация положительного значения Id категории для изменения описания.
  /// </summary>
  /// <param name="command">Команда от пользователя на изменение описания категории.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCategoryId(ChangeDescriptionCommand command, ValidationResult result)
  {
    if (command.CategoryId <= 0) 
      result.AddError(new ValidationError(nameof(command.CategoryId), "Некорректный идентификатор категории."));
  }
}