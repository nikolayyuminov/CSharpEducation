namespace FinanceTracker.Application.Common.Validation;

/// <summary>
/// Сообщение валидации для определенного свойства.
/// </summary>
public class ValidationError
{
  /// <summary>
  /// Имя свойства, для которого нужно сообщение валидации.
  /// </summary>
  public string PropertyName { get; init; }
  
  /// <summary>
  /// Сообщение валидации.
  /// </summary>
  public string ErrorMessage { get; init; }
  
  /// <summary>
  /// Конструктор сообщения валидации.
  /// </summary>
  /// <param name="propertyName">Имя свойства.</param>
  /// <param name="errorMessage">Сообщение.</param>
  public ValidationError(string propertyName, string errorMessage)
  {
    PropertyName = propertyName;
    ErrorMessage = errorMessage;
  }
}