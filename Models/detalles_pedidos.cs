using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DulceSaborOnline___WEB.Models
{
    public class detalles_pedidos
    {
        [Key]
        public int id_detalles_pedidos { get; set; }
        public int id_pedido { get; set; }
        public int id_comida {  get; set; }
        public string Tipo_Plato { get; set; }
        public string comentario {  get; set; }
    }
}
