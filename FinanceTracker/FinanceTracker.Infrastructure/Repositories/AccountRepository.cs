using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория счетов.
/// </summary>
public class AccountRepository : IAccountRepository
{
  #region Поля и свойства

  /// <summary>
  /// Коллекция счетов.
  /// </summary>
  private readonly List<Account> _accounts;

  #endregion

  #region Методы

  /// <summary>
  /// Добавление нового счета в репозиторий.
  /// </summary>
  /// <param name="account">Счет.</param>
  public void Add(Account account)
  {
    _accounts.Add(account);
  }

  /// <summary>
  /// Получение счета по Id.
  /// </summary>
  /// <param name="accountId">Id счета.</param>
  /// <returns>Найденный счет или null, если счет не существует.</returns>
  public Account? GetById(long accountId)
  {
    return _accounts.FirstOrDefault(x => x.Id == accountId);
  }

  /// <summary>
  /// Получение счета по имени.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит счет.</param>
  /// <param name="name">Имя счета для поиска.</param>
  /// <returns>Найденный счет или null, если счет не существует.</returns>
  public Account? GetByName(long userId, string name)
  {
    return _accounts.FirstOrDefault(x => x.UserId == userId && x.Name == name);
  }

  #endregion

  #region Конструктор

  public AccountRepository()
  {
    _accounts = [];
  }

  #endregion

}