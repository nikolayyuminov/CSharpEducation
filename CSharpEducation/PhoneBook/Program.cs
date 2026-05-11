using System;

namespace PhoneBook;

class Program
{
    private static void Main()
    {
        var phonebook = Phonebook.Instance;
        Console.WriteLine("\nНажмите любую клавишу для начала работы...");
        Console.ReadKey();
        bool isRunning = true;

        do
        {
            Console.Clear();
            Menu.ShowMenu();
            var choice = Console.ReadLine();
            Menu.SwitchChoice(choice, phonebook);
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();

            if (choice != "0") continue;
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
            isRunning = false;
        } 
        while (isRunning);
    }
}