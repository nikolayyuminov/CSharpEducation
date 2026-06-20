using System;

namespace Practice7.Task2;

public class WithdrawalLimitExceededException : Exception
{
  public WithdrawalLimitExceededException(string message, Exception innerException) : base(message, innerException) { }
  
  public WithdrawalLimitExceededException(string? message) : base(message) { }
}