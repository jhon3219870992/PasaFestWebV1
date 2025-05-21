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
    public class EntradumsController : Controller
    {
        private readonly PasaFestDbContext _context;

        public EntradumsController(PasaFestDbContext context)
        {
            _context = context;
        }

        // GET: Entradums
        public async Task<IActionResult> Index()
        {
            var pasaFestDbContext = _context.Entrada.Include(e => e.IdCompraNavigation).Include(e => e.IdZonaNavigation);
            return View(await pasaFestDbContext.ToListAsync());
        }

        // GET: Entradums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradum = await _context.Entrada
                .Include(e => e.IdCompraNavigation)
                .Include(e => e.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdEntrada == id);
            if (entradum == null)
            {
                return NotFound();
            }

            return View(entradum);
        }

        // GET: Entradums/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona");
            return View();
        }

        // POST: Entradums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEntrada,Estado,FechaCompra,TipoEntrada,IdCompra,IdZona")] Entradum entradum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entradum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", entradum.IdCompra);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", entradum.IdZona);
            return View(entradum);
        }

        // GET: Entradums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradum = await _context.Entrada.FindAsync(id);
            if (entradum == null)
            {
                return NotFound();
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", entradum.IdCompra);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", entradum.IdZona);
            return View(entradum);
        }

        // POST: Entradums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEntrada,Estado,FechaCompra,TipoEntrada,IdCompra,IdZona")] Entradum entradum)
        {
            if (id != entradum.IdEntrada)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entradum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradumExists(entradum.IdEntrada))
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
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", entradum.IdCompra);
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", entradum.IdZona);
            return View(entradum);
        }

        // GET: Entradums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradum = await _context.Entrada
                .Include(e => e.IdCompraNavigation)
                .Include(e => e.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdEntrada == id);
            if (entradum == null)
            {
                return NotFound();
            }

            return View(entradum);
        }

        // POST: Entradums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entradum = await _context.Entrada.FindAsync(id);
            if (entradum != null)
            {
                _context.Entrada.Remove(entradum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradumExists(int id)
        {
            return _context.Entrada.Any(e => e.IdEntrada == id);
        }
    }
}
