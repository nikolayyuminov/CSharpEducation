namespace Practice5.Task1;

public class Contractor : Employee
{
  public double HorlyRate { get; set; }

  public new double CalculateBonus(int hoursWorked)
  {
    return Salary * HorlyRate * hoursWorked;
  }
  public Contractor(string firstName, double salary, double horlyRate ) : base(firstName, salary)
  {
    HorlyRate = horlyRate;
  }
}