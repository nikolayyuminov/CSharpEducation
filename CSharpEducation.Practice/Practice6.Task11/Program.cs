namespace Practice6.Task11;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 11!");
    
    int a = 10;
    int b = 11;
    Swap(ref a, ref b);
    Console.WriteLine($"{a}, {b}");
    
    string first = "Hello";
    string second = "World";
    Swap(ref first, ref second);
    Console.WriteLine($"{first}, {second}");

  }

  public static void Swap<T>(ref T a, ref T b)
  {
    T temp = a;
    a = b;
    b = temp;
  }
}