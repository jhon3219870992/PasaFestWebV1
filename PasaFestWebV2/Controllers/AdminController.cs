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

}

