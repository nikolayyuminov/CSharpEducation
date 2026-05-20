namespace Practice5.Task3;

public class Cat : Animal
{
  public override void MakeSound()
  {
    Console.WriteLine("Meow!");
  }
  
  #region Конструктор

  public Cat(string name, int age) : base(name, age)
  {
    Console.WriteLine("Cat is created!");
  }

  #endregion
}