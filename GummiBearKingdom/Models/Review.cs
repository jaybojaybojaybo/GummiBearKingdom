using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GummiBearKingdom.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Author { get; set; }

        [StringLength(255, ErrorMessage = "Cut it down. The Review cannot exceed 255 characters.")]
        public string Content_Body { get; set; }

        [Range(1, 5, ErrorMessage = "Price must be between 1 and 5")]
        public int rating { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
