using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.entidades.Orden
{
    public class FormaDeEnvio : BaseEntidad
    {
        public string NombreAbreviado { get; set; }
        public string TiempoDeEnvio { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
