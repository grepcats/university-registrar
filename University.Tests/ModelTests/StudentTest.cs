using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using University.Models;
using System;

namespace University.Tests
{
  [TestClass]
  public class StudentTest : IDisposable
  {
    public void Dispose()
    {
      Student.DeleteAll();
    }
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=registrar_test;";
    }
    [TestMethod]
    public void GetFirstName_FetchFirstName_String()
    {
      //arrange
      Student newStudent = new Student("Frank","Ngo","September 1st 2011");
      string testFirstName = "Frank";
      string testLastName = "Ngo";
      string testRawDate = "September 1st 2011";
      int testId = 0;

      //act
      string firstNameResult = newStudent.GetFirstName();
      string lastNameResult = newStudent.GetLastName();
      string rawDateResult = newStudent.GetDate();
      int idResult = newStudent.GetId();

      //assert
      Assert.AreEqual(firstNameResult,testFirstName);
      Assert.AreEqual(lastNameResult,testLastName);
      Assert.AreEqual(rawDateResult,testRawDate);
      Assert.AreEqual(idResult,testId);
    }

    [TestMethod]
    public void GetAll_DatebaseEmptyAtFirst_0()
    {
      //arrange, act
      int result = Student.GetAll().Count;

      //assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_StudentList()
    {
      //arrange
      Student testStudent = new Student("Kayla", "Ondracek", "cat");

      //act
      testStudent.Save();
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      //assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignedIdToObject_Id()
    {
      //arrange
      Student testStudent = new Student("Kayla", "Ondracek", "2008-03-02");

      //act
      testStudent.Save();
      Student savedStudent = Student.GetAll()[0];
      int result = savedStudent.GetId();
      int testId = testStudent.GetId();

      //assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAllSame_Student()
    {
      //arrange, act
      Student firstStudent = new Student("Kayla", "Ondracek", "2008-01-01");
      Student secondStudent = new Student("Kayla", "Ondracek", "2008-01-01");

      //assert
      Assert.AreEqual(firstStudent, secondStudent);
    }


  }
}
