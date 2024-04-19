using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DulceSaborOnline___WEB.Models
{
    public class items_menu
    {
        [Key]
        public int id_item_menu { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
        public int id_estado { get; set; }
        public int id_categoria { get; set; }
    }
}
