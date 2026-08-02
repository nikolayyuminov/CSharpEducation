using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

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
  
  /// <summary>
  /// Репозиторий транзакций.
  /// </summary>
  private readonly ITransactionRepository _transactionRepository;

  #endregion

  #region Методы

  /// <summary>
  /// Перевод средств между счетами.
  /// </summary>
  /// <param name="command">Команда для перевода средств между счетами.</param>
  /// <returns>Ошибки при переводе. Если ошибок нет, перевод средств осуществился успешно.</returns>
  public ValidationResult Transfer(TransferMoneyCommand command)
  {
    var transferId = Guid.NewGuid();
    
    var result = _transferMoneyValidator.Validate(command);

    if (result.HasErrors) return result;
    
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
    var expenseTransaction = new Transaction(command.FromAccountId, null, command.Amount, command.Description, TransactionKind.Expense, transferId);
    
    receiverAccount.Deposit(command.Amount);
    var incomeTransaction = new Transaction(command.ToAccountId, null, command.Amount, command.Description, TransactionKind.Income, transferId);
    
    _transactionRepository.Add(expenseTransaction);
    _transactionRepository.Add(incomeTransaction);
    
    return result;
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="accountRepository">Репозиторий счетов.</param>
  /// <param name="transferMoneyValidator">Валидатор перевода средств между счетмаи.</param>
  /// <param name="transactionRepository">Репозиторий транзакций.</param>
  public TransferService(IAccountRepository accountRepository, 
                        IValidator<TransferMoneyCommand> transferMoneyValidator, 
                        ITransactionRepository transactionRepository)
  {
    _accountRepository = accountRepository;
    _transferMoneyValidator = transferMoneyValidator;
    _transactionRepository = transactionRepository;
  }
  #endregion
}