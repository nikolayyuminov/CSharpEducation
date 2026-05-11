using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhoneBook;

/// <summary>
/// Телефонная книга.
/// </summary>
public class Phonebook
{
    #region Поля и свойства

    /// <summary>
    /// Приватное поле для экземпляра телефонной книги.
    /// </summary>
    private static Phonebook _instance;
    
    /// <summary>
    /// Приватное поле для списка всех абонентов телефонной книги.
    /// </summary>
    private List<Abonent> _abonents;
    
    /// <summary>
    /// Приватное поле для хранения пути до файла со всеми абонентами.
    /// </summary>
    private readonly string _filePath = "phonebook.txt";
    
    /// <summary>
    /// Получить телефонную книгу.
    /// </summary>
    /// <returns>Объект класса PhoneBook.</returns>
    public static Phonebook Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Phonebook();
            return _instance;
        }
    }
    
    #endregion

    #region Методы
    
    /// <summary>
    /// Загрузка данных из файла "phonebook.txt".
    /// </summary>
    private void LoadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("Файл не найден... \n" +
                              "Новый файл создастся при добавлении первого абонента.");
            return;
        }
        
        try
        {
            var lines = File.ReadAllLines(_filePath, Encoding.UTF8);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    break;
                    
                var parts = line.Split(':');
                var name = parts[0].Trim();
                var phoneNumber = parts[1].Trim();
                
                _abonents.Add(new Abonent(phoneNumber, name));
                
            }
            Console.WriteLine($"Загружено {_abonents.Count} контактов из файла.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
            throw;
        }
    }
        
    /// <summary>
    /// Сохранение данных в файл "phonebook.txt".
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            var lines = _abonents.Select(a => $"{a.Name} : {a.PhoneNumber}");
            File.WriteAllLines(_filePath, lines, Encoding.UTF8);
            Console.WriteLine("Данные сохранены в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            throw;
        }
    }
            
    /// <summary>
    /// Добавление абонента в телефонную книгу.
    /// </summary>
    /// <param name="phoneNumber">Телефон абонента.</param>
    /// <param name="name">Имя абонента.</param>
    public void AddAbonent(string phoneNumber, string name)
    {
        if (_abonents.Any(a => a.PhoneNumber == phoneNumber))
        {
            Console.WriteLine($"Ошибка: Абонент с номером {phoneNumber} уже существует!");
            return;
        }
    
        if (_abonents.Any(a => a.Name.Equals(name)))
        {
            Console.WriteLine($"Ошибка: Абонент с именем {name} уже существует!");
            return;
        }
    
        var newAbonent = new Abonent(phoneNumber, name);
        _abonents.Add(newAbonent);
        SaveToFile(); // Переписываем файл с новыми данными
        Console.WriteLine($"Абонент {name} успешно добавлен.");
    }
    
    /// <summary>
    /// Получить абонента по номеру телефона.
    /// </summary>
    /// <param name="phoneNumber">Телефон абонента.</param>
    /// <returns>Абонент телефонной книги.</returns>
    public Abonent? GetAbonentByPhone(string phoneNumber)
    {
        foreach (var abonent in _abonents)
        {
            if  (abonent.PhoneNumber == phoneNumber)
                return abonent;
        }
        return null;

    }
    
    /// <summary>
    /// Получить номер телефона по имени абонента.
    /// </summary>
    /// <param name="name">Имя абонента.</param>
    /// <returns>Абонент телефонной книги.</returns>
    public Abonent? GetPhoneByName(string name)
    {
        foreach (var abonent in _abonents)
        {
            if  (abonent.Name.ToLower() == name.ToLower())
                return abonent;
        }
        return null;

    }

    /// <summary>
    /// Изменить данные абонента.
    /// </summary>
    /// <param name="abonent">Абонент, которого надо изменить.</param>
    /// <param name="newPhoneNumber">Новое значение телефона абонента.</param>
    /// <param name="newName">Новое значение имени абонента.</param>
    public void UpdateAbonent(Abonent abonent, string newPhoneNumber, string newName)
    {
        if (newPhoneNumber != abonent.PhoneNumber && _abonents.Any(a => a.PhoneNumber == newPhoneNumber))
        {
            Console.WriteLine($"Ошибка: Номер {newPhoneNumber} уже занят другим абонентом.");
            return;
        }

        if (!abonent.Name.Equals(newName) && _abonents.Any(a => a.Name.Equals(newName)))
        {
            Console.WriteLine($"Ошибка: Имя {newName} уже используется другим абонентом.");
            return;
        }
        
        abonent.PhoneNumber = newPhoneNumber;
        abonent.Name = newName;
        SaveToFile();
        Console.WriteLine("Данные абонента обновлены.");
    }

    /// <summary>
    /// Удалить абонента из телефонной книги.
    /// </summary>
    /// <param name="abonent">Абонент, которого надо удалить из телефонной книги.</param>
    public void DeleteAbonent(Abonent abonent)
    {
        _abonents.Remove(abonent);
        SaveToFile();
        Console.WriteLine($"Абонент {abonent.Name} удален.");
    }
    
    /// <summary>
    /// Показать все записи телефонной книги.
    /// </summary>
    public List<Abonent> GetAllAbonents()
    {
        return _abonents;
    }
    #endregion
    
    #region Конструктор
    
    /// <summary>
    /// Создается телефонная книга из файла phonebook.txt.
    /// Если файла нет, то создается пустой файл.
    /// </summary>
    private Phonebook()
    {
        _abonents = new List<Abonent>();
        LoadFromFile();
    }

    #endregion
}