using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.Models;
using University.Controllers;

namespace University.Tests
{
  [TestClass]
  public class StudentsControllerTest
  {
    [TestMethod]
    public void Index_ReturnIfTrue_View()
    {
      //arrange
      StudentsController controller = new StudentsController();

      //act
      IActionResult indexView = controller.Index();
      ViewResult result = indexView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Index_HasCorrectModelType_True()
    {
      //arrange
      ViewResult indexView = new StudentsController().Index() as ViewResult;

      //act
      var result = indexView.ViewData.Model;

      //assert
      Assert.IsInstanceOfType(result, typeof(List<Student>));
    }

    [TestMethod]
    public void CreateStudentForm_ReturnIfTrue_View()
    {
      //arrange
      StudentsController controller = new StudentsController();

      //act
      IActionResult createStudentFormView = controller.CreateStudentForm();
      ViewResult result = createStudentFormView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
  }
}
