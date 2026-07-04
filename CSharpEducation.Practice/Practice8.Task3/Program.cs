namespace Practice8.Task3;

class Program
{
 
  static void Main(string[] args)
  {
    Console.WriteLine("Task 3!");
    
    var bankAccount = new BankAccount();
    double sum = 0;
    AccountTransactionDelegate transfer = bankAccount.Transfer;
    bankAccount.OnTransaction += LogTransaction;
    bankAccount.OnTransaction += ShowTransaction;
    try
    {
      transfer(10, Operation.Deposit);
      transfer(10, Operation.Withdraw);
      transfer(10, Operation.Withdraw);
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }

    Console.WriteLine("Press any key to continue ...");
    Console.ReadKey();
  }
  
  // Обработчик №1
  static void LogTransaction(double amount, Operation operation)
  {
    Console.WriteLine($"[ЛОГ] {operation}: {amount} руб.");
  }

  // Обработчик №2
  static void ShowTransaction(double amount, Operation operation)
  {
    Console.WriteLine($"Выполнена операция: {operation} на сумму {amount} руб.");
  }
}