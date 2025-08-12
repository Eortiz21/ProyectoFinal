using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoyectoParqueo.Models;

namespace ProyectoParqueo.Models
{
    public class ERPDbContext : IdentityDbContext<IdentityUser>
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<EspacioEstacionamiento> EspaciosEstacionamiento { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public class DashboardController : Controller
        {
            public IActionResult Index()
            {
                return View(); // Esto busca Views/Dashboard/Index.cshtml
            }
        }

    }
}
