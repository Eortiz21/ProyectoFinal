using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoParqueo.Models;

namespace ProyectoParqueo.Controllers
{
    public class EspacioEstacionamientoController : Controller
    {
        private readonly ERPDbContext _context;

        public EspacioEstacionamientoController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.EspaciosEstacionamiento.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var espacio = await _context.EspaciosEstacionamiento.FirstOrDefaultAsync(m => m.Id_Espacio == id);
            if (espacio == null) return NotFound();
            return View(espacio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EspacioEstacionamiento espacio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espacio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(espacio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var espacio = await _context.EspaciosEstacionamiento.FindAsync(id);
            if (espacio == null) return NotFound();
            return View(espacio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EspacioEstacionamiento espacio)
        {
            if (id != espacio.Id_Espacio) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(espacio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.EspaciosEstacionamiento.Any(e => e.Id_Espacio == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(espacio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var espacio = await _context.EspaciosEstacionamiento.FirstOrDefaultAsync(m => m.Id_Espacio == id);
            if (espacio == null) return NotFound();
            return View(espacio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var espacio = await _context.EspaciosEstacionamiento.FindAsync(id);
            _context.EspaciosEstacionamiento.Remove(espacio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
