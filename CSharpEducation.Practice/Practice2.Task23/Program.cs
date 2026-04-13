namespace Practice2.Task23;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 23!");
    
    Console.Write("Введите год: ");
    if (int.TryParse(Console.ReadLine(), out int result))
    {
      if (result == 400) Console.WriteLine("Год високосный");
      else if (result % 100 == 0) Console.WriteLine("Год НЕ високосный");
      else if (result % 4 == 0) Console.WriteLine("Год високосный");
      else Console.WriteLine("Год НЕ високосный");
    }
    
    
  }
}