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

  public Transaction(long accountId, long? categoryId, decimal amount, string? description, TransactionKind? kind)
  {
    AccountId = accountId;
    Kind = kind;
    Amount = amount;
    CreatedAt = DateTime.Now;
    CategoryId = categoryId;
    Description = description;
  }

  #endregion
}