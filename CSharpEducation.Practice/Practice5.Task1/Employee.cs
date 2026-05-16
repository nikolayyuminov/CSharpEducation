namespace Practice5.Task1;

public class Employee
{
  public string Name { get; set; }
  
  public double Salary { get; set; }

  public virtual double CalculateBonus()
  {
    return Salary * 0.1;
  }

  public override string ToString()
  {
    return $"Name: {this.Name}, Salary: {this.Salary}";
  }

  public Employee(string name, double salary)
  {
    Name = name;
    Salary = salary;
  }
}