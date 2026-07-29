using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Дебетовый счет.
/// </summary>
public class DebitAccount : Account
{
  #region Методы

  /// <summary>
  /// Вычесть сумму со счета.
  /// </summary>
  /// <param name="amount">Значение суммы.</param>
  /// <exception cref="InvalidOperationException">Недостаточно средств.</exception>
  public override void Withdraw(decimal amount)
  {
    EnsureAccountIsOpen();
    EnsurePositiveAmount(amount);
    if (Balance - amount < 0) throw new InvalidOperationException("Недостаточно средств.");
    Balance -= amount;
  }
  
  /// <summary>
  /// Установить баланс.
  /// </summary>
  /// <param name="balance">Значение баланса.</param>
  /// <exception cref="Range"></exception>
  protected void SetInitialBalance(decimal balance)
  {
    Balance = balance >=0 ? balance : throw new InvalidOperationException("Баланс не может быть отрицательным, если счет не кредитный.");
  }

  #endregion
  
  #region Конструкторы

  /// <summary>
  /// Конструктор дебетового счета.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="name">Имя счета.</param>
  /// <param name="currency">Валюта счета.</param>
  /// <param name="balance">Текущий баланс счета, по умолчанию '0'.</param>
  public DebitAccount(long userId, 
                      string name,  
                      AccountType accountType,
                      Currency currency, 
                      decimal balance = 0) : 
    base(userId, name, accountType, currency)
  {
    accountType = AccountType.Debit;
    SetInitialBalance(balance);
  }

  #endregion
}