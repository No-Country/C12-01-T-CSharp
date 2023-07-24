using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BookToCreateDto
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public byte[]? CoverFileContent { get; set; }

    }
}
