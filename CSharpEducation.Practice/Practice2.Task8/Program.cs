namespace Practice2.Task8;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 8!");
    
    bool isEqual = true;
    Console.Write("Введите число 1: ");
    var a = int.Parse(Console.ReadLine());
    Console.Write("Введите число 2: ");
    var b = int.Parse(Console.ReadLine());
    Console.Write("Введите число 3: ");
    var c = int.Parse(Console.ReadLine());

    if (a != b && c != a && c != b)
    {
      Console.WriteLine("Равных нет");
    }
    else
    {
      a +=5;
      b +=5;
      c +=5;
      Console.WriteLine($"{a}, {b}, {c}");
    }
  }
}