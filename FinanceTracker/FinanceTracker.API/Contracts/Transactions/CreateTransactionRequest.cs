using FinanceTracker.Domain.Enums;

namespace FinanceTracker.API.Contracts.Transactions;

/// <summary>
/// Запрос от пользователя с данными для создания транзакции.
/// </summary>
public sealed record CreateTransactionRequest
{
  /// <summary>
  /// Id счета, которому принадлежит транзакция.
  /// </summary>
  public long AccountId { get; init; }

  /// <summary>
  /// Id категории транзакции. Может отсутствовать.
  /// </summary>
  public long? CategoryId { get; init; }
  
  /// <summary>
  /// Вид операции.
  /// Используется только если категория не указана.
  /// </summary>
  public TransactionKind Kind { get; init; }

  /// <summary>
  /// Значение суммы транзакции. Только положительное значение.
  /// </summary>
  public decimal Amount { get; init; }

  /// <summary>
  /// Описание транзакции. Может отсутствовать.
  /// </summary>
  public string? Description { get; init; }
}