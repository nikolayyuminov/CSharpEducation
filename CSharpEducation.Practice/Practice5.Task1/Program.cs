using System;
using System.Collections.Generic;

namespace Practice5.Task1;

class Program
{
  static void Main()
  {
    Console.WriteLine("Task 1");
    
    var employee = new Employee("John Doe", 100);
    Console.WriteLine(employee.CalculateBonus());
    
    Employee manager = new Manager("Ivan", 150);
    Console.WriteLine(manager.CalculateBonus());

    var employees = new List<Employee>()
    {
      new Employee("emp1", 100),
      new Manager("manag1", 100),
      new Manager("manag2", 100),
      new Contractor("contr1", 100, 0.01)
    };

    foreach (var emp in employees)
    {
      if (emp is Contractor)
      {
        var empcontractor = emp as Contractor;
        Console.WriteLine($"{empcontractor}, {empcontractor.CalculateBonus(100)}");
        continue;
      }
      Console.WriteLine($"{emp}, {emp.CalculateBonus()}");
    }
  }
  
}