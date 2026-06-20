using System;

namespace Practice7.Task3;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task3!");
    var attemptCount = 0;
    do
    {
      try
      {
        if (attemptCount >= 5)
        {
          throw new TooManyAttemptsException("Слишком много попыток");
          
        }
        var age = Age();
        CheckAdult(age);
        Console.WriteLine($"Ваш возраст {age}");

      }
      catch (ArgumentException e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Попробуйте еще раз");
        attemptCount++;
      }
      catch (TooManyAttemptsException e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Программа завершена");
        attemptCount++;
      }
    } while (attemptCount <= 5);
    
    

  }

  public static int Age()
  {
    Console.Write("Введите возраст: ");
    var input = Console.ReadLine();
    var isParse = int.TryParse(input, out int result);
    if (!isParse)
    {
      throw new ArgumentException("Введите число");
    }
    return result;
  }

  public static void CheckAdult(int age)
  {
    if (age < 18)
    {
      throw new ArgumentException("Ваш возраст меньше 18");
    }
  }
}