using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DulceSaborOnline___WEB.Models
{
    public class Pagos
    {
        [Key]
        public int Id_pagos { get; set; }

        [Column("forma pago")] // Especifica el nombre de la columna en la base de datos
        public int FormaPago { get; set; }
        public decimal total { get; set; }
        public int id_usuario { get; set; }
    }
}
