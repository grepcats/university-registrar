using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.Models;

namespace University.Controllers
{
  public class CoursesController : Controller
  {
    [HttpGet("/courses")]
    public ActionResult Index()
    {
      List<Course> allCourses = Course.GetAll();
      return View("Index", allCourses);
    }

    [HttpGet("/courses/new")]
    public ActionResult CreateCourseForm()
    {
      return View("CreateCourseForm");
    }

    [HttpPost("/courses")]
    public ActionResult CreateCourse()
    {
      Course newCourse = new Course(Request.Form["course-name"], Request.Form["course-number"]);
      newCourse.Save();
      return RedirectToAction("Index", "courses");
    }
  }
}
