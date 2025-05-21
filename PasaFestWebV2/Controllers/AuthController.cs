using Microsoft.AspNetCore.Mvc;
using PasaFestWebV2.Models;

namespace PasaFestWebV2.Controllers
{
    public class AuthController : Controller
    {
        private readonly PasaFestDbContext _context;

        public AuthController(PasaFestDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string correo, string contraseña)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == correo && u.Contraseña == contraseña);

            if (usuario != null)
            {
                // ⚠️ Puedes validar si es admin por correo o con un campo específico
                if (usuario.Correo == "admin@pasa.com")
                {
                    TempData["AdminNombre"] = usuario.Nombre;
                    return RedirectToAction("Panel", "Admin");
                }

                TempData["UsuarioNombre"] = usuario.Nombre;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "❌ Correo o contraseña incorrectos.";
            return View();
        }
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(string nombre, string apellidoPat, string apellidoMat, string correo, string contraseña)
        {
            // Validar si ya existe un usuario con ese correo
            if (_context.Usuarios.Any(u => u.Correo == correo))
            {
                ViewBag.Mensaje = "⚠️ Ya existe un usuario con ese correo.";
                return View();
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                ApellidoPat = apellidoPat,
                ApellidoMat = apellidoMat,
                Correo = correo,
                Contraseña = contraseña,
                FechaRegistro = DateTime.Now
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            TempData["UsuarioNombre"] = nuevoUsuario.Nombre;
            return RedirectToAction("Index", "Home");
        }

    }
}
