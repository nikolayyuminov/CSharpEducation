using System;
using System.IO;

namespace Practice5.Task2;

public class FileLogger : ILogger
{
  private readonly string _filePath = "log.txt";

  public void SaveInFile(string message, LogLevel level)
  {
    if (!File.Exists(_filePath))
    {
      return;
    }
    File.AppendAllText(_filePath, Log(message, level));
  }
  
  public string Trace(string message)
  {
    return  LogLevel.Trace + message;
  }

  public string Info(string message)
  {
    return LogLevel.Info + message;
  }

  public string Debug(string message)
  {
    return LogLevel.Debug + message;
  }

  public string Warning(string message)
  {
    return LogLevel.Warning + message;
  }

  public string Error(string message)
  {
    return LogLevel.Error + message;
  }

  public string Fatal(string message)
  {
    return LogLevel.Fatal + message;
  }

  public string Log(string message, LogLevel level)
  {
    switch (level)
    {
      case LogLevel.Debug:
        return DateTime.Now + this.ToString() + Debug(message);

      case LogLevel.Trace:
        return DateTime.Now + this.ToString() +  Trace(message);
      
      case LogLevel.Info:
        return DateTime.Now + this.ToString() + Info(message);
      
      case LogLevel.Warning:
        return DateTime.Now + this.ToString() + Warning(message);
      
      case LogLevel.Error:
        return DateTime.Now + this.ToString() + Error(message);
      
      case LogLevel.Fatal:
        return DateTime.Now + this.ToString() + Fatal(message);
      
      default:
        return DateTime.Now + this.ToString() + "noLevel" + message;
    }
  }
}