using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GummiBearKingdom.Models
{
    public class GummiTestDbContext : GummiDbContext
    {
        public override DbSet<Product> Products { get; set; }
        public override DbSet<Category> Categories { get; set; }
        public override DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseMySql(@"Server=localhost;Port=8889;database=gummibearkingdom_tests;uid=root;pwd=root;");
    }
}
