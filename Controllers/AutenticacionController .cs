using DulceSaborOnline___WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DulceSaborOnline___WEB.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly DScontext _context;

        public AutenticacionController(DScontext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public ActionResult login(string correo, string contrasena)
        {
            var usuario = _context.usuarios.FirstOrDefault(u => u.nombre_usuario == correo && u.contrasena == contrasena);

            if (usuario != null)
            {
                // Guardar la información del usuario en una cookie
                Response.Cookies.Append("UsuarioCookie", $"{usuario.id_usuario}|{usuario.nombre}");

                // Redirigir a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticación fallida
                // Manejar el caso de autenticación fallida (por ejemplo, mostrando un mensaje de error)
                ViewData["Error"] = "Credenciales incorrectas.";
                return View("login");
            }
        }
    }
}
