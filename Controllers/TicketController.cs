using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoyectoParqueo.Models;
using ProyectoParqueo.Models;

namespace ProyectoParqueo.Controllers
{
    public class TicketController : Controller
    {
        private readonly ERPDbContext _context;

        public TicketController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = _context.Tickets
                .Include(t => t.Vehiculo)
                .Include(t => t.EspacioEstacionamiento)
                .Include(t => t.Tarifa);
            return View(await tickets.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ticket = await _context.Tickets
                .Include(t => t.Vehiculo)
                .Include(t => t.EspacioEstacionamiento)
                .Include(t => t.Tarifa)
                .FirstOrDefaultAsync(m => m.Id_Ticket == id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        public IActionResult Create()
        {
            ViewData["NoPlaca"] = new SelectList(_context.Vehiculos, "NoPlaca", "NoPlaca");
            ViewData["Id_Espacio"] = new SelectList(_context.EspaciosEstacionamiento, "Id_Espacio", "No_Espacio");
            ViewData["Id_Tarifa"] = new SelectList(_context.Tarifas, "Id_Tarifa", "TipoTarifa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoPlaca"] = new SelectList(_context.Vehiculos, "NoPlaca", "NoPlaca", ticket.NoPlaca);
            ViewData["Id_Espacio"] = new SelectList(_context.EspaciosEstacionamiento, "Id_Espacio", "No_Espacio", ticket.Id_Espacio);
            ViewData["Id_Tarifa"] = new SelectList(_context.Tarifas, "Id_Tarifa", "TipoTarifa", ticket.Id_Tarifa);
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            ViewData["NoPlaca"] = new SelectList(_context.Vehiculos, "NoPlaca", "NoPlaca", ticket.NoPlaca);
            ViewData["Id_Espacio"] = new SelectList(_context.EspaciosEstacionamiento, "Id_Espacio", "No_Espacio", ticket.Id_Espacio);
            ViewData["Id_Tarifa"] = new SelectList(_context.Tarifas, "Id_Tarifa", "TipoTarifa", ticket.Id_Tarifa);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id_Ticket) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tickets.Any(e => e.Id_Ticket == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoPlaca"] = new SelectList(_context.Vehiculos, "NoPlaca", "NoPlaca", ticket.NoPlaca);
            ViewData["Id_Espacio"] = new SelectList(_context.EspaciosEstacionamiento, "Id_Espacio", "No_Espacio", ticket.Id_Espacio);
            ViewData["Id_Tarifa"] = new SelectList(_context.Tarifas, "Id_Tarifa", "TipoTarifa", ticket.Id_Tarifa);
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ticket = await _context.Tickets
                .Include(t => t.Vehiculo)
                .Include(t => t.EspacioEstacionamiento)
                .Include(t => t.Tarifa)
                .FirstOrDefaultAsync(m => m.Id_Ticket == id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
