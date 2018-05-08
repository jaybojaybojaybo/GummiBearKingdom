using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GummiBearKingdom.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Product(int productId, string name, string description, int price, int categoryId)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public Product() { }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is Product))
            {
                return false;
            }
            else
            {
                Product newProduct = (Product)obj;
                return this.ProductId.Equals(newProduct.ProductId);
            }
        }

        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode();
        }

        public int GetAvgRate(Product product)
        {
            if (product.Reviews.Count > 0)
            {
                List<int> ratings = new List<int>();
                foreach (var rev in product.Reviews)
                {
                    ratings.Add(rev.rating);
                }
                int avgRate = (int)Math.Round(ratings.Average());
                return avgRate;
            } else {
                return 0;
            }
        }
    }
}
