using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Application.Transactions.Commands;

namespace FinanceTracker.Application.Transactions.Validators;

/// <summary>
/// Валидация при изменении описания транзакции.
/// </summary>
public class ChangeTransactionDescriptionValidator : IValidator<ChangeTransactionDescriptionCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова всей валидации.
  /// </summary>
  /// <param name="command">Команда от пользователя для изменения описания транзакции.</param>
  /// <returns>Коллекция сообщений об ошибках</returns>
  public ValidationResult Validate(ChangeTransactionDescriptionCommand command)
  {
    var result = new ValidationResult();
    
    ValidateTransactionId(command, result);
    
    return result;
  }
  
  /// <summary>
  /// Валидация Id транзакции.
  /// </summary>
  /// <param name="command">Команда от пользователя на изменение описания транзакции.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateTransactionId(ChangeTransactionDescriptionCommand command, ValidationResult result)
  {
    if (command.TransactionId <= 0) 
      result.AddError(new ValidationError(nameof(command.TransactionId), "Некорректный идентификатор транзакции."));
  }
}