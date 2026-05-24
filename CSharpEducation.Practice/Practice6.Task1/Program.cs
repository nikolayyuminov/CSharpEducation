namespace Practice6.Task1;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 1!");
    
    var pi = Math.PI;
    var circle = new Circle();
    var rectangle = new Rectangle();

    Console.WriteLine($"Радиус круга: {circle.CalculateArea(pi, 15):F2}");
    Console.WriteLine($"Радиус прямоугольника: {rectangle.CalculateArea(20, 15)}");
  }
}