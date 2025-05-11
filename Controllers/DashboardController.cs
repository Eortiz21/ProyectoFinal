using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using laboratorio1ElvisOrtiz160625.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace laboratorio1ElvisOrtiz160625.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ERPDbContext _context;

        public DashboardController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.tblProductos
                                          .Include(p => p.Proveedor)
                                          .ToListAsync();
            var proveedores = await _context.tblProveedors.ToListAsync();

            var fechaActual = DateTime.Now;
            var fechaProxima = fechaActual.AddDays(15);
            var fechaInicioSemana = fechaActual.AddDays(-7);

            var viewModel = new DashboardViewModel
            {
                TotalProductos = productos.Count,
                TotalProveedores = proveedores.Count,
                StockMaximo = productos.Max(p => p.Stock),

                ProductosConMasExistencia = productos.OrderByDescending(p => p.Stock).Take(5).ToList(),
                ProductosBajoStock = productos.Where(p => p.Stock < 20).ToList(),
                ProductosAltoStock = productos.Where(p => p.Stock > 100).ToList(),
                ProductosExpirados = productos.Where(p => p.FechaVencimiento < fechaActual).ToList(),
                ProductosPorExpirar = productos.Where(p => p.FechaVencimiento >= fechaActual && p.FechaVencimiento <= fechaProxima).ToList(),
                ProductosRecientes = productos.Where(p => p.FechaRegistro >= fechaInicioSemana).ToList()
            };

            return View(viewModel);
        }
    }
}
