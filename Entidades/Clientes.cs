using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroOrden.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }

        public Clientes()
        {
            ClienteId = 0;
            Nombres = string.Empty;
        }
    }
}
