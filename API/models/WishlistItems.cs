using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class WishlistItems
    {
        [Key]
        public int WishlistItemId { get; set; }
        public string WishlistId { get; set; }
        public int ProductId { get; set; }
    }
}
