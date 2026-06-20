using System;

namespace Practice7.Task2;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 2!");

    try
    {
      BankAccount bankAccount = new BankAccount();
      bankAccount.Deposit(100);
      Console.WriteLine($"Сейчас на счете {bankAccount.Account}");
      Console.ReadKey();


      DepositBankAccount depositBankAccount = new DepositBankAccount(456);
      
      
      Console.WriteLine($"Сейчас на счете {depositBankAccount.Account}");
      Console.ReadKey();
      depositBankAccount.Deposit(100);
      Console.WriteLine($"Сейчас на счете {depositBankAccount.Account}");
    }
    catch (WithdrawalLimitExceededException e)
    {
      Console.WriteLine($"{e}, попробуйте еще раз");
    }
  }
}