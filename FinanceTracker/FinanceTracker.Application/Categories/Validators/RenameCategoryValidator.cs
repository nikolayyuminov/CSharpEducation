using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Categories.Validators;

/// <summary>
/// Валидация переименования категории. 
/// </summary>
public class RenameCategoryValidator : IValidator<RenameCategoryCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование категории.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(RenameCategoryCommand command)
  {
    var result = new ValidationResult();
    
    ValidateCategoryId(command, result);
    ValidateNewName(command, result);
    
    return result;
  }
  
  /// <summary>
  /// Валидация положительного значения Id категории для переименования.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование категории.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCategoryId(RenameCategoryCommand command, ValidationResult result)
  {
    if (command.CategoryId <= 0) 
      result.AddError(new ValidationError(nameof(command.CategoryId), "Некорректный идентификатор категории."));
  }

  /// <summary>
  /// Валидация значения нового имени.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование категории.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateNewName(RenameCategoryCommand command, ValidationResult result)
  {
    if (string.IsNullOrWhiteSpace(command.NewName)) 
      result.AddError(new ValidationError(nameof(command.NewName), "Новое имя не может быть пустым."));
  }
}