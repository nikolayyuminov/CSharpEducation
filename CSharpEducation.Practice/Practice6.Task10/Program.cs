namespace Practice6.Task10;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 10!");
    
    // Тест с числами
    Stack<int> intStack = new Stack<int>();

    intStack.Push(10);
    intStack.Push(20);
    intStack.Push(30);

    Console.WriteLine("Верхний элемент intStack: " + intStack.Peek());
    intStack.Pop(1);
    Console.WriteLine("Количество элементов: " + intStack.Count);

    Console.WriteLine();

    // Тест со строками
    Stack<string> stringStack = new Stack<string>();

    stringStack.Push("Привет");
    stringStack.Push("Мир");

    Console.WriteLine("Верхний элемент stringStack: " + stringStack.Peek());
    stringStack.Pop(1);
    Console.WriteLine("Количество элементов: " + stringStack.Count);
  }
}