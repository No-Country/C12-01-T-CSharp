using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.entidades.Orden
{
    public class Direccion
    {
        public Direccion()
        {
        }

        public Direccion(string nombre, string apellido, string calle, string ciudad, string estado, string codigoPostal)
        {
            Nombre = nombre;
            Apellido = apellido;
            Calle = calle;
            Ciudad = ciudad;
            Estado = estado;
            CodigoPostal = codigoPostal;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
    }
}
