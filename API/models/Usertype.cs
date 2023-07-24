using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class Usertype
    {
        [Key]
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
}
