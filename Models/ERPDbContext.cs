using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoParqueo.Models
{
    public class ERPDbContext : IdentityDbContext<IdentityUser>
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options)
            : base(options)
        {
        }
        public class DashboardController : Controller
        {
            public IActionResult Index()
            {
                return View(); // Esto busca Views/Dashboard/Index.cshtml
            }
        }

    }
}
