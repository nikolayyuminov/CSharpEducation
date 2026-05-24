namespace Practice6.Task2;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 2!");
    
    var cat = new Cat();
    cat.MakeSound();
    var dog = new Dog();
    dog.MakeSound();

    var animals = new List<Animal>()
    {
      new Dog(), new Cat()
    };
    
    foreach (var animal in animals)
    {
      animal.MakeSound();
    }
  }
}