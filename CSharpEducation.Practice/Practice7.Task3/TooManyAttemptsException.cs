using System;

namespace Practice7.Task3;

public class TooManyAttemptsException : Exception
{
  public TooManyAttemptsException() : base()
  {
  }

  public TooManyAttemptsException(string message) : base(message)
  {
    
  }
}