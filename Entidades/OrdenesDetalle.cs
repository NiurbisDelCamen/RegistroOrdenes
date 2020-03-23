using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroOrden.Entidades
{
public class OrdenesDetalle
    {
        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int ArticuloId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public OrdenesDetalle()
        {
            Id = 0;
            OrdenId = 0;
            ProductoId = 0;
            ArticuloId = 0;
            Cantidad = 0;
            Precio = 0;
        }

        public OrdenesDetalle(int ordenID, int productoId, int articuloId, decimal cantidad, decimal precio)
        {
            Id = 0;
            OrdenId = ordenID;
            ProductoId = productoId;
            ArticuloId = articuloId;
            Cantidad = cantidad;
            Precio = precio;

        }


    }
}
