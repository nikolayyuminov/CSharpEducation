namespace Employee;

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