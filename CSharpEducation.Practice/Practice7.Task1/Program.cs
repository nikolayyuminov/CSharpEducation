using Practice4.MathHelper;

namespace Practice7.Task1;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task1");
    int a;
    int b;
    do
    {
      Console.WriteLine("Введите 2 числа через пробел:");
      string input = Console.ReadLine();
      var parts = input.Split(' ');

      try
      {
        a = int.Parse(parts[0]);
        b = int.Parse(parts[1]);
        break;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Попробуйте ввести число");
        continue;
      }
    } while (true);

    Console.WriteLine(MathHelper.Division(a, b));

  }
  
  
}