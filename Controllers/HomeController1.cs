using Microsoft.AspNetCore.Mvc;

namespace PoyectoParqueo.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
