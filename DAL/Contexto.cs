using Microsoft.EntityFrameworkCore;
using RegistroOrden.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroOrden.DAL
{
    public class Contexto :DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA/Ordenes.db");
        }
    }
}
