using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroOrden.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        [ForeignKey("Clientes")]
        public decimal Monto { get; set; }

        [ForeignKey("OrdenId")]
        

        public virtual List<OrdenesDetalle> Detalles { get; set; }
        
        public Ordenes()
        {
            OrdenId = 0;
            Fecha = DateTime.Now;
            ClienteId = 0;
            Monto = 0;
           Detalles = new List<OrdenesDetalle>();
        }

    }
}
