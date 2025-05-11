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
    public class HistorialMedicoesController : Controller
    {
        private readonly ERPDbContext _context;

        public HistorialMedicoesController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: HistorialMedicoes
        public async Task<IActionResult> Index()
        {
            var eRPDbContext = _context.HistorialesMedicos.Include(h => h.Mascota);
            return View(await eRPDbContext.ToListAsync());
        }

        // GET: HistorialMedicoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMedico = await _context.HistorialesMedicos
                .Include(h => h.Mascota)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialMedico == null)
            {
                return NotFound();
            }

            return View(historialMedico);
        }

        // GET: HistorialMedicoes/Create
        public IActionResult Create()
        {
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "IdMascota", "Especie");
            return View();
        }

        // POST: HistorialMedicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MascotaId,Fecha,Descripcion,Tratamiento")] HistorialMedico historialMedico)
        {
            if (ModelState.IsValid)
            {
                historialMedico.Id = Guid.NewGuid();
                _context.Add(historialMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "IdMascota", "Especie", historialMedico.MascotaId);
            return View(historialMedico);
        }

        // GET: HistorialMedicoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMedico = await _context.HistorialesMedicos.FindAsync(id);
            if (historialMedico == null)
            {
                return NotFound();
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "IdMascota", "Especie", historialMedico.MascotaId);
            return View(historialMedico);
        }

        // POST: HistorialMedicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MascotaId,Fecha,Descripcion,Tratamiento")] HistorialMedico historialMedico)
        {
            if (id != historialMedico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialMedicoExists(historialMedico.Id))
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
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "IdMascota", "Especie", historialMedico.MascotaId);
            return View(historialMedico);
        }

        // GET: HistorialMedicoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMedico = await _context.HistorialesMedicos
                .Include(h => h.Mascota)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialMedico == null)
            {
                return NotFound();
            }

            return View(historialMedico);
        }

        // POST: HistorialMedicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var historialMedico = await _context.HistorialesMedicos.FindAsync(id);
            if (historialMedico != null)
            {
                _context.HistorialesMedicos.Remove(historialMedico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialMedicoExists(Guid id)
        {
            return _context.HistorialesMedicos.Any(e => e.Id == id);
        }
    }
}
