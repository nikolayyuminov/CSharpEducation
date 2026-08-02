using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Абстрактный счет.
/// </summary>
public abstract class Account
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
    public decimal Balance { get; protected set; }
    
    /// <summary>
    /// Валюта, в которой измеряется баланс счета.
    /// </summary>
    public Currency Currency { get; private set; }
    
    /// <summary>
    /// Состояние счета (Открытый/Закрытый).
    /// </summary>
    public bool IsClosed { get; private set; }
    
    /// <summary>
    /// Навигационное свойство список транзакций.
    /// </summary>
    public IReadOnlyCollection<Transaction> Transactions => [];

  #endregion

  #region Методы
  
  /// <summary>
  /// Изменить имя счета.
  /// </summary>
  /// <param name="newName">Новое имя счета.</param>
  /// <exception cref="InvalidOperationException">Имя счета не может быть пустым.</exception>
  public void Rename(string newName)
  {
    EnsureAccountIsOpen();
    if (string.IsNullOrWhiteSpace(newName)) throw new InvalidOperationException("Имя счета не может быть пустым.");
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
  /// Проверка закрытости счета.
  /// </summary>
  /// <exception cref="InvalidOperationException">Нельзя выполнять операции с закрытым счетом.</exception>
  protected void EnsureAccountIsOpen()
  {
    if (IsClosed) throw new InvalidOperationException("Нельзя выполнять операции с закрытым счетом.");
  }
  
  /// <summary>
  /// Проверка положительного значения суммы.
  /// </summary>
  /// <param name="amount">Значение суммы.</param>
  /// <exception cref="InvalidOperationException">Сумма не может быть отрицательной.</exception>
  protected void EnsurePositiveAmount(decimal amount)
  {
    if (amount <= 0) throw new InvalidOperationException("Сумма не может быть отрицательной или равна нулю.");
  }

  /// <summary>
  /// Добавить сумму на счет.
  /// </summary>
  /// <param name="amount">Значение суммы.</param>
  public virtual void Deposit(decimal amount)
  {
    EnsureAccountIsOpen();
    EnsurePositiveAmount(amount);
    Balance += amount;
  }

  /// <summary>
  /// Вычесть сумму со счета.
  /// </summary>
  /// <param name="amount">Значение суммы.</param>
  public abstract void Withdraw(decimal amount);

  #endregion

  #region Конструкторы
  /// <summary>
  /// Конструктор счета.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <param name="name">Имя счета.</param>
  /// <param name="accountType">Тип счета.</param>
  /// <param name="currency">Валюта счета.</param>
  public Account(long userId, string name, AccountType accountType, Currency currency)
  {
    UserId = userId;
    AccountType = accountType;
    Currency = currency;
    Rename(name);
    IsClosed = false;
  }

  #endregion
}