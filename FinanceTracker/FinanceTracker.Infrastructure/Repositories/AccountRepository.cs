using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория счетов.
/// </summary>
public class AccountRepository : IAccountRepository
{
  #region Поля и свойства

  /// <summary>
  /// Контекст БД.
  /// </summary>
  private readonly FinanceTrackerDbContext _dbContext;

  #endregion

  #region Методы

  /// <summary>
  /// Добавление нового счета в репозиторий.
  /// </summary>
  /// <param name="account">Счет.</param>
  public void Add(Account account)
  {
    _dbContext.Accounts.Add(account);
  }

  /// <summary>
  /// Получение счета по Id.
  /// </summary>
  /// <param name="accountId">Id счета.</param>
  /// <returns>Найденный счет или null, если счет не существует.</returns>
  public Account? GetById(long accountId)
  {
    return _dbContext.Accounts.Find(accountId);
  }

  /// <summary>
  /// Получение счета по имени.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит счет.</param>
  /// <param name="name">Имя счета для поиска.</param>
  /// <returns>Найденный счет или null, если счет не существует.</returns>
  public Account? GetByName(long userId, string name)
  {
    return _dbContext.Accounts
                     .FirstOrDefault(x => x.UserId == userId && x.Name == name);
  }
  
  public IReadOnlyCollection<Account> GetAll(long userId)
  {
    return _dbContext.Accounts
      .Where(x => x.UserId == userId)
      .OrderBy(x => x.Name)
      .ToList();
  }

  #endregion

  #region Конструктор

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="dbContext">Контекст БД.</param>
  public AccountRepository(FinanceTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  #endregion

}