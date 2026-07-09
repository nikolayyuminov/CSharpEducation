
namespace Employee;
/// <summary>
/// Управление сотрудником.
/// </summary>
public class EmployeeManager<T> : IEmployeeManager<T> where T : Employee
{
    #region Поля и свойства

    /// <summary>
    /// Коллекция для работы со списком сотрудников.
    /// </summary>
    private readonly List<T?> _employees = [];
    
    /// <summary>
    /// Значение id, для создания нового сотрудника
    /// </summary>
    private int IdMax => _employees.Count + 1;

    #endregion

    #region Методы

    /// <summary>
    /// Добавление нового сотрудника в коллекцию.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    public void Add(T employee)
    {
        if (employee.Id < IdMax) throw new IdAlreadyExistException($"ИД с номером {employee.Id} уже существует");
        _employees.Add(employee);
    }
    
    /// <summary>
    ///Создание нового полного сотрудника.
    /// </summary>
    public FullTimeEmployee CreateNewEmployee(string name, decimal salary)
    {
        var emp = new FullTimeEmployee(IdMax, name, salary);
        return emp;
    }
    
    /// <summary>
    ///Добавление нового частичного сотрудника.
    /// </summary>
    public PartTimeEmployee CreateNewEmployee(string name, decimal salary, int hoursWorked) 
    {
        var emp = new PartTimeEmployee(IdMax, name, salary,  hoursWorked);
        return emp;
    }
    
    /// <summary>
    /// Получить сотрудника по имени.
    /// </summary>
    /// <param name="name">Имя сотрудника.</param>
    /// <returns>Сотрудник.</returns>
    /// <exception cref="NullReferenceException">Списка сотрудников не существует.</exception>
    public T? Get(string name)
    {
        if  (_employees == null) throw new NullReferenceException("Список сотрудников не существует");
        var emp = _employees.FirstOrDefault(e => e.Name.Equals(name));
        return emp ?? throw new IdNotFoundException($"Сотрудника с ИД {emp.Id} Не существует");
    }

    /// <summary>
    /// Изменить данные сотрудника
    /// </summary>
    /// <param name="emp">Сотрудник</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Update(T emp)
    {
        Console.Write("Новое имя: ");
        var newName = Console.ReadLine() ?? throw new InvalidOperationException("Имя не может быть пустым");
        if (newName.Equals(string.Empty)) throw new ArgumentNullException(newName,"Новое имя не может быть пустым");
        if (!newName.Equals(emp.Name)) emp.Name = newName;
    
        Console.Write("Новая базовая зарплата: ");
        var newSalary = 
            decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Зарплата не может быть пустой"));
        if (newSalary != emp.BaseSalary) emp.BaseSalary = newSalary;

        if (emp is not PartTimeEmployee partTimeEmp) return;
        Console.Write("Новое количество часов: ");
        var newHoursWorked =
            int.Parse(Console.ReadLine() ??
                      throw new InvalidOperationException("Количество отработанных часов не может быть пустым"));
        if (newHoursWorked != partTimeEmp.HoursWorked) partTimeEmp.HoursWorked = newHoursWorked;
    }

    public List<T?> GetAll()
    {
        if (_employees == null)
            throw new NullReferenceException("Список сотрудников пуст.");

        return _employees.Count == 0 ? throw new NullReferenceException("Количество сотрудников '0'") : _employees;
    }
    
    public void DeleteEmployee(T emp)
    {
        if (emp.Id > IdMax) throw new IdNotFoundException($"Пользователя с {emp.Id} не существует");
        _employees.Remove(emp);
    }
    
    #endregion
}