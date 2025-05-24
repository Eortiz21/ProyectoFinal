using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using laboratorio1ElvisOrtiz160625.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace laboratorio1ElvisOrtiz160625.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private readonly ERPDbContext _context;
        private readonly IConfiguration _configuration;

        public CitasController(ERPDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Método para obtener veterinarios desde la base sin entidad modelo
        private List<SelectListItem> GetVeterinarios()
        {
            var lista = new List<SelectListItem>();

            string connStr = _configuration.GetConnectionString("SomeeConexion");
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT IdVeterinario, Nombre FROM Veterinarios", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new SelectListItem
                            {
                                Value = reader["IdVeterinario"].ToString(),
                                Text = reader["Nombre"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var eRPDbContext = _context.Citas.Include(c => c.Mascota);
            return View(await eRPDbContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Citas
                .Include(c => c.Mascota)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Especie");
            ViewData["IdVeterinario"] = GetVeterinarios();
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,FechaHora,Motivo,Estado,IdMascota,IdVeterinario")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.IdCita = Guid.NewGuid();
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Especie", cita.IdMascota);
            ViewData["IdVeterinario"] = GetVeterinarios();
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Especie", cita.IdMascota);
            ViewData["IdVeterinario"] = GetVeterinarios();
            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdCita,FechaHora,Motivo,Estado,IdMascota,IdVeterinario")] Cita cita)
        {
            if (id != cita.IdCita) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Especie", cita.IdMascota);
            ViewData["IdVeterinario"] = GetVeterinarios();
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Citas
                .Include(c => c.Mascota)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(Guid id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
