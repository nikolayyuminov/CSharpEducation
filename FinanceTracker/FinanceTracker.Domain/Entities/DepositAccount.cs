using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Депозитный счет.
/// </summary>
public class DepositAccount : DebitAccount
{
  #region Конструкторы

  /// <summary>
  /// Конструктор депозитного счета.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="name">Имя счета.</param>
  /// <param name="currency">Валюта счета.</param>
  /// <param name="balance">Текущий баланс счета, по умолчанию '0'.</param>
  public DepositAccount(long userId, 
                        string name, 
                        Currency currency, 
                        decimal balance = 0) : 
    base(userId, name, AccountType.Deposit, currency)
  {
    SetInitialBalance(balance);
  }

  #endregion

}