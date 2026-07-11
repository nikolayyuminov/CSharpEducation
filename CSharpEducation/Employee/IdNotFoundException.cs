namespace Employee;
/// <summary>
/// Выбрасывается, когда ID сотрудника не найден.
/// </summary>
public class IdNotFoundException : Exception
{
  public IdNotFoundException()
  {
  }

  public IdNotFoundException(string? message)
    : base(message)
  {
  }
}