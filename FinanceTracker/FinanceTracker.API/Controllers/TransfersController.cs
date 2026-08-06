using FinanceTracker.API.Contracts.Transfers;
using FinanceTracker.API.Mappers;
using FinanceTracker.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers;

/// <summary>
/// Контроллер для работы с переводами между счетами.
/// </summary>
[ApiController]
[Route("api/transfers")]
public class TransfersController : ControllerBase
{
  #region Поля и свойства

  /// <summary>
  /// Сервис для работы с переводами.
  /// </summary>
  private readonly ITransferService _transferService;

  #endregion

  #region Методы

  /// <summary>
  /// Перевести средства между двумя счетами. 
  /// </summary>
  /// <param name="request">Запрос от пользователя на перевод средств между счетами.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost]
  public ActionResult Transfer([FromBody] TransferMoneyRequest request)
  {
    var command = TransferMapper.ToTransferMoneyCommand(request);

    var result = _transferService.Transfer(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="transferService">Сервис для работы с переводами.</param>
  public TransfersController(ITransferService transferService)
  {
    _transferService = transferService;
  }

  #endregion
}