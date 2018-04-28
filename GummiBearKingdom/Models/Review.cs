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
        public string Content_Body { get; set; }
        public int rating { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
