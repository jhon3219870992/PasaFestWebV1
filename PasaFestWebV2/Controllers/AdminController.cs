using Microsoft.AspNetCore.Mvc;

namespace PasaFestWebV2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Panel()
        {
            // Validamos si hay un nombre en TempData (o más adelante Session)
            if (TempData["AdminNombre"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.NombreAdmin = TempData["AdminNombre"];
            return View();
        }
    }
}
