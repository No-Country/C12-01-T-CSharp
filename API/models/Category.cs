using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
