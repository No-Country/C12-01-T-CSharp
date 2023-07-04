using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Libro : BaseEntidad
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string FotoUrl { get; set; }
        public Genero Genero { get; set; }
        public int GeneroId { get; set; }
        public Autor Autor { get; set; }
        public int AutorId { get; set; }

    }
}
