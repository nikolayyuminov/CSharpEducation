using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Accounts.Validators;

/// <summary>
/// Валидация переименования счета.
/// </summary>
public class RenameAccountValidator : IValidator<RenameAccountCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование счета.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(RenameAccountCommand command)
  {
    var result = new ValidationResult();
    
    ValidateAccountId(command, result);
    ValidateNewName(command, result);
    
    return result;
  }
  
  /// <summary>
  /// Валидация положительного значения Id счета для переименования.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAccountId(RenameAccountCommand command, ValidationResult result)
  {
    if (command.AccountId <= 0) 
      result.AddError(new ValidationError(nameof(command.AccountId), "Некорректный идентификатор счета."));
  }

  /// <summary>
  /// Валидация значения нового имени.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateNewName(RenameAccountCommand command, ValidationResult result)
  {
    if (string.IsNullOrWhiteSpace(command.NewName)) 
      result.AddError(new ValidationError(nameof(command.NewName), "Новое имя не может быть пустым."));
  }
}