namespace Practice2.Task17;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 17!");
    
    int a = 5;
    int b = 10;
    Swap(ref a, ref b);
    Console.WriteLine($"a={a}, b={b}");
  }
  
  static void Swap(ref int a, ref int b)
  {
    (a, b) = (b, a);
  }
}