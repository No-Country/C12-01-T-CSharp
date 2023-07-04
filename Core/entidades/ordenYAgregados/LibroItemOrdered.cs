using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.entidades.Orden
{
    public class LibroItemOrdered
    {

        public int LibroItemId { get; set; }
        public string NombreProducto { get; set; }
        public string ImagenUrl { get; set; }
        public LibroItemOrdered()
        {
        }

        public LibroItemOrdered(int libroItemId, string nombreProducto, string imagenURL)
        {
            LibroItemId = libroItemId;
            NombreProducto = nombreProducto;
            ImagenUrl = imagenURL;
        }
    }
}
