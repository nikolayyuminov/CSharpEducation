namespace Practice6.Task7;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Hello, World!");
    
    var pi = Math.PI;
    
    var circle = new Circle();
    var rectangle = new Rectangle();
    var triangle = new Triangle();

    Console.WriteLine(circle.CalculateArea(pi, 56));
    Console.WriteLine(rectangle.CalculateArea(15, 20));
    Console.WriteLine(triangle.CalculateArea(19, 5));
  }
}