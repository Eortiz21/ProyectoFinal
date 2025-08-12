using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoyectoParqueo.Models;
using ProyectoParqueo.Models;

namespace ProyectoParqueo.Controllers
{
    public class PagoController : Controller
    {
        private readonly ERPDbContext _context;

        public PagoController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pagos = _context.Pagos.Include(p => p.Ticket);
            return View(await pagos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _context.Pagos
                .Include(p => p.Ticket)
                .FirstOrDefaultAsync(m => m.Id_Pago == id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        public IActionResult Create()
        {
            ViewData["Id_Ticket"] = new SelectList(_context.Tickets, "Id_Ticket", "Id_Ticket");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pago pago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Ticket"] = new SelectList(_context.Tickets, "Id_Ticket", "Id_Ticket", pago.Id_Ticket);
            return View(pago);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();

            ViewData["Id_Ticket"] = new SelectList(_context.Tickets, "Id_Ticket", "Id_Ticket", pago.Id_Ticket);
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pago pago)
        {
            if (id != pago.Id_Pago) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Pagos.Any(e => e.Id_Pago == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Ticket"] = new SelectList(_context.Tickets, "Id_Ticket", "Id_Ticket", pago.Id_Ticket);
            return View(pago);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _context.Pagos
                .Include(p => p.Ticket)
                .FirstOrDefaultAsync(m => m.Id_Pago == id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
