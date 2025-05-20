using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using laboratorio1ElvisOrtiz160625.Models;
using Microsoft.AspNetCore.Authorization;

namespace laboratorio1ElvisOrtiz160625.Controllers
{
    [Authorize]
    public class ProveedorsController : Controller
    {
        private readonly ERPDbContext _context;

        public ProveedorsController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: Proveedors
        public async Task<IActionResult> Index(string buscar)
        {
            var proveedores = _context.tblProveedors.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                proveedores = proveedores.Where(p =>
                    p.Nombre.Contains(buscar) ||
                    p.Telefono.Contains(buscar) ||
                    p.Direccion.Contains(buscar) ||
                    p.Correo.Contains(buscar));
            }

            ViewData["Busqueda"] = buscar;

            return View(await proveedores.ToListAsync());
        }

        // GET: Proveedors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _context.tblProveedors
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        // GET: Proveedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProveedor,Nombre,Telefono,Direccion,Correo")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                proveedor.IdProveedor = Guid.NewGuid();
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _context.tblProveedors.FindAsync(id);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProveedor,Nombre,Telefono,Direccion,Correo")] Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.IdProveedor)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _context.tblProveedors
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var proveedor = await _context.tblProveedors.FindAsync(id);
            if (proveedor != null)
            {
                _context.tblProveedors.Remove(proveedor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(Guid id)
        {
            return _context.tblProveedors.Any(e => e.IdProveedor == id);
        }
    }
}
