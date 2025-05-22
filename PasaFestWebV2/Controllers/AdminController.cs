using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasaFestWebV2.Models;

public class AdminController : Controller
{
    private readonly PasaFestDbContext _context;

    public AdminController(PasaFestDbContext context)
    {
        _context = context;
    }

    public IActionResult Panel()
    {
        if (TempData["AdminNombre"] == null)
            return RedirectToAction("Login", "Auth");

        ViewBag.NombreAdmin = TempData["AdminNombre"];
        return View();
    }

    public IActionResult Conciertos()
    {
        var conciertos = _context.Conciertos.ToList();
        return View(conciertos);
    }
    public IActionResult Usuarios()
    {
        var usuariosConEntradas = _context.Usuarios
            .Include(u => u.Compras)
                .ThenInclude(c => c.Entrada)
                    .ThenInclude(e => e.IdZonaNavigation)
            .ToList();

        return View(usuariosConEntradas);
    }
    // Acción para mostrar las zonas con colores
    public IActionResult Zonas(int? idConcierto)
    {
        // Obtener conciertos para dropdown o filtro
        var conciertos = _context.Conciertos.OrderBy(c => c.Fecha).ToList();
        ViewBag.Conciertos = conciertos;

        // Si no hay concierto seleccionado, tomar el primero
        int conciertoId = idConcierto ?? conciertos.FirstOrDefault()?.IdConcierto ?? 0;

        // Obtener zonas y entradas para el concierto seleccionado
        var zonas = _context.Zonas
            .Include(z => z.Entrada)
            .Where(z => z.IdConcierto == conciertoId)
            .ToList();

        return View(zonas);
    }

}

