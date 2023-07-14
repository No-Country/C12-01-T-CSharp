using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class CartItems
    {
        [Key]
        public int CartItemId { get; set; }
        public string CartId { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
