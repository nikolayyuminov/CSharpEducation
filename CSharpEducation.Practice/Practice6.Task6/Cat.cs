namespace Practice6.Task6;

public class Cat : Animal
{
  public override void Move()
  {
    base.Move();
    Console.WriteLine("I'm a Cat");
  }
}