﻿using Microsoft.EntityFrameworkCore;

namespace DulceSaborOnline___WEB.Models
{
    public class DScontext : DbContext
    {
        public DScontext(DbContextOptions<DScontext> options) : base(options) { }
    
        //Usuarios
        public DbSet<Usuarios> usuarios { get; set; }
        
        //Menus
        public DbSet<items_menu> items_menu { get; set; }
        public DbSet<combos> combos { get; set; }
        public DbSet<Pedidos> pedidos { get; set; }
        public DbSet<Pagos> pagos { get; set; }
        public DbSet<detalles_pedidos> detalles_Pedidos{ get; set; }
    }
}
