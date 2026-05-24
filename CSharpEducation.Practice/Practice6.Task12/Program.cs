namespace Practice6.Task12;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 12!");
    
    var students = new MyDictionary<int, string>();

    // Добавление элементов
    students.Add(1, "Алексей");
    students.Add(2, "Мария");
    students.Add(3, "Иван");

    // Проверка ключа
    Console.WriteLine("Есть ли ключ 2: " + students.ContainsKey(2));

    // Получение значения
    if (students.TryGetValue(3, out string name))
    {
      Console.WriteLine("Значение ключа 3: " + name);
    }

    // Удаление элемента
    Console.WriteLine(students.Remove(2));
    

    Console.WriteLine("После удаления ключа 2:");
    Console.WriteLine("Есть ли ключ 2: " + students.ContainsKey(2));

    Console.WriteLine("Количество элементов: " + students.Count);
  }
}