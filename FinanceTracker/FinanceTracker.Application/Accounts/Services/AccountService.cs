using FinanceTracker.Application.Abstractions.Factories;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Application.Factories;


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
  private readonly IValidator<CreateAccountCommand> _validator;
  
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
    var result = _validator.Validate(command);

    if (result.HasErrors)
    {
      return result;
    }

    if (_accountRepository.ExistsWithName(command.UserId, command.Name))
    {
      var validationError = new ValidationError(nameof(command.Name), "Счет с таким именем уже существует.");
      result.AddError(validationError);
      return result;
    }

    var account = _accountFactory.Create(command);
    
    _accountRepository.Add(account);
    return result;
  }
  
  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="accountRepository">Репозиторий счетов.</param>
  /// <param name="validator">Валидатор создания счета.</param>
  /// <param name="accountFactory">Фабрика для создания счета.</param>
  public AccountService(IAccountRepository accountRepository, IValidator<CreateAccountCommand> validator, IAccountFactory accountFactory)
  {
    _accountRepository = accountRepository;
    _validator = validator;
    _accountFactory = accountFactory;
  }

  #endregion 
}