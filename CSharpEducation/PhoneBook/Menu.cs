using System;

namespace PhoneBook;

/// <summary>
/// Меню пользователя, для работы с телефонной книгой
/// </summary>
public static class Menu
{
  /// <summary>
  /// Отобразить меню для работы с телефонной книгой.
  /// </summary>
  public static void ShowMenu()
  {
    Console.WriteLine("\n=== ТЕЛЕФОННАЯ КНИГА ===");
    Console.WriteLine("1. Добавить абонента");
    Console.WriteLine("2. Показать всех абонентов");
    Console.WriteLine("3. Найти абонента по номеру телефона");
    Console.WriteLine("4. Найти номер телефона по имени");
    Console.WriteLine("5. Редактировать абонента");
    Console.WriteLine("6. Удалить абонента");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");
  }

  /// <summary>
  /// Выполнить действие, выбранное пользователем.
  /// </summary>
  /// <param name="choice">Выбор пользователя.</param>
  /// <param name="phonebook">Телефонная книга.</param>
  public static void SwitchChoice(string? choice, Phonebook phonebook)
  {
    switch (choice)
    {
      case "1":
        ConsoleRequestManager.AddAbonent(phonebook);
        break;
      case "2":
        ConsoleRequestManager.ShowAllAbonents(phonebook);
        break;
      case "3":
        ConsoleRequestManager.FindByPhone(phonebook);
        break;
      case "4":
        ConsoleRequestManager.FindByName(phonebook);
        break;
      case "5":
        ConsoleRequestManager.UpdateAbonent(phonebook);
        break;
      case "6":
        ConsoleRequestManager.DeleteAbonent(phonebook);
        break;
      case "0":
        Console.WriteLine("До свидания!");
        break;
      default:
        Console.WriteLine("Неверный выбор. Попробуйте снова.");
        break;
    }
  }
}