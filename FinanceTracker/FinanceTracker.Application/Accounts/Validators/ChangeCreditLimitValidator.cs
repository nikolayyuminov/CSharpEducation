using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Accounts.Validators;

/// <summary>
/// Валидатор изменения кредитного лимита.
/// </summary>
public class ChangeCreditLimitValidator : IValidator<ChangeCreditLimitCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя для изменения кредитного лимита счета.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(ChangeCreditLimitCommand command)
  {
    var result = new ValidationResult();
    
    ValidateAccountId(command, result);
    ValidateCreditLimit(command, result);

    return result;
  }

  /// <summary>
  /// Валидация Id счета.
  /// </summary>
  /// <param name="command">Команда от пользователя для изменения кредитного лимита счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAccountId(ChangeCreditLimitCommand command, ValidationResult result)
  {
    if (command.AccountId <= 0) 
      result.AddError(new ValidationError(nameof(command.AccountId), "Счет не найден."));
  }

  /// <summary>
  /// Валидация нового значения кредитного лимита.
  /// </summary>
  /// <param name="command">Команда от пользователя для изменения кредитного лимита счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCreditLimit(ChangeCreditLimitCommand command, ValidationResult result)
  {
    if (command.NewCreditLimit < 0)
      result.AddError(new ValidationError(nameof(command.NewCreditLimit), "Кредитный лимит не может быть отрицательным."));
  }
}