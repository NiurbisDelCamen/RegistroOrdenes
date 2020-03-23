using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroOrden.Entidades
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Inventario { get; set; }

        [ForeignKey("ProductoId")]

        public virtual List<OrdenesDetalle> OrdenesDetalle { get; set; }

        public Producto()
        {
            ProductoId = 0;
            Descripcion = string.Empty;
            Precio = 0;
            Inventario = 0;

            OrdenesDetalle = new List<OrdenesDetalle>();
        }



    }
}
