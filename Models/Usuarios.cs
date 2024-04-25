using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DulceSaborOnline___WEB.Models
{
    public class Usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }    
        public string direccion { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasena { get; set; }
        public string? foto { get; set; }
    }
}
