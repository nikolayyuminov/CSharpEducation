using FinanceTracker.Application.Abstractions.Repositories;

namespace FinanceTracker.Application.Accounts.Queries.GetAccounts;

/// <summary>
/// Запросы для чтения счетов.
/// </summary>
public class AccountQueries : IAccountQueries
{
  #region Поля

  /// <summary>
  /// Репозиторий счетов.
  /// </summary>
  private readonly IAccountRepository _accountRepository;

  #endregion

  #region Методы

  /// <inheritdoc />
  public IReadOnlyCollection<AccountListItemDto> GetAll(long userId)
  {
    return _accountRepository
      .GetAll(userId)
      .Select(x => new AccountListItemDto
      {
        Id = x.Id,
        Name = x.Name,
        Balance = x.Balance,
        AccountType = x.AccountType,
        IsClosed = x.IsClosed
      })
      .ToList();
  }

  #endregion

  #region Конструктор

  /// <summary>
  /// Конструктор.
  /// </summary>
  public AccountQueries(IAccountRepository accountRepository)
  {
    _accountRepository = accountRepository;
  }

  #endregion
}