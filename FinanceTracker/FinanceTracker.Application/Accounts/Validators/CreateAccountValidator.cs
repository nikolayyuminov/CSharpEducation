using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Accounts.Validators;

/// <summary>
/// Валидация при создании нового счета. 
/// </summary>
public class CreateAccountValidator : IValidator<CreateAccountCommand>
{
  #region Методы
  
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(CreateAccountCommand command)
  {
    var result = new ValidationResult();
    
    ValidateName(command, result);
    ValidateAccountType(command, result);
    ValidateBalance(command,  result);
    ValidateCreditLimit(command,  result);
    ValidateCurrency(command,  result);

    return result;
  }

  /// <summary>
  /// Валидация имени счета.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateName(CreateAccountCommand command, ValidationResult result)
  {
    if (string.IsNullOrWhiteSpace(command.Name)) 
      result.AddError(new ValidationError(nameof(command.Name), " не может быть пустым"));
  }

  /// <summary>
  /// Валидация типа счета.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAccountType(CreateAccountCommand command, ValidationResult result)
  {
    if (!Enum.IsDefined(command.AccountType)) result.AddError(new ValidationError(
        nameof(command.AccountType), "значение не найдено")); 
  }

  /// <summary>
  /// Валидация баланса.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateBalance(CreateAccountCommand command, ValidationResult result)
  {
    switch (command.AccountType)
    {
      case AccountType.Debit:
      case AccountType.Deposit:
      {
        if (command.InitialBalance < 0)
          result.AddError(new ValidationError(nameof(command.InitialBalance), " не может быть отрицательным"));
        break;
      }
      case AccountType.Credit:
      {
        if (command.InitialBalance < -command.CreditLimit)
          result.AddError(new ValidationError(nameof(command.InitialBalance),
            " не может быть меньше отрицательного кредитного лимита"));
        break;
      }
    }
  }

  /// <summary>
  /// Валидация кредитного лимита.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCreditLimit(CreateAccountCommand command, ValidationResult result)
  {
    if (command.AccountType is AccountType.Credit)
    {
      switch (command.CreditLimit)
      {
        case null:
          result.AddError(new ValidationError(nameof(command.CreditLimit), " не может быть пустым"));
          break;
        case < 0:
          result.AddError(new ValidationError(nameof(command.CreditLimit), " не может быть отрицательным"));
          break;
      }
    }
    else if (command.CreditLimit != null)
      result.AddError(new ValidationError(
        nameof(command.CreditLimit), " не должно быть для дебетового или депозитного счета"));
  }

  /// <summary>
  /// Валидация валюты.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания нового счета.</param>
  /// <param name="result">Коллекция ошибок.</param>
  /// <returns>Сообщение об ошибке.</returns>
  private void ValidateCurrency(CreateAccountCommand command, ValidationResult result)
  {
    if (!Enum.IsDefined(command.Currency)) 
      result.AddError(new ValidationError(nameof(command.Currency), "значение не найдено"));
  }

  #endregion

}