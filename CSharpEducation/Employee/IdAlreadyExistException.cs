namespace Employee;
/// <summary>
/// Выбрасывается, если ID сотрудника уже существует.
/// </summary>
public class IdAlreadyExistException : Exception
{
  public IdAlreadyExistException()
  {
  }

  public IdAlreadyExistException(string? message)
    : base(message)
  {
  }
}