using System.Text;

namespace Employee;
/// <summary>
/// Управление сотрудником.
/// </summary>
public static class EmployeeManager
{
    #region Поля и свойства

    /// <summary>
    /// Коллекция для работы со списком сотрудников.
    /// </summary>
    public static readonly List<Employee>? Employees = [];

    /// <summary>
    /// Файл хранения списка всех сотрудников.
    /// </summary>
    private const string? FilePath = "Employee.txt";

    #endregion

    #region Методы

    /// <summary>
    /// Загрузка данных из файла "Employee.txt".
    /// </summary>
    public static void LoadFromFile()
    {
        if (!File.Exists(FilePath))
        {
            throw new FileNotFoundException("Файл не найден... \n" +
                                            "Новый файл создастся при добавлении первого сотрудника.");
        }
        var lines = File.ReadAllLines(FilePath, Encoding.UTF8);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new InvalidOperationException("Файл пуст...");
                
            var parts = line.Split(':');
            var id = int.Parse(parts[0].Trim());
            var name =  parts[1].Trim();
            var position = parts[2].Trim();
            var hourRate = decimal.Parse(parts[3].Trim());
            var hoursWorked = int.Parse(parts[4].Trim());
            
            Employees?.Add(new Employee(id, name, position, hourRate, hoursWorked));
            
        }

        if (Employees == null) throw new NullReferenceException("Список сотрудников не существует");
        var lastId = Employees.Max(emp => emp.Id);
        Employee.CountId = lastId + 1;
        Console.WriteLine($"Загружено {Employees.Count} сотрудников из файла.");

    }
        
    /// <summary>
    /// Сохранение данных в файл "Employee.txt".
    /// </summary>
    private static void SaveToFile()
    {
        try
        {
            if (Employees == null) throw new NullReferenceException("Ошибка при сохранении");
            
            var lines = Employees.Select(e => $"{e.Id}:{e.Name} : {e.Position} : {e.HourRate} : {e.HoursWorked}");
            File.WriteAllLines(FilePath, lines, Encoding.UTF8);

            Console.WriteLine("Данные сохранены в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///Добавление нового сотрудника.
    /// </summary>
    public static void AddNewEmployee(string name, string position, decimal hourRate, int hoursWorked)
    {
        var emp = new Employee(name, position, hourRate, hoursWorked);

        if (Employees != null) Employees.Add(emp);
        else throw new NullReferenceException("Список сотрудников не существует");
        SaveToFile();
    }
    
    public static Employee? FindEmployeeByName(string name)
    {
        if  (Employees == null) throw new NullReferenceException("Список сотрудников не существует");
        var emp = Employees.FirstOrDefault(e => e.Name.Equals(name));
        return emp;
    }

    /// <summary>
    /// Изменить данные сотрудника
    /// </summary>
    public static void UpdateEmployee(Employee emp, string newName, string newPosition, decimal newHourRate, int newHoursWorked)
    {
        if (newName.Equals(string.Empty)) throw new ArgumentNullException(newName,"Новое имя не может быть пустым");
        if (!newName.Equals(emp.Name)) emp.Name = newName;
    
        
        if (newPosition.Equals(string.Empty)) throw new ArgumentNullException(newPosition, "Новая должность не может быть пустой"); 
        if (!newPosition.Equals(emp.Position)) emp.Position = newPosition;

        if (newHourRate != emp.HoursWorked) emp.HourRate = newHourRate;

        if (newHoursWorked != emp.HoursWorked) emp.HoursWorked = newHoursWorked;
        
        SaveToFile();
    }

    /// <summary>
    /// Рассчитать зарплату сотрудника.
    /// </summary>
    /// <returns>Зарплата сотрудника.</returns>
    public static decimal CalculateSalary(Employee? emp)
    {
        if (emp != null) return emp.HourRate * emp.HoursWorked;
        throw new NullReferenceException("Сотрудник не найден");
    }
    #endregion
}