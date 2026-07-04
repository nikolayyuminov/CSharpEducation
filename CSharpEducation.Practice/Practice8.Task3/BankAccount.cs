namespace Practice8.Task3;

public delegate void AccountTransactionDelegate(double sum, Operation operation);

public class BankAccount
{
  public double Balance { get; protected set; }
  
  public event AccountTransactionDelegate OnTransaction;

  public virtual void Deposit(double amount)
  {
    if (amount <= 0)
    {
      throw new ArgumentException("Нельзя положить отрицательную сумму!");
    }
    Balance +=  amount;
    OnTransaction?.Invoke(amount, Operation.Deposit);
    Console.WriteLine($"Пополнение на {amount}. Баланс: {Balance}");
  }

  public virtual void Withdraw(double amount)
  {
    if (amount > Balance)
    {
      throw new ArgumentException("Недостаточно средств!");
    }
    Balance -=  amount;
    OnTransaction?.Invoke(amount, Operation.Withdraw);
    Console.WriteLine($"Снятие на {amount}. Баланс: {Balance}");
  }
  public virtual void Transfer(double amount, Operation operation)
  {
    switch (operation)
    {
      case Operation.Deposit:
        Deposit(amount);
        break;
      case Operation.Withdraw:
        Withdraw(amount);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(operation), operation, "Выбран не существующий вид операции");
    }
  }
  

  public BankAccount()
  {
    Balance = 0;
  }
}