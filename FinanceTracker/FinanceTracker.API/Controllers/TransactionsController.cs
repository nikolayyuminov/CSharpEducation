using FinanceTracker.API.Contracts.Transactions;
using FinanceTracker.API.Mappers;
using FinanceTracker.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers;

/// <summary>
/// Контроллер для работы с транзакциями.
/// </summary>
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
  #region Поля и свойства

  /// <summary>
  /// Сервис для работы с транзакциями.
  /// </summary>
  private readonly ITransactionService _transactionService;

  #endregion

  #region Методы

  /// <summary>
  /// Создать транзакцию. 
  /// </summary>
  /// <param name="request">Запрос пользователя для создания транзакции.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost]
  public IActionResult Create(CreateTransactionRequest request)
  {
    var command = TransactionMapper.ToCreateTransactionCommand(request);

    var result = _transactionService.CreateTransaction(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Изменить описание транзакции. 
  /// </summary>
  /// <param name="request">Запрос пользователя на изменение описания транзакции.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost]
  public IActionResult ChangeDescription(ChangeTransactionDescriptionRequest request)
  {
    var command = TransactionMapper.ToChangeTransactionDescriptionCommand(request);

    var result = _transactionService.ChangeDescription(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="transactionService">Сервис для работы с транзакциями.</param>
  public TransactionsController(ITransactionService transactionService)
  {
    _transactionService = transactionService;
  }

  #endregion
}