namespace Practice6.Task9;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 9!");
    
    var person = new Person("Ivan", 15);
    var person0 = new Person("Ivan", 15);
    var person1 = new Person("Den", 15);
    var person2 = new Person("Pen", 15);
    var person3 = new Person("Zac", 15);

    Console.WriteLine(person.Equals(person0));
    Console.WriteLine(person.Equals(person1));
    Console.WriteLine(person2.GetHashCode());
    Console.WriteLine(person3.ToString());
  }
}