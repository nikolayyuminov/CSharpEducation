using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.Application.Accounts.Commands;

namespace FinanceTracker.API.Mappers;

/// <summary>
/// Маппер для преобразования моделей счетов между API и слоем Application.
/// </summary>
public static class AccountMapper
{
  #region Методы

  /// <summary>
  /// Преобразовать HTTP-запрос на создание счета
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда создания счета.</returns>
  public static CreateAccountCommand ToCreateAccountCommand(CreateAccountRequest request)
  {
    return new CreateAccountCommand
    {
      UserId = request.UserId,
      Name = request.Name,
      AccountType = request.AccountType,
      InitialBalance = request.InitialBalance,
      Currency = request.Currency,
      CreditLimit = request.CreditLimit
    };
  }

  /// <summary>
  /// Преобразовать HTTP-запрос на переименование счета
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда переименования счета.</returns>
  public static RenameAccountCommand ToRenameAccountCommand(RenameAccountRequest request)
  {
    return new RenameAccountCommand
    {
      UserId = request.UserId,
      NewName = request.NewName,
      AccountId = request.AccountId
    };
  }
  
  /// <summary>
  /// Преобразовать HTTP-запрос на закрытие счета
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда закрытия счета.</returns>
  public static CloseAccountCommand ToCloseAccountCommand(CloseAccountRequest request)
  {
    return new CloseAccountCommand
    {
      AccountId = request.AccountId
    };
  }
  
  /// <summary>
  /// Преобразовать HTTP-запрос на изменение кредитного лимита
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда для изменения кредитного лимита.</returns>
  public static ChangeCreditLimitCommand ToChangeCreditLimitCommand(ChangeCreditLimitRequest request)
  {
    return new ChangeCreditLimitCommand
    {
      AccountId = request.AccountId,
      NewCreditLimit = request.NewCreditLimit
    };
  }

  #endregion
}