namespace Practice6.Task10;

public class Stack<T>
{
  private List<T> items = new List<T>();

  // Добавление элемента
  public void Push(T item)
  {
    items.Add(item);
  }
  
  /// <summary>
  /// Удаление элемента по индексу
  /// </summary>
  /// <param name="index">индекс</param>
  /// <exception cref="InvalidOperationException">стек пуск</exception>
  /// <exception cref="IndexOutOfRangeException">индекс вне диапазона списка</exception>
  public void Pop(int index)
  {
    if (items.Count == 0)
      throw new InvalidOperationException("Стек пуст.");
    if (index < 0 || index >= items.Count)
      throw new IndexOutOfRangeException("индекс вне диапазона списка");

    T item = items[index];
    Console.WriteLine($"Удаление элемента: {item}");
    items.RemoveAt(index);
  }

  // Получение верхнего элемента без удаления
  public T Peek()
  {
    if (items.Count == 0)
      throw new InvalidOperationException("Стек пуст.");

    return items[items.Count - 1];
  }

  // Проверка количества элементов
  public int Count => items.Count;
}