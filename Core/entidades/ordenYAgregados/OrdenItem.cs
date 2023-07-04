using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.entidades.Orden
{
    public class OrdenItem : BaseEntidad
    {
        public LibroItemOrdered LibroDeseado { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public OrdenItem()
        {

        }

        public OrdenItem(LibroItemOrdered libroDeseado, decimal precio, int cantidad)
        {
            LibroDeseado = libroDeseado;
            Precio = precio;
            Cantidad = cantidad;
        }
    }
}
