namespace Practice6.Task3;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 3!");
    
    Celsius c = new Celsius(90);
    Console.WriteLine(c.degrees);
    Fahrenheit f = c;
    Console.WriteLine(f.degrees);
    Celsius c2 = (Celsius)f;
    Console.WriteLine(c2.degrees);
    Console.WriteLine();

    Fahrenheit f2 = new Fahrenheit(20);
    Console.WriteLine(f2.degrees);
    Celsius c3 = (Celsius)f2;
    Console.WriteLine(c3.degrees);
    Fahrenheit f3 = c3;
    Console.WriteLine(f3.degrees);

    int a = (int)3.14;
  }
}