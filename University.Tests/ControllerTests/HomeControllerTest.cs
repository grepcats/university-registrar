using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.Models;
using University.Controllers;

namespace University.Tests
{
  [TestClass]
  public class HomeControllerTest
  {
    [TestMethod]
    public void Index_ReturnIfTrue_View()
    {
      //arrange
      HomeController controller = new HomeController();

      //act
      IActionResult indexView = controller.Index();
      ViewResult result = indexView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
  }
}
