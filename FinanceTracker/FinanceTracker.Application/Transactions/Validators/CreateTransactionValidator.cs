using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Application.Transactions.Commands;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Transactions.Validators;

/// <summary>
/// Валидатор создания транзакции.
/// </summary>
public class CreateTransactionValidator : IValidator<CreateTransactionCommand>
{
  /// <summary>
  /// Основной метод валидации для вызова всей валидации.
  /// </summary>
  /// <param name="command">Команда пользователя для создания новой транзакции.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult Validate(CreateTransactionCommand command)
  {
    var result = new ValidationResult();
    
    ValidateAccountId(command, result);
    ValidateCategoryId(command, result);
    ValidateAmount(command, result);
    ValidateTransactionKind(command, result);
    ValidateCategoryIdAndKind(command, result);
    
    return result;
  }

  /// <summary>
  /// Валидатор вида транзакции
  /// </summary>
  /// <param name="command">Команда пользователя для создания новой транзакции.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateTransactionKind(CreateTransactionCommand command, ValidationResult result)
  {
    if (command.Kind !=null && !Enum.IsDefined(typeof(TransactionKind), command.Kind)) 
      result.AddError(new ValidationError(nameof(command.Kind), "значение не найдено."));
  }

  /// <summary>
  /// Валидация отсутствия категории и вида транзакции.
  /// </summary>
  /// <param name="command">Команда пользователя для создания новой транзакции.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCategoryIdAndKind(CreateTransactionCommand command, ValidationResult result)
  {
    if (command.CategoryId == null && command.Kind == null)
      result.AddError(new ValidationError(nameof(command.Kind), "Необходимо выбрать категорию или указать вид операции."));
  }

  /// <summary>
  /// Валидация Id счета.
  /// </summary>
  /// <param name="command">Команда пользователя для создания новой транзакции.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAccountId(CreateTransactionCommand command, ValidationResult result)
  {
    if (command.AccountId <= 0) 
      result.AddError(new ValidationError(nameof(command.AccountId), "Некорректный идентификатор счета."));
  }
  
  /// <summary>
  /// Валидация Id категории.
  /// </summary>
  /// <param name="command">Команда пользователя для создания новой транзакции.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateCategoryId(CreateTransactionCommand command, ValidationResult result)
  {
    if (command.CategoryId is <= 0) 
      result.AddError(new ValidationError(nameof(command.CategoryId), "Некорректный идентификатор категории."));
  }
  
  /// <summary>
  /// Валидация суммы транзакции.
  /// </summary>
  /// <param name="command">Команда пользователя для создания новой транзакции.</param>
  /// <param name="result">Коллекция ошибок.</param>
  private void ValidateAmount(CreateTransactionCommand command, ValidationResult result)
  {
    if (command.Amount is <= 0) 
      result.AddError(new ValidationError(nameof(command.Amount), "Сумма должна быть положительной."));
  }
}