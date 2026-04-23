namespace Practice2.Task3;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача3!");
    
    Console.Write("Введите значение а: ");
    double a = double.Parse(Console.ReadLine());
    if (a == 0)
    {
      throw new DivideByZeroException("Вы будете делить на нуль, так нельзя!");
    }
    Console.Write("Введите значение b: ");
    var b = int.Parse(Console.ReadLine());
    
    Console.Write("Введите значение f: ");
    var f = int.Parse(Console.ReadLine());

    double result = (a + b - f / a) + f * a * a - (a + b);
    Console.WriteLine($"Результат: {result:F2}");
  }
}