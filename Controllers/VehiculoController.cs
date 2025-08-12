using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoParqueo.Models;

namespace ProyectoParqueo.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly ERPDbContext _context;

        public VehiculoController(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vehiculos = _context.Vehiculos.Include(v => v.Cliente);
            return View(await vehiculos.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            var vehiculo = await _context.Vehiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.NoPlaca == id);
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        public IActionResult Create()
        {
            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Nombres");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Nombres", vehiculo.Id_Cliente);
            return View(vehiculo);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null) return NotFound();
            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Nombres", vehiculo.Id_Cliente);
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Vehiculo vehiculo)
        {
            if (id != vehiculo.NoPlaca) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Vehiculos.Any(e => e.NoPlaca == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Cliente"] = new SelectList(_context.Clientes, "Id_Cliente", "Nombres", vehiculo.Id_Cliente);
            return View(vehiculo);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var vehiculo = await _context.Vehiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.NoPlaca == id);
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
