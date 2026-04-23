namespace Practice2.Task5;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 5!");
    
    Console.Write("Введите 2 числа через пробел: ");
    string input = Console.ReadLine();
    if (!int.TryParse(input.Split(' ')[0], out int a))
    {
      throw new Exception("Первый символ не целое число");
    }
    if (!int.TryParse(input.Split(' ')[1], out int b))
    {
      throw new Exception("Второй символ не целое число");
    }

    if (a < b)
    {
      Console.Write(b);
    }
    else if  (a > b)
    {
      Console.Write(b);
    }
    else
    {
      Console.Write("Числа равны");
    }
  }
}