using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Транзакция.
/// </summary>
public class Transaction
{
  #region Поля и свойства

  /// <summary>
  /// Уникальный идентификатор транзакции.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// Id счета, которому принадлежит транзакция.
  /// </summary>
  public long AccountId { get; init; }

  /// <summary>
  /// Id категории, которой принадлежит транзакция. Может отсутствовать.
  /// </summary>
  public long? CategoryId { get; init; }
  
  /// <summary>
  /// Идентификатор перевода.
  /// Заполняется только для операций перевода.
  /// </summary>
  public Guid? TransferId { get; init; }

  /// <summary>
  /// Значение суммы транзакции. Только положительное значение.
  /// </summary>
  public decimal Amount { get; init; }

  /// <summary>
  /// Вид транзакции. (Входящая/Исходящая).
  /// </summary>
  public TransactionKind? Kind { get; init; }
  
  /// <summary>
  /// Время проведения транзакции.
  /// </summary>
  public DateTime CreatedAt { get; init; }

  /// <summary>
  /// Описание транзакции. Может отсутствовать.
  /// </summary>
  public string? Description { get; private set; }

  #endregion

  #region Методы

  public void ChangeDescription(string? newDescription)
  {
    if (string.Equals(Description, newDescription)) return;
    
    Description = newDescription;
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="accountId">Id счета.</param>
  /// <param name="categoryId">Id категории.</param>
  /// <param name="amount">Сумма транзакции.</param>
  /// <param name="description">Описание транзакции.</param>
  /// <param name="kind">Вид транзакции.</param>
  /// <param name="transferId">Идентификатор перевода между счетами.</param>
  public Transaction(long accountId, long? categoryId, decimal amount, string? description, TransactionKind? kind, Guid? transferId = null)
  {
    AccountId = accountId;
    Kind = kind;
    TransferId = transferId;
    Amount = amount;
    CreatedAt = DateTime.Now;
    CategoryId = categoryId;
    Description = description;
  }

  #endregion
}