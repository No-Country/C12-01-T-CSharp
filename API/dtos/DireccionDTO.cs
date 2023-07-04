using System.ComponentModel.DataAnnotations;

namespace API.dtos
{
    public class DireccionDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string CodigoPostal { get; set; }

    }
}
