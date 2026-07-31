using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Accounts.Validators;

/// <summary>
/// Валидатор закрытия счета.
/// </summary>
public class CloseAccountValidator : IValidator<CloseAccountCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(CloseAccountCommand command)
  {
    var result = new ValidationResult();
    
    if (command.AccountId <= 0) 
      result.AddError(new ValidationError(nameof(command.AccountId), "Счет не найден."));
    
    return result;
  }
}