using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace University.Models
{
  public class Student
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _rawDate;

    public Student (string firstName, string lastName, string date, int Id = 0)
    {
      _firstName = firstName;
      _lastName = lastName;
      _rawDate = date;
      _id = Id;
    }

    public string GetFirstName { return "hello"; }

    public string GetLastName { return "hello"; }

    public string GetDate { return "hello"; }

    public int GetId { return 5; }
  }
}
