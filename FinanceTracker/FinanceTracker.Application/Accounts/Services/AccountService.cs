using FinanceTracker.Application.Abstractions.Factories;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;


namespace FinanceTracker.Application.Accounts.Services;

/// <summary>
/// Сервис для работы со счетом.
/// </summary>
public class AccountService : IAccountService
{
  #region Поля и свойства

  /// <summary>
  /// Репозиторий счетов.
  /// </summary>
  private  readonly IAccountRepository _accountRepository;

  /// <summary>
  /// Валидатор создания счета.
  /// </summary>
  private readonly IValidator<CreateAccountCommand> _createAccountValidator;
  
  /// <summary>
  /// Валидатор переименования счета.
  /// </summary>
  private readonly IValidator<RenameAccountCommand> _renameAccountValidator;
  
  /// <summary>
  /// Валидатор закрытия счета.
  /// </summary>
  private readonly IValidator<CloseAccountCommand> _closeAccountValidator;
  
  /// <summary>
  /// Фабрика для создания счета.
  /// </summary>
  private readonly IAccountFactory _accountFactory;

  #endregion
  
  #region Методы

  /// <summary>
  /// Создание счета.
  /// </summary>
  /// <param name="command">Команда с данными для создания счета.</param>
  /// <returns>Ошибки при создании. Если ошибок нет, счет создался успешно.</returns>
  public ValidationResult CreateAccount(CreateAccountCommand command)
  {
    var result = _createAccountValidator.Validate(command);

    if (result.HasErrors)
    {
      return result;
    }
    
    var existAccount = _accountRepository.GetByName(command.UserId, command.Name);

    if (existAccount != null)
    {
      var validationError = new ValidationError(nameof(command.Name), "Счет с таким именем уже существует.");
      result.AddError(validationError);
      return result;
    }

    var account = _accountFactory.Create(command);
    
    _accountRepository.Add(account);
    return result;
  }

  /// <summary>
  /// Переименовать счет.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование счета.</param>
  /// <returns>Ошибки при переименовании. Если ошибок нет, счет переименовался успешно.</returns>
  public ValidationResult RenameAccount(RenameAccountCommand command)
  {
    var result = _renameAccountValidator.Validate(command);

    if (result.HasErrors)
    {
      return result;
    }
    
    var account = _accountRepository.GetById(command.AccountId);
    
    if (account == null)
    {
      result.AddError(new ValidationError(nameof(command.AccountId), "Счет не найден."));
      return result;
    }
    
    var existingAccount  = _accountRepository.GetByName(command.UserId, command.NewName!);
    
    if (existingAccount != null && existingAccount.Id != account.Id)
    {
      result.AddError(new ValidationError(nameof(command.NewName), "Счет с таким именем уже существует."));
      return result;
    }
    
    account.Rename(command.NewName!);
    
    return result;
  }

  /// TODO
  /// Перенести проверки возможности закрытия счета в доменную модель, вместе с рефакторингом Exception.
  /// <summary>
  /// Закрыть счет.
  /// </summary>
  /// <param name="command">Команда от пользователя на закрытие счета.</param>
  /// <returns>Ошибки при закрытии. Если ошибок нет, счет закрыт.</returns>
  public ValidationResult CloseAccount(CloseAccountCommand command)
  {
    var result = _closeAccountValidator.Validate(command);

    if (result.HasErrors)
    {
      return result;
    }

    var account = _accountRepository.GetById(command.AccountId);

    if (account == null)
    {
      result.AddError(new ValidationError(nameof(command.AccountId), "Счет не найден."));
      return result;
    }

    if (account.Balance != 0)
    {
      result.AddError(new ValidationError(nameof(account.Balance), "Для закрытия счета его баланс должен быть равен нулю."));
      return result;
    }
  
    account.Close();
    
    return result;
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="accountRepository">Репозиторий счетов.</param>
  /// <param name="createAccountValidator">Валидатор создания счета.</param>
  /// <param name="accountFactory">Фабрика для создания счета.</param>
  /// <param name="renameAccountValidator">Валидатор переименования счета.</param>
  /// <param name="closeAccountValidator">Валидатор закрытия счета.</param>
  public AccountService(IAccountRepository accountRepository, 
                        IValidator<CreateAccountCommand> createAccountValidator, 
                        IAccountFactory accountFactory, 
                        IValidator<RenameAccountCommand> renameAccountValidator, 
                        IValidator<CloseAccountCommand> closeAccountValidator)
  {
    _accountRepository = accountRepository;
    _createAccountValidator = createAccountValidator;
    _accountFactory = accountFactory;
    _renameAccountValidator = renameAccountValidator;
    _closeAccountValidator = closeAccountValidator;
  }

  #endregion 
}