namespace Practice6.Task12;

public class MyDictionary <TKey, TValue>
{
  private List<TKey> keys = new List<TKey>();
  private List<TValue> values = new List<TValue>();
  
  // Добавление элемента
  public void Add(TKey key, TValue value)
  {
    if (ContainsKey(key))
      throw new ArgumentException("Ключ уже существует.");

    keys.Add(key);
    values.Add(value);
  }

  // Удаление элемента по ключу
  public bool Remove(TKey key)
  {
    int index = keys.IndexOf(key);

    if (index == -1)
      return false;

    keys.RemoveAt(index);
    values.RemoveAt(index);

    return true;
  }

  // Проверка наличия ключа
  public bool ContainsKey(TKey key)
  {
    return keys.Contains(key);
  }

  // Получение значения по ключу
  public bool TryGetValue(TKey key, out TValue value)
  {
    int index = keys.IndexOf(key);

    if (index != -1)
    {
      value = values[index];
      return true;
    }

    value = default(TValue);
    return false;
  }

  // Количество элементов
  public int Count => keys.Count;
}