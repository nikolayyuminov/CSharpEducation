namespace Practice5.Task2;

public interface ILogger
{
  public string Trace(string  message);
  public string Info(string  message);
  public string Debug(string message);
  public string Warning(string  message);
  public string Error(string  message); 
  public string Fatal(string  message);
  public string Log(string message, LogLevel logLevel);
}