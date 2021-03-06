﻿using System;
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

        public void RemoveAll()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE products");
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE reviews");
            db.SaveChanges();
        }

        public IQueryable<Review> Reviews { get { return db.Reviews; } }

        public Review Save(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return review;
        }

        public IQueryable<Category> Categories { get { return db.Categories; } }

        public Category Save(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }

        public Category Edit(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return category;
        }
    }
}
