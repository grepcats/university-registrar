using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using University.Models;
using System;

namespace University.Tests
{
  [TestClass]
  public class CourseTest : IDisposable
  {
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=registrar_test;";
    }

    [TestMethod]
    public void GetCourseInfo_GetAllCourseInfo_String()
    {
      //arrange
      Course newCourse = new Course("Calculus III","MATH252");
      string testCourseName = "Calculus III";
      string testCourseNumber = "MATH252";
      int testId = 0;

      //act
      string courseNameResult = newCourse.GetCourseName();
      string courseNumberResult = newCourse.GetCourseNumber();
      int idResult = newCourse.GetCourseId();

      //assert
      Assert.AreEqual(courseNameResult,testCourseName);
      Assert.AreEqual(courseNumberResult,testCourseNumber);
      Assert.AreEqual(idResult,testId);
    }

    [TestMethod]
    public void GetAll_DatebaseEmptyAtFirst_0()
    {
      //arrange, act
      int result = Course.GetAll().Count;

      //assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_CoursesList()
    {
      //arrange
      Course testCourse = new Course("Calculus III", "MATH252");

      //act
      testCourse.Save();
      List<Course> result = Course.GetAll();
      List<Course> testList = new List<Course>{testCourse};

      //assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignedIdToObject_Id()
    {
      //arrange
      Course testCourse = new Course("Calculus III", "MATH252");

      //act
      testCourse.Save();
      Course savedCourse = Course.GetAll()[0];
      int result = savedCourse.GetCourseId();
      int testId = testCourse.GetCourseId();

      //assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAllSame_Course()
    {
      //arrange, act
      Course firstCourse = new Course("Calculus III", "MATH252");
      Course secondCourse = new Course("Calculus III", "MATH252");

      //assert
      Assert.AreEqual(firstCourse, secondCourse);
    }
  }
}
