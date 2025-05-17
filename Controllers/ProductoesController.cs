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
    public class ProductoesController : Controller
    {
        private readonly ERPDbContext _context;

        public ProductoesController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: Productoes
        // GET: Productoes
        public async Task<IActionResult> Index(string buscar)
        {
            var productos = _context.tblProductos.Include(p => p.Proveedor).AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                productos = productos.Where(p => p.Nombre.Contains(buscar));
            }

            ViewData["Busqueda"] = buscar;

            return View(await productos.ToListAsync());
        }


        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.tblProductos
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        // GET: Productoes buscar

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["idProveedor"] = new SelectList(_context.tblProveedors, "IdProveedor", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,Categoria,Precio,Stock,Lote,FechaVencimiento,idProveedor,Estado,FechaRegistro")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.IdProducto = Guid.NewGuid();
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idProveedor"] = new SelectList(_context.tblProveedors, "IdProveedor", "Nombre", producto.idProveedor);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.tblProductos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["idProveedor"] = new SelectList(_context.tblProveedors, "IdProveedor", "Nombre", producto.idProveedor);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProducto,Nombre,Descripcion,Categoria,Precio,Stock,Lote,FechaVencimiento,idProveedor,Estado,FechaRegistro")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["idProveedor"] = new SelectList(_context.tblProveedors, "IdProveedor", "Nombre", producto.idProveedor);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.tblProductos
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var producto = await _context.tblProductos.FindAsync(id);
            if (producto != null)
            {
                _context.tblProductos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(Guid id)
        {
            return _context.tblProductos.Any(e => e.IdProducto == id);
        }
    }
}
