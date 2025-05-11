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
    public class MunicipiosController : Controller
    {
        private readonly ERPDbContext _context;

        public MunicipiosController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: Municipios
        public async Task<IActionResult> Index()
        {
            var eRPDbContext = _context.Municipios.Include(m => m.Departamento);
            return View(await eRPDbContext.ToListAsync());
        }

        // GET: Municipios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios
                .Include(m => m.Departamento)
                .FirstOrDefaultAsync(m => m.MunicipioId == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // GET: Municipios/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.tblDepartamento.Where(d => !d.Inactivo), "DepartamentoId", "Nombre");
            return View();
        }

        // POST: Municipios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MunicipioId,Nombre,Codigo,DepartamentoId")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                municipio.MunicipioId = Guid.NewGuid();
                _context.Add(municipio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.tblDepartamento.Where(d => !d.Inactivo), "DepartamentoId", "Nombre", municipio.DepartamentoId);
            return View(municipio);
        }

        // GET: Municipios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.tblDepartamento.Where(d => !d.Inactivo), "DepartamentoId", "Nombre", municipio.DepartamentoId);
            return View(municipio);
        }

        // POST: Municipios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MunicipioId,Nombre,Codigo,DepartamentoId")] Municipio municipio)
        {
            if (id != municipio.MunicipioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipioExists(municipio.MunicipioId))
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
            ViewData["DepartamentoId"] = new SelectList(_context.tblDepartamento.Where(d => !d.Inactivo), "DepartamentoId", "Nombre", municipio.DepartamentoId);
            return View(municipio);
        }

        // GET: Municipios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios
                .Include(m => m.Departamento)
                .FirstOrDefaultAsync(m => m.MunicipioId == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var municipio = await _context.Municipios.FindAsync(id);
            if (municipio != null)
            {
                _context.Municipios.Remove(municipio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MunicipioExists(Guid id)
        {
            return _context.Municipios.Any(e => e.MunicipioId == id);
        }
    }
}
