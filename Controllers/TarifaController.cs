using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoyectoParqueo.Models;
using ProyectoParqueo.Models;

namespace ProyectoParqueo.Controllers
{
    public class TarifaController : Controller
    {
        private readonly ERPDbContext _context;

        public TarifaController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarifas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var tarifa = await _context.Tarifas.FirstOrDefaultAsync(m => m.Id_Tarifa == id);
            if (tarifa == null) return NotFound();
            return View(tarifa);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarifa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null) return NotFound();
            return View(tarifa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarifa tarifa)
        {
            if (id != tarifa.Id_Tarifa) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tarifas.Any(e => e.Id_Tarifa == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarifa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var tarifa = await _context.Tarifas.FirstOrDefaultAsync(m => m.Id_Tarifa == id);
            if (tarifa == null) return NotFound();
            return View(tarifa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            _context.Tarifas.Remove(tarifa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
