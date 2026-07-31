using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Categories.Validators;

/// <summary>
/// Валидатор создания категории.
/// </summary>
public class CreateCategoryValidator : IValidator<CreateCategoryCommand>
{
  /// <summary>
  /// Основной метод валидации, который вызывает все остальные методы.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания категории.</param>
  /// <returns>Список сообщений об ошибках.</returns>
  public ValidationResult Validate(CreateCategoryCommand command)
  {
    var result = new ValidationResult();
    
    ValidateName(command, result);
    ValidateCategoryKind(command, result);
    
    return result;
  }
  
  /// <summary>
  /// Валидация имени категории.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания категории.</param>
  /// <param name="result">Список сообщений об ошибках.</param>
  private void ValidateName(CreateCategoryCommand command, ValidationResult result)
  {
    if (string.IsNullOrWhiteSpace(command.Name)) 
      result.AddError(new ValidationError(nameof(command.Name), "Имя категории не может быть пустым."));
  }
  
  /// <summary>
  /// Валидация вида категории.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания новой категории.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCategoryKind(CreateCategoryCommand command, ValidationResult result)
  {
    if (!Enum.IsDefined(command.CategoryKind)) result.AddError(new ValidationError(
      nameof(command.CategoryKind), "Неизвестный вид категории."));
  }
}