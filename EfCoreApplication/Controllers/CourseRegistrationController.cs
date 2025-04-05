using Microsoft.AspNetCore.Mvc;

namespace EfCoreApplication.Controllers
{
    public class CourseRegistration : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
