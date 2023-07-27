namespace API.Dtos
{
    public class BookToUpdateDto
    {            
        public string Title { get; set; } 
        public string Author { get; set; } 
        public string Category { get; set; } 
        public decimal Price { get; set; }
        public byte[]? CoverFileContent { get; set; }
    }
}
