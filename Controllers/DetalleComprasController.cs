using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using laboratorio1ElvisOrtiz160625.Models;
using Microsoft.AspNetCore.Authorization;

namespace laboratorio1ElvisOrtiz160625.Controllers
{
    [Authorize]
    public class DetalleComprasController : Controller
    {
        private readonly ERPDbContext _context;

        public DetalleComprasController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        public async Task<IActionResult> Index()
        {
            var eRPDbContext = _context.DetalleCompras.Include(d => d.Compra).Include(d => d.Producto);
            return View(await eRPDbContext.ToListAsync());
        }

        // GET: DetalleCompras/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.Compra)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // GET: DetalleCompras/Create
        public IActionResult Create()
        {
            ViewData["CompraId"] = new SelectList(_context.Compras, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.tblProductos, "IdProducto", "Categoria");
            return View();
        }

        // POST: DetalleCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompraId,ProductoId,Cantidad,PrecioUnitario,Subtotal")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                detalleCompra.Id = Guid.NewGuid();
                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompraId"] = new SelectList(_context.Compras, "Id", "Id", detalleCompra.CompraId);
            ViewData["ProductoId"] = new SelectList(_context.tblProductos, "IdProducto", "Categoria", detalleCompra.ProductoId);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }
            ViewData["CompraId"] = new SelectList(_context.Compras, "Id", "Id", detalleCompra.CompraId);
            ViewData["ProductoId"] = new SelectList(_context.tblProductos, "IdProducto", "Categoria", detalleCompra.ProductoId);
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CompraId,ProductoId,Cantidad,PrecioUnitario,Subtotal")] DetalleCompra detalleCompra)
        {
            if (id != detalleCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleCompraExists(detalleCompra.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompraId"] = new SelectList(_context.Compras, "Id", "Id", detalleCompra.CompraId);
            ViewData["ProductoId"] = new SelectList(_context.tblProductos, "IdProducto", "Categoria", detalleCompra.ProductoId);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.Compra)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra != null)
            {
                _context.DetalleCompras.Remove(detalleCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCompraExists(Guid id)
        {
            return _context.DetalleCompras.Any(e => e.Id == id);
        }
    }
}
