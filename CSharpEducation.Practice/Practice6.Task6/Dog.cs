namespace Practice6.Task6;

public class Dog : Animal
{
  public override void Move()
  {
    base.Move();
    Console.WriteLine("I'm a dog");
  }
}