namespace API.dtos
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string FotoUrl { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }

    }
}
