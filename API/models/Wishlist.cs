using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class Wishlist
    {
        [Key]
        public string WishlistId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
