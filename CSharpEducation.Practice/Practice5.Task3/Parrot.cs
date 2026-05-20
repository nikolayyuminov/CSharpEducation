namespace Practice5.Task3;

public class Parrot : Animal, IFlyable
{
  public string Color { get; set; }

  public new void MakeSound()
  {
    Console.WriteLine("Parrot is talking");
  }

  public void MakeSound(string words)
  {
    Console.WriteLine(words);
  }
  
  #region Конструктор

  public Parrot(string name, int age) : base(name, age)
  {
    Console.WriteLine( "Parrot is created!");
  }

  #endregion

  public void Fly()
  {
    Console.WriteLine("Parrot is flying!");
  }
}