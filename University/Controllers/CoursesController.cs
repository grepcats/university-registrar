using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.Models;
using System;

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

    [HttpGet("/courses/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Course foundCourse = Course.Find(id);
      List<Student> students = foundCourse.GetStudents();
      List<Student> allStudents = Student.GetAll();
      model.Add("course", foundCourse);
      model.Add("students", students);
      model.Add("allstudents", allStudents);
      return View("Details", model);
    }

    [HttpPost("/courses/{courseId}/students")]
    public ActionResult AddStudent(int courseId)
    {
      Course course = Course.Find(courseId);
      Student foundStudent = Student.Find(Int32.Parse(Request.Form["student-id"]));
      course.AddStudent(foundStudent);
      return RedirectToAction("Details", new {Id = courseId});
    }
  }
}
