using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Кредитный счет.
/// </summary>
public class CreditAccount : Account
{
  #region Поля и свойства

  /// <summary>
  /// Кредитный лимит, если счет кредитный.
  /// </summary>
  public decimal? CreditLimit { get; private set; }

  #endregion
  
  #region Методы

  /// <summary>
  /// Вычесть сумму со счета.
  /// </summary>
  /// <param name="amount">Значение суммы.</param>
  /// <exception cref="InvalidOperationException">Сумма превышает доступный кредитный лимит.</exception>
  public override void Withdraw(decimal amount)
  {
    EnsureAccountIsOpen();
    EnsurePositiveAmount(amount);
    if (Balance - amount < -CreditLimit) throw new InvalidOperationException("Сумма превышает доступный кредитный лимит.");
    Balance -= amount;
  }
  
  /// <summary>
  /// Изменить кредитный лимит.
  /// </summary>
  /// <param name="newLimit">Значение нового лимита.</param>
  /// <exception cref="InvalidOperationException">Кредитный лимит не может быть пустым для кредитного счета.</exception>
  /// <exception cref="InvalidOperationException">Кредитный лимит не может быть отрицательным.</exception>
  public void ChangeCreditLimit(decimal? newLimit)
  {
    EnsureAccountIsOpen();
    if (newLimit == null) throw new InvalidOperationException("Кредитный лимит не может быть пустым для кредитного счета.");
    if (newLimit < 0) throw new InvalidOperationException("Кредитный лимит не может быть отрицательным.");
    CreditLimit = newLimit;
  }

  #endregion
  
  #region Конструкторы

  /// <summary>
  /// Конструктор кредитного счета.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="name">Имя счета.</param>
  /// <param name="currency">Валюта счета.</param>
  /// <param name="creditLimit">Кредитный лимит, если счет кредитный, для остальных по умолчанию null.</param>
  /// <param name="balance">Текущий баланс счета, по умолчанию '0'.</param>
  public CreditAccount(long userId,
                      string name,
                      Currency currency,
                      decimal? creditLimit,
                      decimal balance = 0) :
    base(userId, name, AccountType.Credit, currency)
  {
    ChangeCreditLimit(creditLimit);
    if (balance < -CreditLimit)
      throw new InvalidOperationException("Баланс кредитного счета не может быть ниже установленного лимита.");
    Balance = balance;
  }

  #endregion

}