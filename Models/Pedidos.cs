using System.ComponentModel.DataAnnotations;

namespace DulceSaborOnline___WEB.Models
{
    public class Pedidos
    {
        [Key]
        public int Id_Pedido { get; set; }
        public int id_usuario { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public DateTime fecha_hora { get; set; }
        public int com_prom {  get; set; }
        public int direccion_id {  get; set; }
    }
}
