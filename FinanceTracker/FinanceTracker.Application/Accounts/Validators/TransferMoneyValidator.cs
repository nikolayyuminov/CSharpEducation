using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Accounts.Validators;

/// <summary>
/// Валидция перевода средств между счетами. 
/// </summary>
public class TransferMoneyValidator : IValidator<TransferMoneyCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова конкретной валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя для перевода средств между счетами.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(TransferMoneyCommand command)
  {
    var result = new ValidationResult();
    
    ValidateFromAccountId(command, result);
    ValidateToAccountId(command, result);
    ValidateAccountsAreDifferent(command,  result);
    ValidateAmount(command,  result);

    return result;
  }

  /// <summary>
  /// Валидация положительного значения Id счета с которого будет выполнен перевод.
  /// </summary>
  /// <param name="command">Команда от пользователя на перевод средств между счетами.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateFromAccountId(TransferMoneyCommand command, ValidationResult result)
  {
    if (command.FromAccountId <= 0) 
      result.AddError(new ValidationError(nameof(command.FromAccountId), "Некорректный идентификатор счета."));
    
  }

  /// <summary>
  /// Валидация положительного значения Id счета на который будет выполнен перевод.
  /// </summary>
  /// <param name="command">Команда от пользователя на перевод средств между счетами.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateToAccountId(TransferMoneyCommand command, ValidationResult result)
  {
    if (command.ToAccountId <= 0) 
      result.AddError(new ValidationError(nameof(command.ToAccountId), "Аккаунт с таким Id не существует."));
  }

  /// <summary>
  /// Валидация положительного значения суммы перевода.
  /// </summary>
  /// <param name="command">Команда от пользователя на перевод средств между счетами.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAmount(TransferMoneyCommand command, ValidationResult result)
  {
    if (command.Amount <= 0)
      result.AddError(new ValidationError(nameof(command.Amount), "Сумма перевода не может быть отрицательной или равна нулю."));
  }

  /// <summary>
  /// Валидация разного значения Id счетов при переводе средств.
  /// </summary>
  /// <param name="command">Команда от пользователя на перевод средств между счетами.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAccountsAreDifferent(TransferMoneyCommand command, ValidationResult result)
  {
    if  (command.FromAccountId ==command.ToAccountId)
      result.AddError(new ValidationError(nameof(command.FromAccountId), "Счета отправителя и получателя должны отличаться."));
  }
}