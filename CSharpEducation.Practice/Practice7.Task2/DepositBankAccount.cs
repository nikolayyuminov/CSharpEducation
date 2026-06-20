using System;

namespace Practice7.Task2;

public class DepositBankAccount : BankAccount
{
  public DateTime dateOfLastWithdrawal { get; protected set; }

  public TimeSpan checkLastDateOfWithdrawal()
  {
    return DateTime.Now - dateOfLastWithdrawal;
  }

  public override void Withdraw(double amount)
  {
    var check = checkLastDateOfWithdrawal().TotalDays;
    if (check < 30)
    {
      throw new WithdrawalLimitExceededException("В этом месяце уже снимали деньги!");
    }
    base.Withdraw(amount);
    dateOfLastWithdrawal = DateTime.Now;
  }

  public DepositBankAccount(double amount)
  {
    Account = amount;
  }
}