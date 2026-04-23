namespace Practice2.Task16;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 16!");
    int number = -146;
    ReferenceInt(ref number);
    Console.WriteLine(number);
  }

  static void ReferenceInt(ref int number)
  {
    number = -number;
  }
}