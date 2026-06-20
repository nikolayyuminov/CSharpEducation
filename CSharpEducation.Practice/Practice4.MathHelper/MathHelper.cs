using System;

namespace Practice4.MathHelper;

public class MathHelper
{
  public static int Addition(int a, int b) => a + b;
        
  public static int Multiplication(int a, int b) => a * b;

  public static int Division(int a, int b)
  {
    if (b == 0)
    {
      throw new DivideByZeroException("Произошло деление на ноль! попробуйте изменить делитель");
    }
    return a / b;
  }
  
  public static int Subtraction (int a, int b) => a - b;
}