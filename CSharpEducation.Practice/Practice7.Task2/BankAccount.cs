namespace Practice7.Task2;

public class BankAccount
{
  public double Account { get; protected set; }

  public virtual void Deposit(double amount)
  {
    if (amount <= 0)
    {
      throw new WithdrawalLimitExceededException("Нельзя положить отрицательную сумму!");
    }
    Account +=  amount;
  }

  public virtual void Withdraw(double amount)
  {
    if (amount > Account)
    {
      throw new WithdrawalLimitExceededException("Недостаточно средств!");
    }
    Account -=  amount;
  }

  public BankAccount()
  {
    Account = 0;
  }
}