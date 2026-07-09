namespace Employee;

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