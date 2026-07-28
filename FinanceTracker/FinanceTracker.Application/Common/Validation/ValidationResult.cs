namespace FinanceTracker.Application.Common.Validation;

/// <summary>
/// Коллекция ошибок для пользователя.
/// </summary>
public class ValidationResult
{
  #region Поля и свойства

  /// <summary>
  /// Внутренняя коллекция сообщений об ошибках.
  /// </summary>
  private readonly List<ValidationError> _errors = [];
  
  /// <summary>
  /// Неизменяемая коллекция, для просмотра списка сообщений об ошибках
  /// </summary>
  public IReadOnlyCollection<ValidationError> Errors => _errors;
  
  /// <summary>
  /// Есть ли ошибки в коллекции.
  /// </summary>
  public bool HasErrors => Errors.Count > 0;

  #endregion

  #region Методы
  
  /// <summary>
  /// Добавить сообщение об ошибке в коллекцию.
  /// </summary>
  /// <param name="validationError">Сообщение валидации для определенного свойства.</param>
  public void AddError(ValidationError? validationError)
  {
    if (validationError != null) _errors.Add(validationError);
  }

  #endregion
}