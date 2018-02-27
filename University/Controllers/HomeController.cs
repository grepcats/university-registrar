using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.Models;

namespace University.Controllers
{
  public class HomeController : Controller
  {
    [Route("/")]
    public ActionResult Index()
    {
      return View("Index");
    }
  }
}
