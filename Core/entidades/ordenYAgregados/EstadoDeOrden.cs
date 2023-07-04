using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.entidades.Orden
{
    public enum EstadoDeOrden
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,
        [EnumMember(Value = "Pago recibido")]
        PagoRecibido,
        [EnumMember(Value = "Pago fallido")]
        PagoFallido

    }
}
