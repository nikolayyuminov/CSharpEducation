using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Practice4.MathHelper;

namespace Practice7.Task1;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task1");
    int a;
    int b;
    do
    {
      Console.WriteLine("Введите 2 числа через пробел:");
      string input = Console.ReadLine();
      var parts = input.Split(' ');

      try
      {
        a = int.Parse(parts[0]);
        b = int.Parse(parts[1]);
        Console.WriteLine(MathHelper.Division(a, b));
        break;
      }
      catch (DivideByZeroException e)
      {
        Console.WriteLine(e);

      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Попробуйте ввести число");

      }
    } while (true);

    var isExit = true;
    do
    {
      Console.WriteLine("Загрузка чисел из файла. Введите имя файла:");
      try
      {
        var path = Console.ReadLine();
        if (path == "exit")
        {
          isExit = false;
        }
        if (string.IsNullOrEmpty(path))
        {
          throw new NullReferenceException("Не указано имя файла!");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
          throw new NullReferenceException("В имени файла только пробелы");
        }
        var numbers = LoadListOfNumbers(path);
        Console.WriteLine(MathHelper.Division(numbers[0], numbers[1]));
        
      }
      catch (FormatException e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Попробуйте указать другой файл");
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Попробуйте указать имя файла");
      }
      catch (ArgumentNullException e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Попробуйте указать другой файл");
      }
      catch (DivideByZeroException e)
      {
        Console.WriteLine(e);

      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        Console.WriteLine("Что-то пошло не так...");
      }

    } while (isExit);
  }

  public static List<int> LoadListOfNumbers(string file)
  {
    if (!File.Exists(file))
    {
      throw new ArgumentNullException($"Файла {file} не существует");
    }
    
    List<int> result = new List<int>();
    
    Console.WriteLine($"Loading from {file}");
    var lines = new List<string>(File.ReadAllLines(file));
    if (lines.Count == 0)
    {
      throw new ArgumentNullException($"Файл {file} пуст!");
    }
    var parts = lines[0].Split(' ');
    foreach (var line in parts)
    {
      var isParse = int.TryParse(line, out var number);
      if (!isParse)
      {
        throw new FormatException("В файле не числа!");
      }
      result.Add(number);
    }
    return result;
  }
}