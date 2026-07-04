using System.Runtime.CompilerServices;

namespace Employee;
/// <summary>
/// Управление сотрудником.
/// </summary>
public class EmployeeManager
{
    #region Поля и свойства

    public static List<Employee> Employees = new List<Employee>(); 

    #endregion

    /// <summary>
    ///Добавление нового сотрудника.
    /// </summary>
    public static void AddNewEmployee(string name, string position, decimal hourRate, int hoursWorked)
    {
        var emp = new Employee(name, position, hourRate, hoursWorked);

        Employees.Add(emp);
    }
    
    public static Employee? FindEmployeeByName(string name)
    {
        var emp = Employees.FirstOrDefault(e => e.Name.Equals(name));
        return emp;
    }

    /// <summary>
    /// Изменить данные сотрудника
    /// </summary>
    public static void UpdateEmployee(Employee emp, string newName, string newPosition, decimal newHourRate, int newHoursWorked)
    {
        if (!newName.Equals(string.Empty))
        {
            if (!newName.Equals(emp.Name))
                emp.Name = newName;
        }
        
        if (!newPosition.Equals(string.Empty))
        {
            if (!newPosition.Equals(emp.Position))
                emp.Position = newPosition;
        }

        if (newHourRate != 0)
        {
            if (newHourRate != emp.HoursWorked)
                emp.HourRate = newHourRate;
        }

        if (newHoursWorked != emp.HoursWorked)
            emp.HoursWorked = newHoursWorked;
    }

    /// <summary>
    /// Рассчитать зарплату сотрудника.
    /// </summary>
    /// <returns>Зарплата сотрудника.</returns>
    public static decimal? CalculateSalary(Employee emp)
    {
        if (emp != null) return emp.HourRate * emp.HoursWorked;
        return null;
    }



}