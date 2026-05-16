namespace Practice5.Task1;

public class Manager : Employee 
{
  public static int TeamSize { get; set; }
  
  public override double CalculateBonus()
  {
    if (TeamSize >= 5)
      return Salary * 0.25;
    return Salary * 0.2;
  }
  
  public  Manager(string name, double salary) : base(name, salary)
  {
    TeamSize++;
  }
}