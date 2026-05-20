namespace Practice5.Task3;

class Program
{
  static void Main()
  {
    Console.WriteLine("Task3!");
    
    Dog dog = new Dog("Rex", 5);
    Cat cat = new Cat("Tom", 4);
    dog.MakeSound();
    cat.MakeSound();

    List<Animal> animals = new List<Animal>()
    {
      new Dog("T-Rex", 18),
      new Cat("Tomas", 10),
      new Parrot("Gosha", 1),
    };

    foreach (Animal animal in animals)
    {
      if (animal is Dog)
      {
        animal.MakeSound();
      }
      else if (animal is Cat)
      {
        animal.MakeSound();
      }
      else if (animal is Parrot)
      {
        var parrot = animal as Parrot;
        parrot.MakeSound("some words I can repeat");
      }
    }

    List<IFlyable> bird = new List<IFlyable>()
    {
      new Parrot("Popka Durak", 12),
      new Eagle()
    };
    foreach (IFlyable flyable in bird)
    {
      flyable.Fly();
    }
  }
}