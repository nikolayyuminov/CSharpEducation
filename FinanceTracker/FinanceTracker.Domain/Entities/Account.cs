using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Счет.
/// </summary>
public class Account
{
  #region Поля и свойства
    /// <summary>
    /// Уникальный идентификатор счета.
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Id пользователя, которому принадлежит счет.
    /// </summary>
    public long UserId { get; init; }
    
    /// <summary>
    /// Имя счета.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Тип счета.
    /// </summary>
    public AccountType AccountType { get; init; }
    
    /// <summary>
    /// Значение баланса.
    /// </summary>
    public decimal Balance { get; private set; }
    
    /// <summary>
    /// Валюта, в которой измеряется баланс счета.
    /// </summary>
    public Currency Currency { get; private set; }
    
    /// <summary>
    /// Кредитный лимит, если счет кредитный.
    /// </summary>
    public decimal? CreditLimit { get; private set; }
    
    /// <summary>
    /// Состояние счета (Открытый/Закрытый).
    /// </summary>
    public bool IsClosed { get; private set; }

  #endregion

  #region Методы
  
  /// <summary>
  /// Изменить имя счета.
  /// </summary>
  /// <param name="newName">Новое имя счета.</param>
  /// <exception cref="NullReferenceException">Ошибка если имя не указано.</exception>
  public void Rename(string newName)
  {
    if (string.IsNullOrWhiteSpace(newName)) throw new InvalidOperationException("Имя не может быть пустым.");
    if (newName.Equals(Name)) return;
    Name = newName;
  }
  
  /// <summary>
  /// Изменить статус счета.
  /// </summary>
  public void Close()
  {
    if (IsClosed == true) throw new InvalidOperationException("Счет уже закрыт.");
    IsClosed = true;
  }
  
  /// <summary>
  /// Изменить кредитный лимит.
  /// </summary>
  /// <param name="newLimit">Значение нового лимита.</param>
  public void ChangeCreditLimit(decimal? newLimit)
  {
    if (this.AccountType != AccountType.Credit) throw new InvalidOperationException("Для установки кредитного лимита, счет должен быть кредитным.");
    if (newLimit == null) throw new InvalidOperationException("Кредитный лимит не может быть пустым для кредитного счета.");
    if (newLimit < 0) throw new InvalidOperationException("Кредитный лимит не может быть меньше '0'.");
    CreditLimit = newLimit;
  }

  #endregion

  #region Конструкторы
  /// <summary>
  /// Конструктор счета.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="name">Имя счета.</param>
  /// <param name="accountType">Тип счета.</param>
  /// <param name="currency">Валюта счета.</param>
  /// <param name="creditLimit">Кредитный лимит, если счет кредитный, для остальных по умолчанию null.</param>
  /// <param name="balance">Текущий баланс счета, по умолчанию '0'.</param>
  public Account(long userId, string name, AccountType accountType, Currency currency,
    decimal? creditLimit = null, decimal balance = 0)
  {
    UserId = userId;
    AccountType = accountType;
    Currency = currency;
    Rename(name);
    if (accountType == AccountType.Credit) ChangeCreditLimit(creditLimit);
    if (accountType != AccountType.Credit) Balance = balance >=0 ? balance : throw new InvalidOperationException("Баланс не может быть отрицательным, если счет не кредитный.");
    else if (Math.Abs(balance) > CreditLimit)
      throw new InvalidOperationException("Баланс кредитного счета не может быть ниже установленного лимита.");
    else Balance = balance;
    IsClosed = false;
  }

  #endregion
}