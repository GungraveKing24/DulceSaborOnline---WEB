using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DulceSaborOnline___WEB.Models
{
    public class Combos
    {
        [Key]
        public int id_combo { get; set; }
        public string? descripcion { get; set; }
        public decimal precio { get; set; }
        public string? imagen { get; set; }
        public int id_estado { get; set; }
    }
}
