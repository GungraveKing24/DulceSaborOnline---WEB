using Microsoft.AspNetCore.Mvc;

namespace DulceSaborOnline___WEB.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutenticacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Login(string nombreUsuario, string contrasena)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.nombre_usuario == nombreUsuario && u.contrasena == contrasena);

            if (usuario != null)
            {
                // Usuario autenticado correctamente
                // Aquí puedes guardar la información del usuario en una cookie como se mostró anteriormente
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticación fallida
                // Maneja el caso de autenticación fallida (por ejemplo, mostrando un mensaje de error)
                return View("Login");
            }
        }
    }
}
