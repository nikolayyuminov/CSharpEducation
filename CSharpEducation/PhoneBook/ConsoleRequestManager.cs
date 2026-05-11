using System;

namespace PhoneBook;

/// <summary>
/// Менеджер для обработки запросов пользователя к телефонной книге
/// </summary>
public static class ConsoleRequestManager
{
    /// <summary>
    /// Добавить абонента в телефонную книгу.
    /// </summary>
    /// <param name="phonebook">Телефонная книга.</param>
    public static void AddAbonent(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона: ");
        var phone = Console.ReadLine();
        
        Console.Write("Введите имя абонента: ");
        var name = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Номер и имя не могут быть пустыми!");
            return;
        }
        
        phonebook.AddAbonent(phone, name);
    }
    
    /// <summary>
    /// Показать список всех абонентов в телефонной книге.
    /// </summary>
    /// <param name="phonebook">Телефонная книга.</param>
    public static void ShowAllAbonents(Phonebook phonebook)
    {
        var abonents = phonebook.GetAllAbonents();
        
        Console.WriteLine("\n=== Телефонная книга ===");
        foreach (var abonent in abonents)
        {
            Console.WriteLine(abonent);
        }
        Console.WriteLine($"Всего: {abonents.Count} абонентов\n");
    }
    
    /// <summary>
    /// Найти абонента по номеру телефона.
    /// </summary>
    /// <param name="phonebook">Телефонная книга.</param>
    public static void FindByPhone(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона: ");
        var phone = Console.ReadLine();
        
        var abonent = phonebook.GetAbonentByPhone(phone);
        if (abonent != null)
        {
            Console.WriteLine($"Найден: {abonent}");
        }
        else
        {
            Console.WriteLine($"Абонент с номером {phone} не найден.");
        }
    }
    
    /// <summary>
    /// Найти номер телефона по имени.
    /// </summary>
    /// <param name="phonebook">Телефонная книга.</param>
    public static void FindByName(Phonebook phonebook)
    {
        Console.Write("Введите имя абонента: ");
        var name = Console.ReadLine();
        
        var abonent = phonebook.GetPhoneByName(name);
        Console.WriteLine(abonent != null ? $"Найден абонент {abonent}" : $"Абонент с именем {name} не найден.");
    }
    
    /// <summary>
    /// Изменить данные абонента телефонной книги.
    /// </summary>
    /// <param name="phonebook">Телефонная книга.</param>
    public static void UpdateAbonent(Phonebook phonebook)
    {
        Console.Write("Введите текущий номер телефона абонента для редактирования: ");
        var oldPhone = Console.ReadLine();
        
        var existing = phonebook.GetAbonentByPhone(oldPhone);
        if (existing == null)
        {
            Console.WriteLine("Абонент не найден.");
            return;
        }
        
        Console.WriteLine($"Текущие данные: {existing}");
        Console.Write("Введите новый номер телефона (Enter - оставить без изменений): ");
        var newPhone = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newPhone))
            newPhone = existing.PhoneNumber;
            
        Console.Write("Введите новое имя (Enter - оставить без изменений): ");
        var newName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newName))
            newName = existing.Name;
            
        phonebook.UpdateAbonent(existing, newPhone, newName);
    }
    
    /// <summary>
    /// Удалить абонента из телефонной книги.
    /// </summary>
    /// <param name="phonebook">Телефонная книга.</param>
    public static void DeleteAbonent(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона абонента для удаления: ");
        var phone = Console.ReadLine();
        
        var abonent = phonebook.GetAbonentByPhone(phone);
        if (abonent != null)
        {
            Console.Write($"Вы уверены, что хотите удалить {abonent.Name}? (y/n): ");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() == "y")
            {
                phonebook.DeleteAbonent(abonent);
            }
            else
            {
                Console.WriteLine("Удаление отменено.");
            }
        }
        else
        {
            Console.WriteLine("Абонент не найден.");
        }
    }
}