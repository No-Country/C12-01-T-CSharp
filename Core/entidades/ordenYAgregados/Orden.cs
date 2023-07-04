using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.entidades.Orden
{
    public class Orden
    {
        public string EmailComprador { get; set; }
        public DateTime FechaDeOrden { get; set; } = DateTime.UtcNow;
        public Direccion DirecionDeEnvio { get; set; }
        public FormaDeEnvio FormaDeEnvio { get; set; }
        public IReadOnlyList<OrdenItem> OrdenItems { get; set; }
        public decimal Subtotal { get; set; }
        public EstadoDeOrden Estado { get; set; } = EstadoDeOrden.Pendiente;
        public string IdComprador { get; set; }
        
        public Orden() { }
        public Orden(IReadOnlyList<OrdenItem> ordenItems, string emailComprador, Direccion direccionDeEnvio,
            FormaDeEnvio formaDeEnvio, decimal subtotal)
        {
            EmailComprador = emailComprador;
            DirecionDeEnvio = direccionDeEnvio;
            FormaDeEnvio = formaDeEnvio;
            OrdenItems = ordenItems;
            Subtotal = subtotal;
        }
        public decimal ObtenerTotal()
        {
            return Subtotal + FormaDeEnvio.Precio;
        }

    }
}
