using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Accounts.Services;

/// <summary>
/// Сервис для работы с переводами.
/// </summary>
public class TransferService : ITransferService
{
  #region Поля и свойства

  /// <summary>
  /// Валидатор перевода средств.
  /// </summary>
  private readonly IValidator<TransferMoneyCommand> _transferMoneyValidator;
  
  /// <summary>
  /// Репозиторий счетов.
  /// </summary>
  private  readonly IAccountRepository _accountRepository;

  #endregion

  #region Методы

  /// <summary>
  /// Перевод средств между счетами.
  /// </summary>
  /// <param name="command">Команда для перевода средств между счетами.</param>
  /// <returns>Ошибки при переводе. Если ошибок нет, перевод средств осуществился успешно.</returns>
  public ValidationResult Transfer(TransferMoneyCommand command)
  {
    var result = _transferMoneyValidator.Validate(command);

    if (result.HasErrors)
    {
      return result;
    }
    
    var senderAccount = _accountRepository.GetById(command.FromAccountId);
    if (senderAccount == null)
    {
      result.AddError(new ValidationError(nameof(command.FromAccountId), "Счет отправителя не найден."));

      return result;
    }

    var receiverAccount = _accountRepository.GetById(command.ToAccountId);
    if (receiverAccount == null)
    {
      result.AddError(new ValidationError(nameof(command.ToAccountId), "Счет получателя не найден."));

      return result;
    }

    senderAccount.Withdraw(command.Amount);
    receiverAccount.Deposit(command.Amount);

    
    return result;
  }

  #endregion

  #region Конструкторы

  public TransferService(IAccountRepository accountRepository, IValidator<TransferMoneyCommand> transferMoneyValidator)
  {
    _accountRepository = accountRepository;
    _transferMoneyValidator = transferMoneyValidator;
  }
  #endregion
}