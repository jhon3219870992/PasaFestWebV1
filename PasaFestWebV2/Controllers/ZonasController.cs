using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PasaFestWebV2.Models;

namespace PasaFestWebV2.Controllers
{
    public class ZonasController : Controller
    {
        private readonly PasaFestDbContext _context;

        public ZonasController(PasaFestDbContext context)
        {
            _context = context;
        }

        // GET: Zonas
        public async Task<IActionResult> Index()
        {
            var pasaFestDbContext = _context.Zonas.Include(z => z.IdConciertoNavigation);
            return View(await pasaFestDbContext.ToListAsync());
        }

        // GET: Zonas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zonas
                .Include(z => z.IdConciertoNavigation)
                .FirstOrDefaultAsync(m => m.IdZona == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }

        // GET: Zonas/Create
        public IActionResult Create()
        {
            ViewData["IdConcierto"] = new SelectList(_context.Conciertos, "IdConcierto", "IdConcierto");
            return View();
        }

        // POST: Zonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZona,Nombre,Capacidad,EspaciosOcupados,Precio,IdConcierto")] Zona zona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConcierto"] = new SelectList(_context.Conciertos, "IdConcierto", "IdConcierto", zona.IdConcierto);
            return View(zona);
        }

        // GET: Zonas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zonas.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }
            ViewData["IdConcierto"] = new SelectList(_context.Conciertos, "IdConcierto", "IdConcierto", zona.IdConcierto);
            return View(zona);
        }

        // POST: Zonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZona,Nombre,Capacidad,EspaciosOcupados,Precio,IdConcierto")] Zona zona)
        {
            if (id != zona.IdZona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonaExists(zona.IdZona))
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
            ViewData["IdConcierto"] = new SelectList(_context.Conciertos, "IdConcierto", "IdConcierto", zona.IdConcierto);
            return View(zona);
        }

        // GET: Zonas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zonas
                .Include(z => z.IdConciertoNavigation)
                .FirstOrDefaultAsync(m => m.IdZona == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }

        // POST: Zonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zona = await _context.Zonas.FindAsync(id);
            if (zona != null)
            {
                _context.Zonas.Remove(zona);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZonaExists(int id)
        {
            return _context.Zonas.Any(e => e.IdZona == id);
        }
    }
}
