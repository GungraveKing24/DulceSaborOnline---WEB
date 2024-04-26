using DulceSaborOnline___WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DulceSaborOnline___WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly DScontext _context;

        public HomeController(DScontext context)
        {
            _context = context;
        }

        public IActionResult combos()
        {
            return View("combos");
        }

        public IActionResult Historial()
        {
            return View("Historial");
        }

        public IActionResult Index()
        {
            var itemsMenu = _context.items_menu.ToList();

            var combos = _context.combos.ToList();
            ViewBag.Combos1 = combos.ToList();
            ViewBag.Combos = combos.Skip(1).ToList();

            return View(itemsMenu);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarElemento(int idItemMenu)
        {
            // Aquí puedes obtener el ID de usuario desde la cookie o desde el contexto de usuario actual
            var idUsuario = ObtenerIdUsuarioDesdeCookie(); // Función que debes implementar

            // Verificar si existe un pedido pendiente para el usuario actual
            var detallepedidoPendiente = _context.pedidos.FirstOrDefault(p => p.id_usuario == idUsuario && p.Estado == "Pendiente");
            var idPedido = ObtenerIdPedidoPendiente(idUsuario);


            if (detallepedidoPendiente == null)
            {
                // Crear un nuevo pedido pendiente si no existe
                var nuevoPedido = new Pedidos
                {
                    id_usuario = idUsuario,
                    Estado = "Pendiente",
                    Total = 0,
                    fecha_hora = DateTime.Today,
                    com_prom = 0,
                    direccion_id = 0
                };

                _context.pedidos.Add(nuevoPedido);
                _context.SaveChanges();
            }
            if (detallepedidoPendiente != null)
            {
                // Si no hay un pedido pendiente, crear uno nuevo
                var detallepedidoPendienteNuevo = new detalles_pedidos
                {
                   id_pedido = idPedido,
                   id_comida = idItemMenu,
                   Tipo_Plato = "N",
                   comentario = "Coments"
                };

                _context.detalles_Pedidos.Add(detallepedidoPendienteNuevo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }


        public int ObtenerIdPedidoPendiente(int idUsuario)
        {
            var idPedidoPendiente = _context.pedidos
                .Where(p => p.id_usuario == idUsuario && p.Estado == "Pendiente")
                .Select(p => p.Id_Pedido)
                .FirstOrDefault();

            // Puedes manejar el caso en el que no se encuentre ningún pedido pendiente para el usuario aquí
            return idPedidoPendiente; // Devolver el ID del pedido pendiente
        }

        // Función ficticia para obtener el ID de usuario desde la cookie
        private int ObtenerIdUsuarioDesdeCookie()
        {
            // Aquí debes implementar la lógica para obtener el ID de usuario desde la cookie
            // Por ejemplo:
            var usuarioCookie = Request.Cookies["UsuarioCookie"];
            var idUsuario = Convert.ToInt32(usuarioCookie?.Split('|')[0]);
            return idUsuario;
        }

        //para el historial ----------------------------------------------
        public IActionResult HistorialPedidos()
        {
            int idUsuario = ObtenerIdUsuarioDesdeCookie();

            var historialPedidos = (from p in _context.pedidos
                                    join dp in _context.detalles_Pedidos on p.Id_Pedido equals dp.id_pedido
                                    join mi in _context.items_menu on dp.id_comida equals mi.id_item_menu
                                    where p.id_usuario == ObtenerIdUsuarioDesdeCookie() && p.Estado == "Enviado"
                                    select new
                                    {
                                        IdPedido = p.Id_Pedido,
                                        FechaHora = p.fecha_hora,
                                        NombreItemMenu = mi.nombre
                                    }).ToList();

            ViewData["HistorialPedidos"] = historialPedidos;
            return View(historialPedidos);
        }

    }
}
