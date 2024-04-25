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
                // Verificar si hay un pedido pendiente para el usuario
                var pedidoPendiente = _context.pedidos.FirstOrDefault(p => p.id_usuario == usuario.id_usuario && p.Estado == "Pendiente");

                if (pedidoPendiente == null)
                {
                    // Crear un nuevo pedido pendiente si no existe
                    var nuevoPedido = new Pedidos
                    {
                        id_usuario = usuario.id_usuario,
                        Estado = "Pendiente",
                        Total = 0,
                        fecha_hora = DateTime.Today,
                        com_prom = 0,
                        direccion_id = 0
                    };

                    _context.pedidos.Add(nuevoPedido);
                    _context.SaveChanges();
                }

                // Guardar la información del usuario en una cookie solo si el inicio de sesión es exitoso
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
