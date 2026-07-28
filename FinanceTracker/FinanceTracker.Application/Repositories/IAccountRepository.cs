using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Repositories;

/// <summary>
/// Репозиторий счетов.
/// </summary>
public interface IAccountRepository
{
  /// <summary>
  /// Проверка существования имени счета в репозитории.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит счет.</param>
  /// <param name="name">Имя счета для проверки уникальности.</param>
  /// <returns>True если имя уникально.</returns>
  public bool ExistsByName(long userId, string name);

  /// <summary>
  /// Добавление нового счета в репозиторий.
  /// </summary>
  /// <param name="account">Счет.</param>
  public void Add(Account account);
}