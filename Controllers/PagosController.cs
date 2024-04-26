using DulceSaborOnline___WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DulceSaborOnline___WEB.Controllers
{
    public class PagosController : Controller
    {
        private readonly DScontext _context;

        public PagosController(DScontext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int idUsuario = ObtenerIdUsuarioDesdeCookie();
            var pedidosPendientes = (from p in _context.pedidos
                                     join dp in _context.detalles_Pedidos on p.Id_Pedido equals dp.id_pedido
                                     join mi in _context.items_menu on dp.id_comida equals mi.id_item_menu
                                     where p.id_usuario == idUsuario && p.Estado == "Pendiente"
                                     select new
                                     {
                                         idpedido = dp.id_detalles_pedidos,
                                         Imagen = mi.imagen,
                                         Nombre = mi.nombre,
                                         Descripcion = mi.descripcion,
                                         Precio = mi.precio,
                                         FechaHora = p.fecha_hora
                                     }).ToList();

            var direcciones = (from D in _context.direcciones
                               select new
                               {
                                   ID = D.id_direc,
                                   Direccion = D.direccion
                               }).ToList();

            decimal sumaPrecios = pedidosPendientes.Sum(pp => pp.Precio);

            ViewData["DireccionesTake"] = direcciones;
            ViewData["PedidosPendientes"] = pedidosPendientes;
            ViewData["SumaPrecios"] = sumaPrecios;


            return View("Index");
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

        public int ObtenerIdPedidoPendiente(int idUsuario)
        {
            var idPedidoPendiente = _context.pedidos
                .Where(p => p.id_usuario == idUsuario && p.Estado == "Pendiente")
                .Select(p => p.Id_Pedido)
                .FirstOrDefault();

            // Puedes manejar el caso en el que no se encuentre ningún pedido pendiente para el usuario aquí
            return idPedidoPendiente; // Devolver el ID del pedido pendiente
        }

        //metodos para pagar y cancelar pedido
        [HttpPost]
        public IActionResult Pagar(int pagoTipo, int direccionId)
        {
            var pedidosPendientes = (from p in _context.pedidos
                                     join dp in _context.detalles_Pedidos on p.Id_Pedido equals dp.id_pedido
                                     join mi in _context.items_menu on dp.id_comida equals mi.id_item_menu
                                     where p.id_usuario == ObtenerIdUsuarioDesdeCookie() && p.Estado == "Pendiente"
                                     select new
                                     {
                                         Imagen = mi.imagen,
                                         Nombre = mi.nombre,
                                         Descripcion = mi.descripcion,
                                         Precio = mi.precio,
                                         FechaHora = p.fecha_hora
                                     }).ToList();
            decimal sumaPrecios = pedidosPendientes.Sum(pp => pp.Precio);

            InsertarPago(pagoTipo, sumaPrecios, ObtenerIdUsuarioDesdeCookie());
            ActualizarPedido(direccionId);

            // Redirige a una vista de confirmación, página de inicio u otra página según sea necesario
            return RedirectToAction("Index", "Home");
        }

        public void InsertarPago(int idFormaPago, decimal total, int idUsuario)
        {
            var nuevoPago = new Pagos
            {
                FormaPago = idFormaPago,
                total = total,
                id_usuario= idUsuario
            };

            _context.pagos.Add(nuevoPago);
            _context.SaveChanges();
        }

        // Método para actualizar en la tabla pedidos
        public void ActualizarPedido(int direcionIDHTML)
        {
            int idpedido = ObtenerIdPedidoPendiente(ObtenerIdUsuarioDesdeCookie());

            var pedido = _context.pedidos.Find(idpedido);
            if (pedido != null)
            {
                pedido.Estado = "Enviado";
                pedido.fecha_hora = DateTime.Now; // Opcional: puedes agregar la fecha de actualización
                pedido.direccion_id = direcionIDHTML;
                _context.SaveChanges();
            }
            else
            {
                // Manejar el caso en que el pedido no se encuentre
                // Esto podría ser lanzar una excepción, enviar un mensaje de error, etc.
            }
        }

        //metoo eliminar a detalle pedido
        [HttpPost]
        public ActionResult EliminarPedido(int idDPedido)
        {
            var detallePedido = _context.detalles_Pedidos.Find(idDPedido);
            if (detallePedido != null)
            {
                // Eliminar el detalle del pedido
                _context.detalles_Pedidos.Remove(detallePedido);
                _context.SaveChanges(); // Guardar los cambios en la base de datos
                return RedirectToAction("Index", "Pagos"); // Redireccionar a alguna vista apropiada
            }
            else
            {
                return RedirectToAction("Home", "Error"); // Manejar el caso donde el detalle del pedido no se encontró
            }
        }
    }
}
