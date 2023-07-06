using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Book
    {
        public int Bookid { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Coverfilename { get; set; }
    }
}
