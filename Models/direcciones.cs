using System.ComponentModel.DataAnnotations;

namespace DulceSaborOnline___WEB.Models
{
    public class direcciones
    {
        [Key]
        public int id_direc {  get; set; }
        public string direccion {  get; set; }
    }
}
