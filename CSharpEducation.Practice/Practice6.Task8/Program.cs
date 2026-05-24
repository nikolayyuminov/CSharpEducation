namespace Practice6.Task8;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 8!");

    var vehicles = new List<Vehicle>()
    {
      new Car(),
      new Airplane(),
      new Bicycle()
    };
    foreach (var vehicle in vehicles)
    {
      vehicle.Move();
    }
  }
}