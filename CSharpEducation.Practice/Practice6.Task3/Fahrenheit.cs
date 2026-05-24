namespace Practice6.Task3;

public class Fahrenheit
{
  public double degrees { get; set; }
  
  public Fahrenheit(double degrees)
  {
    this.degrees = degrees;
  }
  /*
  public static implicit operator Celsius(Fahrenheit f)
  {
    return new Celsius((f.degrees * 9 / 5) + 32);
  }
  
  public static explicit operator Fahrenheit (Celsius c)
  {
    return new Fahrenheit((c.degrees - 32) * 5 / 9);
  }
  */
}