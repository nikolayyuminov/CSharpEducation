namespace Practice6.Task9;

public class Person
{
  public string Name { get; set; }
  public int Age { get; set; }
  
  public Person(string name, int age)
  {
    Name = name;
    Age = age;
  }

  public override string ToString()
  {
    return  $"Name: {Name}, Age: {Age}";
  }

  public override int GetHashCode()
  {
    return Name.GetHashCode() + Age.GetHashCode();
  }

  public override bool Equals(object? obj)
  {
    if (obj != null)
    {
      if (obj is Person person)
      {
        return person.Age == Age && person.Name == Name;
      }
    }

    return false;
  }
}