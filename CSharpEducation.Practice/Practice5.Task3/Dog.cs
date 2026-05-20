namespace Practice5.Task3;

public class Dog : Animal
{
  public override void MakeSound()
  {
    Console.WriteLine("Woof!");
  }

  #region Конструктор

  public Dog(string name, int age) : base(name, age)
  {
    Console.WriteLine("Dog is created!");
  }

  #endregion
}