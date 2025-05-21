using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasaFestWebV2.Models; // Asegurate que el namespace coincida

namespace PasaFestWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly PasaFestDbContext _context;

        public HomeController(PasaFestDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var conciertos = _context.Conciertos
                .OrderBy(c => c.Fecha)
                .ToList();

            return View(conciertos);
        }
       
    }


}
