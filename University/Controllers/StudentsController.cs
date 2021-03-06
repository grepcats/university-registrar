using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.Models;

namespace University.Controllers
{
  public class StudentsController : Controller
  {
    [HttpGet("/students")]
    public ActionResult Index()
    {
      List<Student> allStudents = Student.GetAll();
      return View("Index", allStudents);
    }

    [HttpGet("/students/new")]
    public ActionResult CreateStudentForm()
    {
      return View("CreateStudentForm");
    }

    [HttpPost("/students")]
    public ActionResult CreateStudent()
    {
      Student newStudent = new Student(Request.Form["first-name"],Request.Form["last-name"],Request.Form["date"]);
      newStudent.Save();

      return RedirectToAction("Index", "students");
    }

    [HttpGet("/students/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Student foundStudent = Student.Find(id);
      foundStudent.Delete();
      return RedirectToAction("Index","students");
    }
  }
}
