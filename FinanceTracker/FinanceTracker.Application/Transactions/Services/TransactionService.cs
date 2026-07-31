using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Application.Transactions.Commands;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Transactions.Services;

/// <summary>
/// Сервис для работы с транзакциями.
/// </summary>
public class TransactionService : ITransactionService
{
  #region Поля и свойства

  /// <summary>
  /// Репозиторий транзакций.
  /// </summary>
  private readonly ITransactionRepository _transactionRepository;
  
  /// <summary>
  /// Репозиторий счетов.
  /// </summary>
  private readonly IAccountRepository _accountRepository;
  
  /// <summary>
  /// Репозиторий категорий.
  /// </summary>
  private readonly ICategoryRepository _categoryRepository;
  
  /// <summary>
  /// Валидатор создания транзакции.
  /// </summary>
  private readonly IValidator<CreateTransactionCommand> _createTransactionValidator;
  
  /// <summary>
  /// Валидатор изменения описания транзакции.
  /// </summary>
  private readonly IValidator<ChangeTransactionDescriptionCommand> _changeDescriptionTransactionValidator;

  #endregion

  #region Методы

  /// <summary>
  /// Создание транзакции.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания транзакции.</param>
  /// <returns>Коллекция ошибок.</returns>
  public ValidationResult CreateTransaction(CreateTransactionCommand command)
  {
    var  validationResult = _createTransactionValidator.Validate(command);
    if (validationResult.HasErrors) return validationResult;
    
    var account = _accountRepository.GetById(command.AccountId);
    if (account == null)
    {
      validationResult.AddError(new ValidationError(nameof(command.AccountId), "Счет не найден."));
      return  validationResult;
    }

    var kind = command.Kind;
    
    if (command.CategoryId != null)
    {
      var category = _categoryRepository.GetById(command.CategoryId.Value);
      if (category == null)
      {
        validationResult.AddError(new ValidationError(nameof(command.CategoryId), "Категория не найдена."));
        return  validationResult;
      }

      if (category.IsArchived)
      {
        validationResult.AddError(new ValidationError(nameof(category.IsArchived), "Нельзя работать с архивной категорией."));
        return  validationResult;
      }
      kind = (TransactionKind)category.CategoryKind;
    }

    if (kind == TransactionKind.Expense) account.Withdraw(command.Amount);
    else account.Deposit(command.Amount);
    
    var transaction = new Transaction(command.AccountId, command.CategoryId, command.Amount, command.Description, kind);
    
    _transactionRepository.Add(transaction);

    return validationResult;
  }

  /// <summary>
  /// Изменение описания транзакции.
  /// </summary>
  /// <param name="command">Команда от пользователя на изменение описания транзакции.</param>
  /// <returns>Коллекция ошибок.</returns>
  public ValidationResult ChangeDescription(ChangeTransactionDescriptionCommand command)
  {
    var  validationResult = _changeDescriptionTransactionValidator.Validate(command);
    if (validationResult.HasErrors) return validationResult;
    
    var transaction = _transactionRepository.GetById(command.TransactionId);

    if (transaction == null)
    {
      validationResult.AddError(new ValidationError(nameof(command.TransactionId), "Транзакция не существует."));
      return  validationResult;
    }

    transaction.ChangeDescription(command.NewDescription);

    return validationResult;
  }

  #endregion

  #region Конструкторы

  public TransactionService(ITransactionRepository transactionRepository,
                            IValidator<CreateTransactionCommand> createTransactionValidator,
                            IValidator<ChangeTransactionDescriptionCommand> changeDescriptionTransactionValidator, 
                            IAccountRepository accountRepository, 
                            ICategoryRepository categoryRepository)
  {
    _transactionRepository = transactionRepository;
    _createTransactionValidator = createTransactionValidator;
    _changeDescriptionTransactionValidator = changeDescriptionTransactionValidator;
    _accountRepository = accountRepository;
    _categoryRepository = categoryRepository;
  }

  #endregion
}