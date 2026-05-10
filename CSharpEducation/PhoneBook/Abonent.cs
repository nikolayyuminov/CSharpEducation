namespace PhoneBook;

public class Abonent
{
  #region Свойства

    public string PhoneNumber { get; set; }
    public string Name { get; set; }

  #endregion

  #region Методы

    public override string ToString()
    {
      return $"{Name}: {PhoneNumber}";
    }

  #endregion

  #region Конструктор

    public Abonent(string name, string phoneNumber)
    {
      PhoneNumber = phoneNumber;
      Name = name;
    }

  #endregion



}