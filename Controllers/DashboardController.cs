using Microsoft.AspNetCore.Mvc;

namespace PoyectoParqueo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Esto busca Views/Dashboard/Index.cshtml
        }
    }
}