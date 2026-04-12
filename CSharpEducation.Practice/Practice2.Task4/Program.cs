namespace Practice2.Task4;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 4!");

    TriangleA(5, '@');
    TriangleB(5, 'z');
  }

  static void TriangleA(int a, char b)
  {
    for (int i = 0; i <= a; i++)
    {
      for (int j = 0; j <= i; j++)
      {
        Console.Write(b);
      }
      Console.WriteLine();
    }
  }
  
  static void TriangleB(int a, char b)
  {
    for (int i = 0; i <= a; i++)
    {
      for (int j = a; j > i; j--)
      {
        Console.Write(" ");
      }
      for (int j = 0; j <= i; j++)
      {
        Console.Write(b);
      }
      Console.WriteLine();
    }
  }
}