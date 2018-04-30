using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GummiBearKingdom.Models
{
    public class EFProductRepository : IGummiRepository
    {        

        GummiDbContext db = new GummiDbContext();

        public EFProductRepository()
        {
            db = new GummiDbContext();
        }

        public EFProductRepository(GummiDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Product> Products { get { return db.Products; } }

        public Product Save(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return product;
        }

        public void Remove(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public IQueryable<Review> Reviews { get { return db.Reviews; } }

        public Review Save(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return review;
        }
    }
}
